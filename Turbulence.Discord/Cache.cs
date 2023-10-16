using Turbulence.Discord.Models;

namespace Turbulence.Discord;

public interface ICache
{
    public Image? GetAvatar(Snowflake userId);
}

public class Cache : ICache
{
    public Image? GetAvatar(Snowflake userId)
    {
        return null;
    }
}

public record Image(byte[] Data, int Size);
