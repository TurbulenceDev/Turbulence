using Turbulence.Discord.Models;

namespace Turbulence.Discord.Services;

public interface ICache
{
    public byte[]? GetAvatar(Snowflake userId, int size);

    public void SetAvatar(Snowflake userId, int size, byte[] avatar);
}

public class Cache : ICache
{
    private readonly Dictionary<(Snowflake, int), byte[]> _avatars = new();

    public byte[]? GetAvatar(Snowflake userId, int size)
    {
        return _avatars.TryGetValue((userId, size), out var avatar) ? avatar : null;
    }

    public void SetAvatar(Snowflake userId, int size, byte[] avatar)
    {
        _avatars[(userId, size)] = avatar;
    }
}