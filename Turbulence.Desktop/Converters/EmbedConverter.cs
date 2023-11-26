using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.DependencyInjection;
using LibVLCSharp.Shared;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Services;

namespace Turbulence.Desktop.Converters;

public class EmbedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Embed embed)
            throw new Exception("Not a EmbedImage.");
        
        // Debug image
        if (Design.IsDesignMode)
        {
            return new Uri("https://media.tenor.com/rIZ4kijzR18AAAPo/turbulence.mp4");
        }
        
        return new Uri(embed.Video!.Url!.AbsoluteUri);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}