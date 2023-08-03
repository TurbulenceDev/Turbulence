using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-secrets">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-secrets">
/// GitHub</a>.
/// </summary>
public record ActivitySecrets {
	/// <summary>
	/// Secret for joining a party.
	/// </summary>
	[JsonPropertyName("join")]
	public string? Join { get; init; }

	/// <summary>
	/// Secret for spectating a game.
	/// </summary>
	[JsonPropertyName("spectate")]
	public string? Spectate { get; init; }

	/// <summary>
	/// Secret for a specific instanced match.
	/// </summary>
	[JsonPropertyName("match")]
	public string? Match { get; init; }
}
