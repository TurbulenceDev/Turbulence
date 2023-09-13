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

    private static ulong Counter = 0;
    // https://discord.com/developers/docs/reference#snowflakes-snowflake-id-format-structure-left-to-right
    public static Snowflake Now()
    {
        var now = DateTimeOffset.UtcNow;
        var millis = (ulong)now.ToUnixTimeMilliseconds();
        var epoch = millis - DiscordEpoch;
        var snowflake = epoch << 22;
        // could also add the worker and process ids here
        snowflake |= Counter++ % 4096; // add the increment
        return new Snowflake(snowflake);
    }

    public override string ToString() => Id.ToString();
}