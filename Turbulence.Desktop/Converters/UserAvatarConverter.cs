using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using System.Globalization;
using Turbulence.Discord.Models.DiscordUser;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace Turbulence.Desktop.Converters;

public class UserAvatarConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not User user)
            throw new Exception("Not a user.");

        // Debug image
        if (Design.IsDesignMode)
        {
            return new Bitmap(AssetLoader.Open(new Uri("resm:Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png?assembly=Avalonia.Skia")));
        }

        // TODO: use client avatar get
        if (user.Avatar is { } avatar)
        {
            return Task.Run(async () =>
                await LoadFromWeb(new Uri($"https://cdn.discordapp.com/avatars/{user.Id}/{avatar}.png?size=80"))).Result;
        }
        else
        {
            return Task.Run(async () =>
                await LoadFromWeb(new Uri($"https://cdn.discordapp.com/embed/avatars/{(user.Id >> 22) % 6}.png"))).Result!.CreateScaledBitmap(new PixelSize(80, 80));
        }
        // Image.Source = new Bitmap(new MemoryStream(await _client.GetAvatar(message.Author)));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static async Task<Bitmap?> LoadFromWeb(Uri url)
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsByteArrayAsync();
            return new Bitmap(new MemoryStream(data));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"An error occurred while downloading image '{url}': {ex.Message}");
            return null;
        }
    }
}
