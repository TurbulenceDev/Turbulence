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
    private readonly ICache _cache = Ioc.Default.GetService<ICache>()!;

    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Embed embed)
            throw new Exception("Not a EmbedImage.");
        
        // Debug image
        if (Design.IsDesignMode)
        {
            return new Bitmap(AssetLoader.Open(new Uri("resm:Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png?assembly=Avalonia.Skia"))).CreateScaledBitmap(new(180,180));
        }

        LibVLCSharp.Shared.Core.Initialize();
        var libVLC = new LibVLC(
            enableDebugLogs: true,
            "--input-repeat=2"
        );
        libVLC.Log += (sender, args) => Console.WriteLine(args.Message);
        var mediaPlayer = new MediaPlayer(libVLC);

        using var media = new Media(libVLC, new Uri(embed.Video!.Url!.AbsoluteUri));
        mediaPlayer.Play(media);
        media.Dispose();

        return mediaPlayer;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}