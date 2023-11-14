using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Services;

public interface ICache
{
    public string GetLocalFilePath(string url);
    
    public byte[]? GetAvatar(Snowflake userId, int size);
    public byte[]? GetEmoji(Snowflake emojiId, int size);

    public void SetAvatar(Snowflake userId, int size, byte[] avatar);
    public void SetEmoji(Snowflake emojiId, int size, byte[] emoji);
}

public class Cache : ICache, IDisposable
{
    private readonly Dictionary<(Snowflake, int), byte[]> _avatars = new();
    private readonly Dictionary<(Snowflake, int), byte[]> _emojis = new();
    private readonly Dictionary<string, string> _cachedFiles = new();

    public string GetLocalFilePath(string url)
    {
        if (_cachedFiles.TryGetValue(url, out var path)) return path;
        
        var client = new HttpClient();
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        var response = client.Send(req);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiException($"Got CDN Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        var writeStream = File.OpenWrite(path);
        response.Content.ReadAsStream().CopyTo(writeStream);
        _cachedFiles[url] = path;

        return path;
    }

    public byte[]? GetAvatar(Snowflake userId, int size)
    {
        return _avatars.TryGetValue((userId, size), out var avatar) ? avatar : null;
    }
    public byte[]? GetEmoji(Snowflake emojiId, int size)
    {
        return _emojis.TryGetValue((emojiId, size), out var emoji) ? emoji : null;
    }

    public void SetAvatar(Snowflake userId, int size, byte[] avatar)
    {
        _avatars[(userId, size)] = avatar;
    }
    public void SetEmoji(Snowflake emojiId, int size, byte[] emoji)
    {
        _avatars[(emojiId, size)] = emoji;
    }

    public void Dispose()
    {
        foreach (var (_, file) in _cachedFiles)
        {
            File.Delete(file);
        }
    }
}