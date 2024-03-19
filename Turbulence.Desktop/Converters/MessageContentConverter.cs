using Avalonia.Controls.Documents;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.Globalization;
using Avalonia.Controls;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Utils;
using Turbulence.Discord.Utils.Parser;

namespace Turbulence.Desktop.Converters;

public class MessageContentConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Message message)
            return null;

        var res = new InlineCollection();
        var content = _client.GetMessageContent(message);
        var nodes = MessageParser.Parse(content);

        // Recursive function that turns nodes into avalonia inlines (run/span)
        static Inline FromNode(Node node)
        {
            Inline ret;
            switch (node.Type)
            {
                case NodeType.STRIKETHROUGH:
                case NodeType.SPOILER:
                case NodeType.BOLD:
                case NodeType.UNDERLINE:
                case NodeType.ITALIC:
                case NodeType.CODE_INLINE:
                case NodeType.CODE_BLOCK:
                case NodeType.QUOTE_BLOCK:
                case NodeType.HEADER1:
                case NodeType.HEADER2:
                case NodeType.HEADER3:
                    ret = new Span();
                    ret.Classes.Add(node.Type.ToString());
                    if (node.Children != null)
                        foreach (var child in node.Children)
                            ((Span)ret).Inlines.Add(FromNode(child));
                    break;
                case NodeType.URL_WITH_PREVIEW:
                case NodeType.URL_WITHOUT_PREVIEW:
                    ret = new Run(node.Url);
                    ret.Classes.Add("Url");
                    break;
                case NodeType.USER:
                case NodeType.CHANNEL:
                case NodeType.ROLE:
                    //TODO: mentions
                    ret = new Run($"@{node.Id}");
                    break;
                case NodeType.EMOJI_UNICODE_ENCODED:
                case NodeType.EMOJI_CUSTOM:
                    //TODO: emojis
                    ret = new Run($":{node.Emoji}:");
                    break;
                case NodeType.TEXT:
                default:
                    ret = new Run(node.Text);
                    break;
            }

            return ret;
        }

        // Add nodes
        foreach (var node in nodes)
        {
            res.Add(FromNode(node));
        }

        if (message.EditedTimestamp != null)
        {
            var editRun = new Run(" [Edited]");
            //TODO: tooltip?
            editRun.Classes.Add("Edit");
            res.Add(editRun);
        }

        return res;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}