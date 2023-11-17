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