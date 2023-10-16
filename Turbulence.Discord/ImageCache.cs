using Turbulence.Discord.Models;

namespace Turbulence.Discord;

public interface ICache
{
    public object? /* idk what type this should be :( */ GetAvatar(Snowflake userId);
}

public class Cache : ICache
{
    public object? GetAvatar(Snowflake userId)
    {
        return null;
    }
}
