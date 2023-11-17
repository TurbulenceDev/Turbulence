using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test.MessageParser;

public class MessageParserTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var parts = Utils.MessageParser.Parse("hello, **bold**");
        foreach (var part in parts)
        {
            Console.WriteLine(part);
        }
    }
}