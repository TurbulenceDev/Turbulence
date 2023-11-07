using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Services;

public interface ICache
{
    public byte[]? GetAvatar(Snowflake userId, int size);
    public byte[]? GetEmoji(Snowflake emojiId, int size);

    public void SetAvatar(Snowflake userId, int size, byte[] avatar);
    public void SetEmoji(Snowflake emojiId, int size, byte[] emoji);
}

public class Cache : ICache
{
    private readonly Dictionary<(Snowflake, int), byte[]> _avatars = new();
    private readonly Dictionary<(Snowflake, int), byte[]> _emojis = new();

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
}