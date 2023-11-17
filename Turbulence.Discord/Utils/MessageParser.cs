

using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Utils;

public static class MessageParser
{
    public static List<Node> parse(string text)
    {
        var tokens = Lexer.Lex(text);
        return Parser.Parser.parse_tokens(tokens.ToArray());
    }
}
