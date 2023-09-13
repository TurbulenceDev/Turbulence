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
    // https://github.com/uowuo/abaddon/blob/master/src/discord/snowflake.cpp#L29
    //TODO: figure out whereever this comes from
    public static Snowflake FromNow()
    {
        var now = DateTimeOffset.UtcNow;
        var millis = (ulong)now.ToUnixTimeMilliseconds();
        var epoch = millis - DiscordEpoch;
        ulong snowflake = epoch << 22;
        snowflake |= Counter++ % 4096;
        return new Snowflake(snowflake);
    }

    public override string ToString() => Id.ToString();
}