using System.Text.Json.Serialization;
using System.Text.Json;

namespace Turbulence.Discord.Models;

[JsonConverter(typeof(SnowflakeConverter))]
public record Snowflake(ulong Id)
{
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.FromUnixTimeMilliseconds((long)((Id >> 22) + DiscordEpoch));
    public const ulong DiscordEpoch = 1420070400000;

    public static implicit operator ulong(Snowflake snowflake) => snowflake.Id;

    private static ulong _counter = 0;

    public static Snowflake Now()
    {
        // https://discord.com/developers/docs/reference#snowflakes-snowflake-id-format-structure-left-to-right
        var now = DateTimeOffset.UtcNow;
        var millis = (ulong)now.ToUnixTimeMilliseconds();
        var epoch = millis - DiscordEpoch;
        var snowflake = epoch << 22;
        // could also add the worker and process ids here
        snowflake |= _counter++ % 4096; // add the increment
        return new Snowflake(snowflake);
    }

    public override string ToString() => Id.ToString();

    private class SnowflakeConverter : JsonConverter<Snowflake>
    {
        public override Snowflake Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions _)
        {
            if (ulong.TryParse(reader.GetString(), out var id))
                return new Snowflake(id);

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Snowflake snowflake, JsonSerializerOptions _)
        {
            writer.WriteStringValue(snowflake.Id.ToString());
        }
    }
}
