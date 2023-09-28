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
    private readonly MessagesViewModel _vm;

    public MessagesView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = Design.GetDataContext(this);
        }
        _vm = (MessagesViewModel)DataContext!;

        _vm.CurrentMessages.CollectionChanged += (_, _) =>
        {
            // TODO: This whole thing could use improvement
            // TODO: Is Dispatcher.UIThread.Invoke really necessary?
            Dispatcher.UIThread.Invoke(() =>
            {
                if (Math.Abs(Scroll.Offset.Y - Scroll.ScrollBarMaximum.Y) < 0.05)
                    Scroll.ScrollToEnd();
                else
                    Scroll.LineDown();
            });
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
