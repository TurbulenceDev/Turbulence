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
            new Token(TokenType.TEXT_INLINE, "world"),
            new Token(TokenType.STAR, "*"),
            new Token(TokenType.STAR, "*"),
        };
        var parts = Parser.ParseTokens(tokens);
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

        var should = new Node[]
        {
            new(NodeType.TEXT, Text: "hello, "),
            new(NodeType.BOLD, Children: new List<Node>(){
                new(NodeType.TEXT, Text: "world")
            }),
        };
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
                Assert.That(actual.Children, Has.Count.EqualTo(should.Children!.Count));
                for (var i = 0; i < actual.Children.Count; i++)
                {
                    var a = actual.Children[i];
                    var s = should.Children![i];
                    assert(s, a);
                }
            }
        }

        Assert.That(parts, Has.Count.EqualTo(should.Length));
        for (var i = 0; i < should.Length; i++)
        {
            var a = parts[i];
            var s = should[i];
            assert(s, a);
        }
    }
}