using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Views.Main;

public partial class MessageView : UserControl
{
    // The ShowJumpToMessage Property shows/hides the "Jump To Message" button. The value can be read in xaml through {TemplateBinding ShowJumpToMessage}
    public static readonly StyledProperty<bool> ShowJumpToMessageProperty =
        AvaloniaProperty.Register<MessageView, bool>(nameof(ShowJumpToMessage), defaultValue: false);

    public bool ShowJumpToMessage
    {
        get { return GetValue(ShowJumpToMessageProperty); }
        set { SetValue(ShowJumpToMessageProperty, value); }
    }

    public MessageView()
    {
        InitializeComponent();
    }

    public void OnAttachmentButton(object? sender, RoutedEventArgs args)
    {
        if (sender is not Control control ||
            control.DataContext is not Attachment attachment)
            return;

        var url = new Uri(attachment.Url);
        // check if its a valid url before running it as a process...
        if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url.AbsoluteUri,
                UseShellExecute = true
            });
        }
    }
}
