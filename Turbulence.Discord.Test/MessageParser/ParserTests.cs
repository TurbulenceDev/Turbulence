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
        void print(Node node, int depth = 0)
        {
            Console.WriteLine($"{new string('-', depth)}{node}");
            if (node.Children != null)
            {
                foreach (var c in node.Children)
                {
                    print(c, depth + 1);
                }
            }
        }
        foreach (var part in parts)
        {
            print(part);
        }
    }
}