

using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Discord.Utils;

public static class MessageParser
{
    public static List<Node> Parse(string text)
    {
        var tokens = Lexer.Lex(text);
        if (tokens == null)
            return new List<Node>();
        return Parser.Parser.ParseTokens(tokens.ToArray());
    }
}
