using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test.MessageParser;

public class ParserTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TextBold()
    {
        var tokens = new Token[]
        {
            new Token(TokenType.TEXT_INLINE, "hello, "),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.TEXT_INLINE, "bold"),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.STAR, "*"),
        };
        var parts = Parser.ParseTokens(tokens);
        Assert.IsNotEmpty(parts);
        foreach (var part in parts)
        {
            Console.WriteLine(part);
        }
    }
}