using Avalonia.Controls;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.MessageType;

namespace Turbulence.Desktop.Views.Main;

public partial class MessageView : UserControl
{
    public MessageView(Message message)
    {
        InitializeComponent();

        // TODO: Profile picture
        
        Author.Text = message.GetBestAuthorName();
        
        // TODO: make timestamp relative?
        var localTime = message.Timestamp.ToLocalTime();
        Timestamp.Text = localTime.ToString("G");
        Timestamp.SetValue(ToolTip.TipProperty, localTime.ToString("F"));

        Content.Text = message.Type switch
        {
            THREAD_CREATED => $"{Author.Text} created thread \"{message.Content}\"",
            CALL => $"{Author.Text} started a voice call",
            _ => message.Content,
        };
    }
}
