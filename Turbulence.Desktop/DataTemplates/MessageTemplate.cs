using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class MessageTemplate : IDataTemplate
{
    public bool Match(object? data) => data is Message;

    Control? ITemplate<object?, Control?>.Build(object? param)
    {
        if (param is not Message message)
            return new TextBlock() { Text = "Error" };

        var parent = new StackPanel() { Orientation = Orientation.Vertical };
        parent.Children.Add(new TextBlock() 
        { 
            FontWeight = FontWeight.Bold, 
            Text = message.GetBestAuthorName() 
        });
        parent.Children.Add(new TextBlock() { Text = message.Content });
        return parent;
    }
}
