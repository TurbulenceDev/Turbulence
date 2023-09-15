using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway#get-gateway">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway.md#get-gateway--get-gateway">
/// GitHub</a>.
/// </summary>
public record Gateway {
    /// <summary>
    /// An object with a valid WSS URL which the app can use when
    /// <a href="https://discord.com/developers/docs/topics/gateway#connecting">Connecting</a> to the Gateway. Apps
    /// should cache this value and only call this endpoint to retrieve a new URL when they are unable to properly
    /// establish a connection using the cached one.
    /// </summary>
    [JsonPropertyName("url")]
    public required Uri Url { get; init; }
}
