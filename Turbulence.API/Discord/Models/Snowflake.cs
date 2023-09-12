using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models;

[JsonConverter(typeof(SnowflakeConverter))]
public record Snowflake(ulong Id)
{
    public DateTimeOffset Timestamp { get; init; } = GetTimestamp(Id);
    public const ulong DiscordEpoch = 1420070400000;

    public static implicit operator ulong(Snowflake snowflake) => snowflake.Id;
    
    public static DateTimeOffset GetTimestamp(ulong id)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds((long)((id >> 22) + DiscordEpoch));
    }

    public override string ToString() => Id.ToString();
}