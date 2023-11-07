using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using System.Globalization;
using CommunityToolkit.Mvvm.DependencyInjection;
using Turbulence.Discord;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Turbulence.Discord.Models.DiscordEmoji;

namespace Turbulence.Desktop.Converters;

public class EmojiImageConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Emoji emoji)
            throw new Exception("Not a emoji.");

        // Debug image
        if (Design.IsDesignMode)
        {
            return new Bitmap(AssetLoader.Open(new Uri("resm:Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png?assembly=Avalonia.Skia"))).CreateScaledBitmap(new(32,32));
        }
        var data = Task.Run(async () => await _client.GetEmojiAsync(emoji, 32)).Result;
        if (data.Length == 0)
            return null;

        var bmp = new Bitmap(new MemoryStream(data));
        if (bmp.PixelSize.Height > 32)
        {
            bmp = bmp.CreateScaledBitmap(new PixelSize(32, 32));
        }

        return bmp;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
