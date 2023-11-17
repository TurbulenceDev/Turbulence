using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Turbulence.Discord.Models;
using Turbulence.Discord.Utils.Parser;

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
public record Node(NodeType Type, string? text = null, Snowflake? Id = null, string? Emoji = null, string? CodeLanguage = null, string? Url = null, List<Node>? children = null)
{
    public string? Text { get; set; } = text;
    public List<Node>? Children { get; set; } = children;

    //TODO: to dict?
}

public static class Parser
{
    public static List<Node> parse_tokens(Token[] tokens)
    {
        return merge_text_nodes(parse_tokens_generator(tokens.ToArray()));
    }


    public static List<Node> merge_text_nodes(IEnumerable<Node> subtree)
    {
        var compressed_tree = new List<Node>();
        Node? prev_text_node = null;
        foreach (var node in subtree)
        {
            if (node.Type == NodeType.TEXT)
            {
                if (prev_text_node is null)
                {
                    prev_text_node = node;
                }
                else
                {
                    prev_text_node.Text += node.Text;
                    continue; // don't store this node
                }
            }
            else
            {

                prev_text_node = null;
            }

            if (node.Children is not null)
            {
                node.Children = merge_text_nodes(node.Children);
            }

            compressed_tree.Add(node);
        }
        return compressed_tree;
    }

    public static IEnumerable<Node> parse_tokens_generator(Token[] tokens, bool in_quote = false)
    {
        var i = 0;
        while (i < tokens.Length)
        {
            Token current_token = tokens[i];

            // === simple node types without children
            // just continue; once any of them match

            // text
            if (current_token.Type == TokenType.TEXT_INLINE)
            {
                yield return new Node(NodeType.TEXT, text: current_token.Value);
                i += 1;
                continue;
            }

            // user mentions
            if (current_token.Type == TokenType.USER_MENTION)
            {
                yield return new Node(NodeType.USER, Id: new(ulong.Parse(current_token.Groups![0].Value)));
                i += 1;
                continue;
            }

            // role mentions
            if (current_token.Type == TokenType.ROLE_MENTION)
            {
                yield return new Node(NodeType.ROLE, Id: new(ulong.Parse(current_token.Groups![0].Value)));
                i += 1;
                continue;
            }

            // channel mentions
            if (current_token.Type == TokenType.CHANNEL_MENTION)
            {
                yield return new Node(NodeType.CHANNEL, Id: new(ulong.Parse(current_token.Groups![0].Value)));
                i += 1;
                continue;
            }

            // custom emoji
            if (current_token.Type == TokenType.EMOJI_CUSTOM)
            {
                yield return new Node(
                    NodeType.EMOJI_CUSTOM,
                    Id: new(ulong.Parse(current_token.Groups![1].Value)),
                    Emoji: current_token.Groups[0].Value
                );
                i += 1;
                continue;
            }

            // unicode emoji (when it's encoded as :name: and not just written as unicode)
            if (current_token.Type == TokenType.EMOJI_UNICODE_ENCODED)
            {
                yield return new Node(
                    NodeType.EMOJI_UNICODE_ENCODED,
                    Emoji: current_token.Groups![0].Value
                );
                i += 1;
                continue;
            };

            // URL with preview
            if (current_token.Type == TokenType.URL_WITH_PREVIEW)
            {
                yield return new Node(NodeType.URL_WITH_PREVIEW, Url: current_token.Value);
                i += 1;
                continue;
            }

            // URL without preview
            if (current_token.Type == TokenType.URL_WITHOUT_PREVIEW)
            {
                yield return new Node(NodeType.URL_WITHOUT_PREVIEW, Url: current_token.Value[1..^1]);
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

            // format: delimiter, type
            var text_modifiers = new Dictionary<TokenType[], NodeType>()
            {
                { new TokenType[]{ TokenType.STAR, TokenType.STAR }, NodeType.BOLD },
                { new TokenType[]{ TokenType.UNDERSCORE, TokenType.UNDERSCORE }, NodeType.UNDERLINE },
                { new TokenType[]{ TokenType.TILDE, TokenType.TILDE }, NodeType.STRIKETHROUGH },
                { new TokenType[]{ TokenType.STAR }, NodeType.ITALIC },
                { new TokenType[]{ TokenType.UNDERSCORE }, NodeType.ITALIC },
                { new TokenType[]{ TokenType.SPOILER_DELIMITER }, NodeType.SPOILER },
                { new TokenType[]{ TokenType.CODE_INLINE_DELIMITER }, NodeType.CODE_INLINE },
            };


            {
                Node? node = null;
                int? amount_consumed_tokens = null;
                foreach ((var delimiter, var node_type) in text_modifiers)
                {
                    var res = try_parse_node_with_children(
                        tokens[i..], delimiter, delimiter, node_type, in_quote
                    );
                    node = res.Item1;
                    amount_consumed_tokens = res.Item2;

                    if (node is not null)
                        break;
                }


                if (node is not null)
                {
                    i += amount_consumed_tokens!.Value;
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

            if (current_token.Type == TokenType.CODE_BLOCK_DELIMITER)
            {
                var (children_token, amount_consumed_tokens) = search_for_closer(
                    tokens[(i + 1)..], new TokenType[] { TokenType.CODE_BLOCK_DELIMITER }
                );
                if (children_token is not null)
                {
                    var children_content = "";
                    // treat all children token as inline text
                    foreach (var child_token in children_token)
                    {
                        children_content += child_token.Value;
                    }

                    // check for a language specifier
                    var lines = children_content.Split("\n");
                    // there must be at least one other non-empty line
                    // (the content doesn't matter, there just has to be one)
                    var non_empty_line_found = false;

                    string? lang = null;
                    for (var line_index = 1; line_index < lines.Length; line_index++)
                    {
                        if (lines[line_index].Length > 0)
                        {
                            non_empty_line_found = true;
                            break;
                        }
                    }
                    if (non_empty_line_found)
                    {
                        var re = new Regex("^([a-zA-Z0-9-]*)(.*)$");
                        var match = re.Match(lines[0]);
                        //if there is any behind the lang spec, then it is normal text
                        // otherwise, it is either a lang spec (gets removed from the
                        // displayed text) or it is empty (newline gets removed)
                        if (match.Captures[2].Length == 0)
                        {
                            lines = lines[1..];  // remove first line from code block
                            if (match.Captures[1].Length > 0)
                            {
                                lang = match.Captures[1].Value;
                            }
                        }
                    }


                    children_content = string.Join("\n", lines);
                    var child_node = new Node(NodeType.TEXT, text: children_content);
                    yield return new Node(NodeType.CODE_BLOCK, CodeLanguage: lang, children: new List<Node>() { child_node });
                    i += 1 + amount_consumed_tokens!.Value;
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
            Token[]? children_token_in_quote_block = null;
            // note that in_quote won't change during the while-loop, we're just reducing
            // the level of indentation here by including it in the condition instead of
            // making an additional if statement around the while loop
            while (
                !in_quote &&
                (i < tokens.Length) &&
                    (tokens[i].Type == TokenType.QUOTE_LINE_PREFIX))
            {
                // scan until next newline
                var found = false;
                for (var j = i; j < tokens.Length; j++)
                {
                    if (tokens[j].Type == TokenType.NEWLINE)
                    {
                        // add everything from the quote line prefix (non-inclusive)
                        // to the newline (inclusive) as children token
                        children_token_in_quote_block = tokens[(i + 1)..(j + 1)];
                        i = j + 1;  // move to the token after the newline
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    // this is the last line,
                    // all remaining tokens are part of the quote block
                    children_token_in_quote_block = tokens[(i + 1)..];
                    i = tokens.Length;  // move to the end
                    break;
                }
            }


            if (children_token_in_quote_block != null)
            {
                // tell the inner parse function that it's now inside a quote block
                var children_nodes = parse_tokens_generator(children_token_in_quote_block, in_quote = true);
                yield return new Node(NodeType.QUOTE_BLOCK, children: children_nodes.ToList());
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
            yield return new Node(NodeType.TEXT, current_token.Value);
            i += 1;
        }
    }


    public static (Node?, int?) try_parse_node_with_children(
        Token[] tokens,
        TokenType[] opener,
        TokenType[] closer,
        NodeType node_type,
        bool in_quote
    )
    {
        //if there aren't enough tokens to match this node type, abort immediately
        // +1 because there needs to be at least one child token
        if (tokens.Length < opener.Length + 1 + closer.Length)
            return (null, null);

        // check if the opener matches
        for (var opener_index = 0; opener_index < opener.Length; opener_index++)
        {
            if (tokens[opener_index].Type != opener[opener_index])
            {
                return (null, null);
            }
        }

        // try finding the matching closer and consume as few tokens as possible
        // (skip the first token as that has to be a child token)
        // TODO: edge case ***bold and italic*** doesn't work
        var (children_token, amount_consumed_tokens) = search_for_closer(
            tokens[(opener.Length + 1)..], closer
        );

        if (children_token is null)
        {
            // closer not found, abort trying to parse as the selected node type
            return (null, null);
        }

        // put first child token back in
        children_token = new Token[] { tokens[opener.Length] }.Concat(children_token!).ToArray();
        //children_token = (tokens[opener.Length], *children_token);
        amount_consumed_tokens += opener.Length + 1;

        return (
            new Node(
                node_type,
                children: parse_tokens_generator(children_token, in_quote).ToList()
            ),
            amount_consumed_tokens);
    }


    public static (Token[]?, int?) search_for_closer(Token[] tokens, TokenType[] closer)
    {
        // iterate over tokens
        for (var token_index = 0; token_index < (tokens.Length - closer.Length + 1); token_index++)
        {
            var matches = true;
            // try matching the closer to the current position by iterating over the closer
            for (var closer_index = 0; closer_index < closer.Length; closer_index++)
            {
                if (tokens[token_index + closer_index].Type != closer[closer_index])
                {
                    matches = false;
                    break;
                }
            }

            // closer matched
            if (matches)
            {
                return (tokens[..(token_index)], token_index + closer.Length);
            }
        }
        // closer didn't match, try next token_index

        // closer was not found
        return (null, null);
    }
}
