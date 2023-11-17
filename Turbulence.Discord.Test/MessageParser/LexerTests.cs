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
        var tokens = Lexer.Lex("hello, **bold**");
        Assert.IsNotNull(tokens);
        Assert.IsNotEmpty(tokens);
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}