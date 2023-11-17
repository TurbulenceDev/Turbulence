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
    CODE_BLOCK_DELIMITER
}

public record Token(TokenType Type, string Value, CaptureCollection? Groups = null);

public static class Lexer
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
            input = input[(match!.Captures[0].Length)..];

            // yield inline text if we have some left
            if (seenSimpleText.Length > 0)
            {
                yield return new(TokenType.TEXT_INLINE, seenSimpleText);
                seenSimpleText = "";
            }

            CaptureCollection? groups = null;
            if (match.Captures.Count > 0)
            {
                groups = match.Captures;
            }

            yield return new(matchingRule.Type, match.Groups[0].Value, groups);
        }
    }

    private record LexingRule(TokenType Type, Regex Pattern);
    private const string URL_REGEX = "http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\\(\\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+";
    private static readonly LexingRule[] Rules =
    {
        new LexingRule(TokenType.USER_MENTION, new("^<@!?([0-9]+)>")),
        new LexingRule(TokenType.ROLE_MENTION, new("^<@&([0-9]+)>")),
        new LexingRule(TokenType.CHANNEL_MENTION, new("^<#([0-9]+)>")),
        new LexingRule(TokenType.EMOJI_CUSTOM, new("^<:([a-zA-Z0-9_]{2,}):([0-9]+)>")),
        new LexingRule(TokenType.EMOJI_UNICODE_ENCODED, new("^:([a-zA-Z0-9_]+):")),
        new LexingRule(TokenType.URL_WITHOUT_PREVIEW, new($"^<{URL_REGEX}>")),
        new LexingRule(TokenType.URL_WITH_PREVIEW, new($"^{URL_REGEX}")),
        new LexingRule(TokenType.QUOTE_LINE_PREFIX, new("^(>>)?> ")),
        new LexingRule(TokenType.TILDE, new("^~")),
        new LexingRule(TokenType.STAR, new(@"^\*")),
        new LexingRule(TokenType.UNDERSCORE, new("^_")),
        new LexingRule(TokenType.SPOILER_DELIMITER, new(@"^\|\|")),
        new LexingRule(TokenType.CODE_BLOCK_DELIMITER, new("^```")),
        new LexingRule(TokenType.CODE_INLINE_DELIMITER, new("^`")),
        new LexingRule(TokenType.NEWLINE, new("^\n")),
    };
    //TODO: compile deez
}

