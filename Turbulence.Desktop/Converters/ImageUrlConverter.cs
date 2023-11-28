using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using System.Globalization;
using Turbulence.Discord;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Turbulence.Desktop.Converters;

public class ImageUrlConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string url)
            throw new Exception("Not a string.");

        // Debug image
        if (Design.IsDesignMode)
        {
            return new Bitmap(AssetLoader.Open(new Uri("resm:Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png?assembly=Avalonia.Skia")));
        }

        var data = Task.Run(async () => await _client.GetImageAsync(url)).Result;
        var bmp = new Bitmap(new MemoryStream(data));
        /*if (bmp.PixelSize.Height > 80)
        {
            bmp = bmp.CreateScaledBitmap(new PixelSize(80, 80));
        }*/

        return bmp;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
