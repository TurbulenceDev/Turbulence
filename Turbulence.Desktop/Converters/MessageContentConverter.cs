using Avalonia.Data.Converters;
using System.Globalization;
using Turbulence.Discord.Models.DiscordChannel;
using static Turbulence.Discord.Models.DiscordChannel.MessageType;

namespace Turbulence.Desktop.Converters;

public class MessageContentConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Message message)
            return null;

        var author = message.GetBestAuthorName();
        return message.Type switch
        {
            THREAD_CREATED => $"{author} created thread \"{message.Content}\"",
            CALL => $"{author} started a voice call",
            CHANNEL_PINNED_MESSAGE => $"{author} pinned a message.",
            _ => message.Content,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
