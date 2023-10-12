using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Turbulence.Desktop.Views.Main;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class MessageTemplate : IDataTemplate
{
    public bool Match(object? data) => data is Message;

    Control ITemplate<object?, Control?>.Build(object? param)
    {
        if (param is not Message message)
            return new TextBlock { Text = $"Error: Provided object was not {nameof(Message)}" };
        
        return new MessageView(message);
    }
}
