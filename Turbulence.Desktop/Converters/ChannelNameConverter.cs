using Avalonia.Controls;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.Globalization;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Converters;

public class ChannelNameConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Channel channel)
            return null;
        
        // design exception so we dont api call
        if (Design.IsDesignMode)
        {
            return channel.Name;
        }

        return Task.Run(() => _client.GetChannelName(channel)).Result;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
