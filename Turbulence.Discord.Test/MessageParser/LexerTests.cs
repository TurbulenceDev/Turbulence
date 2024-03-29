using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test.MessageParser;

public class LexerTests
{
    [SetUp]
    public void Setup()
    {
    }

    private static void Test(string text, IEnumerable<Token> should)
    {
        var tokens = Lexer.Lex(text);
        Assert.That(tokens, Is.Not.Null);
        Assert.That(tokens, Is.Not.Empty);
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
        Assert.That(tokens.Count(), Is.EqualTo(should.Count()));
        for (var i = 0; i < tokens.Count(); i++)
        {
            var actual = tokens.ElementAt(i);
            var expected = should.ElementAt(i);
            Assert.Multiple(() =>
            {
                Assert.That(actual.Type, Is.EqualTo(expected.Type));
                Assert.That(actual.Value, Is.EqualTo(expected.Value));
            });
        }
    }

    [Test]
    public void Text()
    {
        var text = "text, **bold**, *italics*, _italics_, ~~strikethrough~~, ||spoiler||";
        var tokens = new Token[]
        {
            new(TokenType.TEXT_INLINE, "text, "),
            new(TokenType.STAR, "*"),
            new(TokenType.STAR, "*"),
            new(TokenType.TEXT_INLINE, "bold"),
            new(TokenType.STAR, "*"),
            new(TokenType.STAR, "*"),
            new(TokenType.TEXT_INLINE, ", "),
            new(TokenType.STAR, "*"),
            new(TokenType.TEXT_INLINE, "italics"),
            new(TokenType.STAR, "*"),
            new(TokenType.TEXT_INLINE, ", "),
            new(TokenType.UNDERSCORE, "_"),
            new(TokenType.TEXT_INLINE, "italics"),
            new(TokenType.UNDERSCORE, "_"),
            new(TokenType.TEXT_INLINE, ", "),
            new(TokenType.TILDE, "~"),
            new(TokenType.TILDE, "~"),
            new(TokenType.TEXT_INLINE, "strikethrough"),
            new(TokenType.TILDE, "~"),
            new(TokenType.TILDE, "~"),
            new(TokenType.TEXT_INLINE, ", "),
            new(TokenType.SPOILER_DELIMITER, "||"),
            new(TokenType.TEXT_INLINE, "spoiler"),
            new(TokenType.SPOILER_DELIMITER, "||"),
        };
        Test(text, tokens);
    }

    [Test]
    public void Code()
    {
        var text = @"`code`
```cs
// *fake*
public void Real() {
  Stuff();
}
```".Replace("\r\n", "\n");
        var tokens = new Token[]
        {
            new(TokenType.CODE_INLINE_DELIMITER, "`"),
            new(TokenType.TEXT_INLINE, "code"),
            new(TokenType.CODE_INLINE_DELIMITER, "`"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.CODE_BLOCK_DELIMITER, "```"),
            new(TokenType.TEXT_INLINE, "cs"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.TEXT_INLINE, "// "),
            new(TokenType.STAR, "*"),
            new(TokenType.TEXT_INLINE, "fake"),
            new(TokenType.STAR, "*"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.TEXT_INLINE, "public void Real() {"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.TEXT_INLINE, "  Stuff();"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.TEXT_INLINE, "}"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.CODE_BLOCK_DELIMITER, "```"),
        };
        Test(text, tokens);
    }

    [Test]
    public void Mentions()
    {
        var text = "<@138397087229280257>\n<#959372274299961344>\n<@&985907053237268480>";
        var tokens = new Token[]
        {
            new(TokenType.USER_MENTION, "<@138397087229280257>"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.CHANNEL_MENTION, "<#959372274299961344>"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.ROLE_MENTION, "<@&985907053237268480>"),
        };
        Test(text, tokens);
    }

    [Test]
    public void Emojis()
    {
        var text = ":weary:\n<:salute:933778727547060234>";
        var tokens = new Token[]
        {
            new(TokenType.EMOJI_UNICODE_ENCODED, ":weary:"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.EMOJI_CUSTOM, "<:salute:933778727547060234>"),
        };
        Test(text, tokens);
    }

    [Test]
    public void Links()
    {
        var text = "<https://google.com>\nhttps://google.com";
        var tokens = new Token[]
        {
            new(TokenType.URL_WITHOUT_PREVIEW, "<https://google.com>"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.URL_WITH_PREVIEW, "https://google.com"),
        };
        Test(text, tokens);
    }

    [Test]
    public void Quote()
    {
        var text = "> line1\n> line2";
        var tokens = new Token[]
        {
            new(TokenType.QUOTE_LINE_PREFIX, "> "),
            new(TokenType.TEXT_INLINE, "line1"),
            new(TokenType.NEWLINE, "\n"),
            new(TokenType.QUOTE_LINE_PREFIX, "> "),
            new(TokenType.TEXT_INLINE, "line2"),
        };
        Test(text, tokens);
    }
}