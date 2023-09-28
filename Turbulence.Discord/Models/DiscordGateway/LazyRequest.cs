using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;
// https://luna.gitlab.io/discord-unofficial-docs/lazy_guilds.html#op-14-lazy-request
public record LazyRequest
{
    /// <summary>
    /// the guild id for the request
    /// </summary>
    [JsonPropertyName("guild_id")]
    public required Snowflake Guild { get; init; }

    /// <summary>
    /// Parent channel IDs whose threads are being synced. If omitted, then threads are synced for the entire guild.
    /// </summary>
    [JsonPropertyName("channels")]
    public dynamic? Channels { get; init; }

    /// <summary>
    /// Unknown
    /// </summary>
    [JsonPropertyName("members")]
    public bool? Members { get; init; }

    /// <summary>
    /// Unknown
    /// </summary>
    [JsonPropertyName("typing")]
    public required bool Typing { get; init; }

    /// <summary>
    /// Unknown
    /// </summary>
    [JsonPropertyName("activities")]
    public required bool Activities { get; init; }

    /// <summary>
    /// Unknown
    /// </summary>
    [JsonPropertyName("threads")]
    public required bool Threads { get; init; }
}
