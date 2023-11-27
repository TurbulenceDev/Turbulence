using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Metadata;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class ChannelTemplateSelector : IDataTemplate
{
    [Content]
    public Dictionary<string, IDataTemplate> Templates { get; } = new Dictionary<string, IDataTemplate>();

    public bool Match(object? data) => data is Channel;

    Control? ITemplate<object?, Control?>.Build(object? param)
    {
        var channel = (Channel)param!;
        var type = channel.Type switch
        {
            ChannelType.DM or ChannelType.GROUP_DM => "dm",
            ChannelType.GUILD_CATEGORY => "category",
            _ => "channel",
        };
        if (!Templates.TryGetValue(type, out var template))
            return Templates["unknown"].Build(param);
        return template.Build(param);
    }
}
