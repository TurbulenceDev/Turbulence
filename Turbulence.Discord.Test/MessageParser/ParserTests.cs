using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Test.MessageParser;

public class ParserTests
{
    [SetUp]
    public void Setup()
    {
    }

    private static void Test(string text, IEnumerable<Node> should)
    {
        var parts = Utils.MessageParser.Parse(text);
        Assert.That(parts, Is.Not.Empty);

        static void print(Node node, int depth = 0)
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

        // asserts that two nodes (and its children) are the same
        static void assert(Node should, Node actual)
        {
            // Properties
            Assert.Multiple(() =>
            {
                Assert.That(actual.Type, Is.EqualTo(should.Type));
                Assert.That(actual.Text, Is.EqualTo(should.Text));
                Assert.That(actual.Id, Is.EqualTo(should.Id));
                Assert.That(actual.Emoji, Is.EqualTo(should.Emoji));
                Assert.That(actual.CodeLanguage, Is.EqualTo(should.CodeLanguage));
                Assert.That(actual.Url, Is.EqualTo(should.Url));
            });
            // Children
            if (actual.Children == null)
                Assert.That(should.Children, Is.Null);
            else
            {
                Assert.That(should.Children, Is.Not.Null);
                Assert.That(actual.Children.Count(), Is.EqualTo(should.Children!.Count()));
                for (var i = 0; i < actual.Children.Count(); i++)
                {
                    var a = actual.Children.ElementAt(i);
                    var s = should.Children!.ElementAt(i);
                    assert(s, a);
                }
            }
        }

        Assert.That(parts, Has.Count.EqualTo(should.Count()));
        for (var i = 0; i < parts.Count; i++)
        {
            var a = parts[i];
            var s = should.ElementAt(i);
            assert(s, a);
        }
    }
    [Test]
    public void TextBold()
    {
        var text = "hello, **world**";
        var should = new Node[]
        {
            new(NodeType.TEXT, Text: "hello, "),
            new(NodeType.BOLD, Children: new List<Node>(){
                new(NodeType.TEXT, Text: "world")
            }),
        };
        Test(text, should);
    }

    [Test]
    public void Code()
    {
        var text = @"```cs
// *fake*
public void Real() {
  Stuff();
}
```".Replace("\r\n", "\n");
        var should = new Node[]
        {
            new(NodeType.CODE_BLOCK, CodeLanguage: "cs", Children: new List<Node>()
            {
                new(NodeType.TEXT, Text: "// *fake*\npublic void Real() {\n  Stuff();\n}\n")
            })
        };
        Test(text, should);
    }

    [Test]
    public void Mentions()
    {
        var text = "<@138397087229280257>\n<#959372274299961344>\n<@&985907053237268480>";
        var should = new Node[]
        {
            new(NodeType.USER, Id: new(138397087229280257)),
            new(NodeType.TEXT, Text: "\n"),
            new(NodeType.CHANNEL, Id: new(959372274299961344)),
            new(NodeType.TEXT, Text: "\n"),
            new(NodeType.ROLE, Id: new(985907053237268480)),
        };
        Test(text, should);
    }

    [Test]
    public void Emojis()
    {
        var text = ":weary:\n<:salute:933778727547060234>";
        var should = new Node[]
        {
            new(NodeType.EMOJI_UNICODE_ENCODED, Emoji: "weary"),
            new(NodeType.TEXT, Text: "\n"),
            new(NodeType.EMOJI_CUSTOM, Id: new(933778727547060234), Emoji: "salute"),
        };
        Test(text, should);
    }

    [Test]
    public void Links()
    {
        var text = "<https://google.com>\nhttps://google.com";
        var should = new Node[]
        {
            new(NodeType.URL_WITHOUT_PREVIEW, Url: "https://google.com"),
            new(NodeType.TEXT, Text: "\n"),
            new(NodeType.URL_WITH_PREVIEW, Url: "https://google.com"),
        };
        Test(text, should);
    }
}