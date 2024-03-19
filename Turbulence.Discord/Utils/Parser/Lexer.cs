using System.Text.RegularExpressions;

namespace Turbulence.Discord.Utils.Parser;

public enum TokenType
{
    TEXT_INLINE = 1,
    NEWLINE,
    STAR,
    UNDERSCORE,
    TILDE,
    SPOILER_DELIMITER,
    USER_MENTION,
    ROLE_MENTION,
    CHANNEL_MENTION,
    EMOJI_CUSTOM,
    EMOJI_UNICODE_ENCODED,
    URL_WITH_PREVIEW,
    URL_WITHOUT_PREVIEW,
    QUOTE_LINE_PREFIX,
    CODE_INLINE_DELIMITER,
    CODE_BLOCK_DELIMITER,
    HEADER1,
}

public record Token(TokenType Type, string Value, GroupCollection? Groups = null);

public static partial class Lexer
{
    public static IEnumerable<Token>? Lex(string input)
    {
        var seenSimpleText = "";

        while (true)
        {
            if (input.Length == 0)
            {
                if (seenSimpleText.Length > 0)
                {
                    yield return new(TokenType.TEXT_INLINE, seenSimpleText);
                }
                // finished
                yield break;
            }

            LexingRule? matchingRule = null;
            Match? match = null;
            foreach (var rule in Rules)
            {
                match = rule.Pattern.Match(input);
                if (match.Success)
                {
                    matchingRule = rule;
                    break;
                }
            }

            if (matchingRule == null)
            {
                seenSimpleText += input[0];
                input = input[1..];
                continue;  // don't yield a token in this run
            }

            // cut off matched part
            input = input[match!.Captures[0].Length..];

            // yield inline text if we have some left
            if (seenSimpleText.Length > 0)
            {
                yield return new(TokenType.TEXT_INLINE, seenSimpleText);
                seenSimpleText = "";
            }

            GroupCollection? groups = null;
            if (match.Groups.Count > 0)
            {
                groups = match.Groups;
            }

            yield return new(matchingRule.Type, match.Groups[0].Value, groups);
        }
    }

    private record LexingRule(TokenType Type, Regex Pattern);
    private static readonly LexingRule[] Rules =
    {
        new(TokenType.USER_MENTION, UserMentionRegex()),
        new(TokenType.ROLE_MENTION, RoleMentionRegex()),
        new(TokenType.CHANNEL_MENTION, ChannelMentionRegex()),
        new(TokenType.EMOJI_CUSTOM, CustomEmojiRegex()),
        new(TokenType.EMOJI_UNICODE_ENCODED, UnicodeEmojiRegex()),
        new(TokenType.URL_WITHOUT_PREVIEW, URLRegex()),
        new(TokenType.URL_WITH_PREVIEW, URLPreviewRegex()),
        new(TokenType.QUOTE_LINE_PREFIX, QuoteLineRegex()),
        new(TokenType.TILDE, TildeRegex()),
        new(TokenType.STAR, StarRegex()),
        new(TokenType.UNDERSCORE, UnderscoreRegex()),
        new(TokenType.SPOILER_DELIMITER, SpoilerRegex()),
        new(TokenType.CODE_BLOCK_DELIMITER, CodeBlockRegex()),
        new(TokenType.CODE_INLINE_DELIMITER, CodeInlineRegex()),
        new(TokenType.NEWLINE, NewlineRegex()),
        new(TokenType.HEADER1, Header1Regex())
    };

    // https://discord.com/developers/docs/reference#message-formatting
    private const string URL_REGEX = "http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\\(\\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+";
    [GeneratedRegex("^<@!?([0-9]+)>", RegexOptions.Compiled)]
    private static partial Regex UserMentionRegex();

    [GeneratedRegex("^<@&([0-9]+)>", RegexOptions.Compiled)]
    private static partial Regex RoleMentionRegex();

    [GeneratedRegex("^<#([0-9]+)>", RegexOptions.Compiled)]
    private static partial Regex ChannelMentionRegex();

    [GeneratedRegex("^<:([a-zA-Z0-9_]{2,}):([0-9]+)>", RegexOptions.Compiled)]
    private static partial Regex CustomEmojiRegex();

    [GeneratedRegex("^:([a-zA-Z0-9_]+):", RegexOptions.Compiled)]
    private static partial Regex UnicodeEmojiRegex();

    [GeneratedRegex($"^<{URL_REGEX}>", RegexOptions.Compiled)]
    private static partial Regex URLRegex();

    [GeneratedRegex($"^{URL_REGEX}", RegexOptions.Compiled)]
    private static partial Regex URLPreviewRegex();

    [GeneratedRegex("^(>>)?> ", RegexOptions.Compiled)]
    private static partial Regex QuoteLineRegex();

    [GeneratedRegex("^~", RegexOptions.Compiled)]
    private static partial Regex TildeRegex();

    [GeneratedRegex("^\\*", RegexOptions.Compiled)]
    private static partial Regex StarRegex();

    [GeneratedRegex("^_", RegexOptions.Compiled)]
    private static partial Regex UnderscoreRegex();

    [GeneratedRegex("^\\|\\|", RegexOptions.Compiled)]
    private static partial Regex SpoilerRegex();

    [GeneratedRegex("^```", RegexOptions.Compiled)]
    private static partial Regex CodeBlockRegex();

    [GeneratedRegex("^`", RegexOptions.Compiled)]
    private static partial Regex CodeInlineRegex();

    [GeneratedRegex("^\n", RegexOptions.Compiled)]
    private static partial Regex NewlineRegex();
    
    [GeneratedRegex("^# (.*)(\n)?", RegexOptions.Compiled)]
    private static partial Regex Header1Regex();
}