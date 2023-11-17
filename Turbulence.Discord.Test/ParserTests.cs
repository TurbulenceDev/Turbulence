using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test;

public class ParserTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var a = Lexer.Lex("hello, **bold**")!.ToArray();
        foreach (var token in a)
        {
            Console.WriteLine(token);
        }
        var b = Parser.parse_tokens(a);
        foreach (var token in b)
        {
            Console.WriteLine(token);
        }
        Assert.IsNotEmpty(a);
    }
}