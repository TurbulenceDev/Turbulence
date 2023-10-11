using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Layout;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.DataTemplates;

public class MessageTemplate : IDataTemplate
{
    public bool Match(object? data) => data is Message;

    Control? ITemplate<object?, Control?>.Build(object? param)
    {
        if (param is not Message message)
            return new TextBlock() { Text = "Error" };

        var grid = new Grid()
        {
            ColumnDefinitions = new("Auto,*")
        };
        grid.Classes.Add("Message");
        // Avatar //TODO: actually use images
        var image = new Border();
        image.Classes.Add("Image");
        grid.Children.Add(image);
        // Message
        var parent = new StackPanel() { Orientation = Orientation.Vertical };
        parent.Classes.Add("MessageContent");
        parent.SetValue(Grid.ColumnProperty, 1);
        // Header
        var header = new StackPanel() { Orientation = Orientation.Horizontal };
        var author = new TextBlock()
        {
            Text = message.GetBestAuthorName()
        };
        author.Classes.Add("Author");
        var localTime = message.Timestamp.ToLocalTime();
        var timestamp = new TextBlock()
        {
            Text = localTime.ToString("G") // TODO: make timestamp relative?
        };
        timestamp.Classes.Add("Timestamp");
        timestamp.SetValue(ToolTip.TipProperty, localTime.ToString("F")); // shows detailed time as tooltip
        header.Children.Add(author);
        header.Children.Add(timestamp);
        parent.Children.Add(header);
        // Content
        parent.Children.Add(new TextBlock() { Text = message.Content });
        // TODO: reactions?
        grid.Children.Add(parent);
        return grid;
    }
}
