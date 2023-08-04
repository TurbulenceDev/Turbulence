using System.Text.Json;
using System.Text.Json.Serialization;
using Turbulence.API.Discord.Models;

namespace Turbulence.API.Discord.JsonConverters;

public class SnowflakeConverter : JsonConverter<Snowflake>
{
    public override Snowflake Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions _)
    {
        if (ulong.TryParse(reader.GetString(), out var id))
            return new Snowflake(id);

        throw new JsonException($"Failed to convert {typeToConvert} to Snowflake");
    }

    public override void Write(Utf8JsonWriter writer, Snowflake snowflake, JsonSerializerOptions _)
    {
        writer.WriteStringValue(snowflake.Id.ToString());
    }
}