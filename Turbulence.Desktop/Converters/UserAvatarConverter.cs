using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using System.Globalization;
using CommunityToolkit.Mvvm.DependencyInjection;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordUser;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace Turbulence.Desktop.Converters;

public class UserAvatarConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not User user)
            throw new Exception("Not a user.");

        // Debug image
        if (Design.IsDesignMode)
        {
            return new Bitmap(AssetLoader.Open(new Uri("resm:Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png?assembly=Avalonia.Skia")));
        }

        var data = Task.Run(async () => await _client.GetAvatarAsync(user, 80)).Result;
        var bmp = new Bitmap(new MemoryStream(data));
        if (bmp.PixelSize.Height > 80)
        {
            bmp = bmp.CreateScaledBitmap(new PixelSize(80, 80));
        }

        return bmp;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
