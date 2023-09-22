using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Threading;
using Turbulence.Core.ViewModels;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.MessageType;

namespace Turbulence.Desktop.Views;

public partial class MessagesView : UserControl
{
    public MessagesView()
    {
        InitializeComponent();
        
        ((MessagesViewModel)DataContext!).CurrentMessages.CollectionChanged += (_, _) =>
        {
            // TODO: Requires sleep to scroll to end properly, probably because otherwise it scrolls to end before message is fully added to the control. Can we do without?
            // Thread.Sleep(10); // Can't use this anyways since CollectionChanged is called every time a message is added to the ObservableCollection
            Dispatcher.UIThread.Invoke(Scroll.ScrollToEnd);
        }; 
    }
}

public class MessageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (targetType != typeof(string) || value is not Message message)
            throw new NotSupportedException();
        
        var author = message.GetBestAuthorName();
        return message.Type switch
        {
            THREAD_CREATED => $"{author} created thread \"{message.Content}\"",
            CALL => $"{author} started voice message",
            _ => $"{author}: {message.Content}",
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
