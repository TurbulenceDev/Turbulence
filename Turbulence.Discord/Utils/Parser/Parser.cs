using System.Text.RegularExpressions;
using Turbulence.Discord.Models;

namespace Turbulence.Discord.Utils.Parser;

public enum NodeType
{
    TEXT = 1,
    ITALIC,
    BOLD,
    UNDERLINE,
    STRIKETHROUGH,
    SPOILER,
    USER,
    ROLE,
    CHANNEL,
    EMOJI_CUSTOM,
    EMOJI_UNICODE_ENCODED,
    URL_WITH_PREVIEW,
    URL_WITHOUT_PREVIEW,
    QUOTE_BLOCK,
    CODE_BLOCK,
    CODE_INLINE
}

public record Node(NodeType Type, string? Text = null, Snowflake? Id = null, string? Emoji = null, string? CodeLanguage = null, string? Url = null, IEnumerable<Node>? Children = null)
{
    public string? Text { get; set; } = Text;
    public IEnumerable<Node>? Children { get; set; } = Children;
}

public static partial class Parser
{
    public static List<Node> ParseTokens(Token[] tokens)
    {
        return MergeTextNodes(ParseTokensGenerator(tokens));
    }


    public static List<Node> MergeTextNodes(IEnumerable<Node> subtree)
    {
        var compressedTree = new List<Node>();
        Node? prevTextNode = null;
        foreach (var node in subtree)
        {
            if (node.Type == NodeType.TEXT)
            {
                if (prevTextNode is null)
                {
                    prevTextNode = node;
                }
                else
                {
                    prevTextNode.Text += node.Text;
                    continue; // don't store this node
                }
            }
            else
            {

                prevTextNode = null;
            }

            if (node.Children is not null)
            {
                node.Children = MergeTextNodes(node.Children);
            }

            compressedTree.Add(node);
        }
        return compressedTree;
    }

    private static readonly Dictionary<TokenType[], NodeType> _textModifiers = new()
            {
                { new TokenType[]{ TokenType.STAR, TokenType.STAR }, NodeType.BOLD },
                { new TokenType[]{ TokenType.UNDERSCORE, TokenType.UNDERSCORE }, NodeType.UNDERLINE },
                { new TokenType[]{ TokenType.TILDE, TokenType.TILDE }, NodeType.STRIKETHROUGH },
                { new TokenType[]{ TokenType.STAR }, NodeType.ITALIC },
                { new TokenType[]{ TokenType.UNDERSCORE }, NodeType.ITALIC },
                { new TokenType[]{ TokenType.SPOILER_DELIMITER }, NodeType.SPOILER },
                { new TokenType[]{ TokenType.CODE_INLINE_DELIMITER }, NodeType.CODE_INLINE },
            };
    [GeneratedRegex("^([a-zA-Z0-9-]*)(.*)$", RegexOptions.Compiled)]
    private static partial Regex CodeLanguageRegex();
    public static IEnumerable<Node> ParseTokensGenerator(Token[] tokens, bool inQuote = false)
    {
        var i = 0;
        while (i < tokens.Length)
        {
            Token current = tokens[i];

            // === simple node types without children
            // just continue; once any of them match

            // text
            if (current.Type == TokenType.TEXT_INLINE)
            {
                yield return new Node(NodeType.TEXT, Text: current.Value);
                i += 1;
                continue;
            }

            // user mentions
            if (current.Type == TokenType.USER_MENTION)
            {
                yield return new Node(NodeType.USER, Id: new(ulong.Parse(current.Groups![1].Value)));
                i += 1;
                continue;
            }

            // role mentions
            if (current.Type == TokenType.ROLE_MENTION)
            {
                yield return new Node(NodeType.ROLE, Id: new(ulong.Parse(current.Groups![1].Value)));
                i += 1;
                continue;
            }

            // channel mentions
            if (current.Type == TokenType.CHANNEL_MENTION)
            {
                yield return new Node(NodeType.CHANNEL, Id: new(ulong.Parse(current.Groups![1].Value)));
                i += 1;
                continue;
            }

            // custom emoji
            if (current.Type == TokenType.EMOJI_CUSTOM)
            {
                yield return new Node(
                    NodeType.EMOJI_CUSTOM,
                    Id: new(ulong.Parse(current.Groups![2].Value)),
                    Emoji: current.Groups[1].Value
                );
                i += 1;
                continue;
            }

            // unicode emoji (when it's encoded as :name: and not just written as unicode)
            if (current.Type == TokenType.EMOJI_UNICODE_ENCODED)
            {
                yield return new Node(
                    NodeType.EMOJI_UNICODE_ENCODED,
                    Emoji: current.Groups![1].Value
                );
                i += 1;
                continue;
            };

            // URL with preview
            if (current.Type == TokenType.URL_WITH_PREVIEW)
            {
                yield return new Node(NodeType.URL_WITH_PREVIEW, Url: current.Value);
                i += 1;
                continue;
            }

            // URL without preview
            if (current.Type == TokenType.URL_WITHOUT_PREVIEW)
            {
                yield return new Node(NodeType.URL_WITHOUT_PREVIEW, Url: current.Value[1..^1]);
                i += 1;
                continue;
            }

            // === text modifiers
            // these just modify the look of the text (bold, italic, inline code, ...),
            // can appear everywhere (outside of code blocks) and can span all other
            // elements (including code blocks) and can span across newlines.
            // they must have at least one child token.
            // note, however, that text modifiers (and all other nodes with children),
            // can not overlap partially:
            //   strikethrough is completely inside italic, works:
            //     *a~~b~~c*d = <it>a<s>b</s>c</it>d
            //   strikethrough only partially overlaps italic, strikethrough is ignored
            //     *a~~bc*d~~ = <it>a~~bc</it>~~d
            //
            // known issue:
            // we don't account for the fact that spoilers can't wrap code blocks
            {
                Node? node = null;
                int? consumedTokenCount = null;
                foreach ((var delimiter, var nodeType) in _textModifiers)
                {
                    var res = TryParseNodeWithChildren(
                        tokens[i..], delimiter, delimiter, nodeType, inQuote
                    );
                    node = res.Item1;
                    consumedTokenCount = res.Item2;

                    if (node != null)
                        break;
                }


                if (node != null)
                {
                    i += consumedTokenCount!.Value;
                    yield return node;
                    continue;
                }
            }

            // === code blocks
            // these are similar to text modifiers but have some additional twists
            // - code blocks only contain inline text, all other markdown rules are disabled
            //   inside code blocks
            // - the first line can be a language specifier for syntax highlighting.
            //   - the LS starts immediately after the code block delimiter and is
            //     immediately followed by a newline, otherwise it is treated as normal
            //     text content of the code block.
            //   - if the language specifier is omitted completely, i.e., the code block
            //     delimiter is immediately followed by a newline, then that newline is
            //     removed:
            //       ```
            //       test
            //       ```
            //       is, in HTML, <code-block>test<br /></code-block>
            //       and not <code-block><br />test<br /></code-block>

            if (current.Type == TokenType.CODE_BLOCK_DELIMITER)
            {
                var (childrenToken, consumedTokenCount) = SearchForCloser(
                    tokens[(i + 1)..], new TokenType[] { TokenType.CODE_BLOCK_DELIMITER }
                );
                if (childrenToken != null)
                {
                    var childrenContent = "";
                    // treat all children token as inline text
                    foreach (var child_token in childrenToken)
                    {
                        childrenContent += child_token.Value;
                    }

                    // check for a language specifier
                    var lines = childrenContent.Split("\n");
                    // there must be at least one other non-empty line
                    // (the content doesn't matter, there just has to be one)
                    var nonEmptyLineFound = false;

                    string? lang = null;
                    for (var lineIndex = 1; lineIndex < lines.Length; lineIndex++)
                    {
                        if (lines[lineIndex].Length > 0)
                        {
                            nonEmptyLineFound = true;
                            break;
                        }
                    }
                    if (nonEmptyLineFound)
                    {
                        var match = CodeLanguageRegex().Match(lines[0]);
                        //if there is any behind the lang spec, then it is normal text
                        // otherwise, it is either a lang spec (gets removed from the
                        // displayed text) or it is empty (newline gets removed)
                        if (match.Groups[2].Length == 0)
                        {
                            lines = lines[1..];  // remove first line from code block
                            if (match.Groups[1].Length > 0)
                            {
                                lang = match.Groups[1].Value;
                            }
                        }
                    }


                    childrenContent = string.Join("\n", lines);
                    var child_node = new Node(NodeType.TEXT, Text: childrenContent);
                    yield return new Node(NodeType.CODE_BLOCK, CodeLanguage: lang, Children: new List<Node>() { child_node });
                    i += 1 + consumedTokenCount!.Value;
                    continue;
                }
            }

            // === quote blocks
            // these are a bit trickier. essentially, quote blocks are also
            // "just another text modifier" but with a few more complicated rules
            // - quote blocks always have "> " at the very beginning of every line
            // - quote blocks can span multiple lines, meaning that if multiple consecutive
            // lines start with "> ", then they belong to the same quote block
            // - quote blocks can't be nested. any quote delimiters inside a quote block
            // are just inline text. all other elements can appear inside a quote block
            // - text modifiers
            List<Token>? childrenTokenInQuoteBlock = new();
            // note that in_quote won't change during the while-loop, we're just reducing
            // the level of indentation here by including it in the condition instead of
            // making an additional if statement around the while loop
            while (
                !inQuote &&
                i < tokens.Length &&
                tokens[i].Type == TokenType.QUOTE_LINE_PREFIX)
            {
                // scan until next newline
                var found = false;
                for (var j = i; j < tokens.Length; j++)
                {
                    if (tokens[j].Type == TokenType.NEWLINE)
                    {
                        // add everything from the quote line prefix (non-inclusive)
                        // to the newline (inclusive) as children token
                        childrenTokenInQuoteBlock.AddRange(tokens[(i + 1)..(j + 1)]);
                        i = j + 1;  // move to the token after the newline
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    // this is the last line,
                    // all remaining tokens are part of the quote block
                    childrenTokenInQuoteBlock.AddRange(tokens[(i + 1)..]);
                    i = tokens.Length;  // move to the end
                    break;
                }
            }


            if (childrenTokenInQuoteBlock.Count > 0)
            {
                // tell the inner parse function that it's now inside a quote block
                var childrenNodes = ParseTokensGenerator(childrenTokenInQuoteBlock.ToArray(), inQuote = true);
                yield return new Node(NodeType.QUOTE_BLOCK, Children: childrenNodes.ToList());
                continue;
            }

            //if we get all the way here, than whatever token we're currently sitting on
            // is not an inline text token but also failed to match any of our parsing rules.
            // this happens when a special character, such as ">" or "*" is used as part of
            // normal text.
            // in this case, we just treat it as normal text.
            //
            // TODO:
            // note that we don't combine multiple text nodes here.
            // we *could* do it similar to what we do in the lexer but
            // - remembering inline text into future loop iterations would require adding
            // a check to every yield return-continue; combo in this function, which would be quite
            // ugly
            // - we can't change previous segments without dropping the generator
            // functionality (even though that *is* the current workaround)
            // - we can't look ahead without simulating this entire function on future tokens
            //if you know how to do this *without adding ugly code*: help is appreciated.
            // until then, this is a case of "we'll cross that bridge when we get there",
            // i.e., we'll fix it if anyone comes along that actually needs it
            yield return new Node(NodeType.TEXT, current.Value);
            i += 1;
        }
    }


    private static (Node?, int?) TryParseNodeWithChildren(
        Token[] tokens,
        TokenType[] opener,
        TokenType[] closer,
        NodeType nodeType,
        bool in_quote
    )
    {
        //if there aren't enough tokens to match this node type, abort immediately
        // +1 because there needs to be at least one child token
        if (tokens.Length < opener.Length + 1 + closer.Length)
            return (null, null);

        // check if the opener matches
        for (var openerIndex = 0; openerIndex < opener.Length; openerIndex++)
        {
            if (tokens[openerIndex].Type != opener[openerIndex])
            {
                return (null, null);
            }
        }

        // try finding the matching closer and consume as few tokens as possible
        // (skip the first token as that has to be a child token)
        // TODO: edge case ***bold and italic*** doesn't work
        var (childrenToken, consumedTokenCount) = SearchForCloser(
            tokens[(opener.Length + 1)..], closer
        );

        if (childrenToken == null)
        {
            // closer not found, abort trying to parse as the selected node type
            return (null, null);
        }

        // put first child token back in
        childrenToken = new Token[] { tokens[opener.Length] }.Concat(childrenToken!).ToArray();
        //children_token = (tokens[opener.Length], *children_token);
        consumedTokenCount += opener.Length + 1;

        return (
            new Node(
                nodeType,
                Children: ParseTokensGenerator(childrenToken, in_quote).ToList()
            ),
            consumedTokenCount);
    }


    private static (Token[]?, int?) SearchForCloser(Token[] tokens, TokenType[] closer)
    {
        // iterate over tokens
        for (var tokenIndex = 0; tokenIndex < tokens.Length - closer.Length + 1; tokenIndex++)
        {
            var matches = true;
            // try matching the closer to the current position by iterating over the closer
            for (var closerIndex = 0; closerIndex < closer.Length; closerIndex++)
            {
                if (tokens[tokenIndex + closerIndex].Type != closer[closerIndex])
                {
                    matches = false;
                    break;
                }
            }

            // closer matched
            if (matches)
            {
                return (tokens[..tokenIndex], tokenIndex + closer.Length);
            }
        }
        // closer didn't match, try next token_index

        // closer was not found
        return (null, null);
    }
}
