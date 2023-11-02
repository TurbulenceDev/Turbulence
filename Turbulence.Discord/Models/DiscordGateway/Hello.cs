using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// <b>Source:</b> <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>, <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API</a>
/// </summary>
public record Hello {
	/// <summary>
	/// Interval (in milliseconds) an app should heartbeat with.
	/// </summary>
	[JsonPropertyName("heartbeat_interval")]
	public required int HeartbeatInterval { get; init; }
}
