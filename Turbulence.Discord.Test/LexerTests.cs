using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test;

public class LexerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var a = Lexer.Lex("hello, **bold**");
        foreach (var b in a)
        {
            Console.WriteLine(b);
        }
        Assert.IsNotEmpty(a);
    }
}