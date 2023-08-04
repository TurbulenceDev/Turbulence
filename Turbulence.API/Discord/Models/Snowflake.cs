namespace Turbulence.API.Discord.Models;

public record Snowflake(ulong Id)
{
    public DateTimeOffset Timestamp { get; init; } = GetTimestamp(Id);
    public const ulong DiscordEpoch = 1420070400000;

    public static implicit operator ulong(Snowflake snowflake) => snowflake.Id;
    
    public static DateTimeOffset GetTimestamp(ulong id)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds((long)((id >> 22) + DiscordEpoch));
    }
}