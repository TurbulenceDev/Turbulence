using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Metadata;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class ChannelTemplateSelector : IDataTemplate
{
    public bool SupportsRecycling => false;
    [Content]
    public Dictionary<string, IDataTemplate> Templates { get; } = new Dictionary<string, IDataTemplate>();

    public bool Match(object? data) => data is Channel;

    Control? ITemplate<object?, Control?>.Build(object? param)
    {
        var channel = (Channel)param!;
        if (!Templates.TryGetValue(channel.Type.ToString(), out var template))
            return Templates["unknown"].Build(param);
        return template.Build(param);
    }
}
