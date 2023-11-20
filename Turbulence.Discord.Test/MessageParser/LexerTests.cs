using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test.MessageParser;

public class LexerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var tokens = Lexer.Lex("hello, **world**");
        Assert.That(tokens, Is.Not.Null);
        Assert.That(tokens, Is.Not.Empty);
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
        var should = new Token[]
        {
            new Token(TokenType.TEXT_INLINE, "hello, "),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.TEXT_INLINE, "world"),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.STAR, "*"),
        };
        Assert.That(tokens.Count(), Is.EqualTo(should.Length));
        for (var i = 0; i < should.Length; i++)
        {
            var actual = tokens.ElementAt(i);
            var expected = should[i];
            Assert.Multiple(() =>
            {
                Assert.That(actual.Type, Is.EqualTo(expected.Type));
                Assert.That(actual.Value, Is.EqualTo(expected.Value));
            });
        }
    }
}