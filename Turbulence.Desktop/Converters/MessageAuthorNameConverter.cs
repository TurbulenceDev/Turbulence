using Avalonia.Data.Converters;
using System.Globalization;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Converters;

public class MessageAuthorNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Message msg)
            throw new Exception("Not a message.");

        return msg.GetBestAuthorName();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
