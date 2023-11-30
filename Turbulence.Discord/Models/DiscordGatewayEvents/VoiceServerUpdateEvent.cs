using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGatewayEvents;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>.
/// </summary>
public record VoiceServerUpdateEvent {
	/// <summary>
	/// Voice connection token.
	/// </summary>
	[JsonPropertyName("token")]
	public required string Token { get; init; }

	/// <summary>
	/// Guild this voice server update is for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Snowflake GuildId { get; init; }

    /// <summary>
    /// Voice server host.
    /// A null endpoint means that the voice server allocated has gone away and is trying to be reallocated. You should attempt to disconnect from the currently connected voice server, and not attempt to reconnect until a new voice server is allocated.
    /// </summary>
    [JsonPropertyName("endpoint")]
	public required string? Endpoint { get; init; }
}
