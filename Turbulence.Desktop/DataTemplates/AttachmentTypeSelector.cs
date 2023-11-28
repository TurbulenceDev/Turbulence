using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class AttachmentTypeSelector : IDataTemplate
{
    [Content]
    public Dictionary<string, IDataTemplate> Templates { get; } = new Dictionary<string, IDataTemplate>();

    public bool Match(object? data) => data is Attachment;

    Control? ITemplate<object?, Control?>.Build(object? param)
    {
        var attachment = (Attachment)param!;
        var type = attachment.ContentType switch
        {
            "image/png" or "image/jpeg" => "image",
            _ => "default",
        };
        if (!Templates.TryGetValue(type, out var template))
            return Templates["unknown"].Build(param);
        return template.Build(param);
    }
}