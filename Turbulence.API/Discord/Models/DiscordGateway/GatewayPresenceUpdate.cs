using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the
/// <a href="https://discord.com/developers/docs/topics/gateway-events#update-presence-gateway-presence-update-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#gateway-presence-update-structure">
/// GitHub</a>.
/// </summary>
public record GatewayPresenceUpdate {
	/// <summary>
	/// User's new <a href="https://discord.com/developers/docs/topics/gateway-events#update-presence-status-types">status</a>.
	/// </summary>
	[JsonPropertyName("status")]
	public required string Status { get; init; } // TODO: Enum
	
	/// <summary>
	/// Unix time (in milliseconds) of when the client went idle, or null if the client is not idle.
	/// </summary>
	[JsonPropertyName("since")]
	public required long? Since { get; init; }

	/// <summary>
	/// User's activities.
	/// </summary>
	[JsonPropertyName("activities")]
	public required Activity[] Activities { get; init; }

	/// <summary>
	/// Whether or not the client is afk.
	/// </summary>
	[JsonPropertyName("afk")]
	public required bool Afk { get; init; }
}
