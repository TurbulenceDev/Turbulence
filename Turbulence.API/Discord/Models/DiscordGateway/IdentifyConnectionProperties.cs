using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-connection-properties">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#identify-connection-properties">
/// GitHub</a>.
/// </summary>
public record IdentifyConnectionProperties {
	/// <summary>
	/// Your operating system.
	/// </summary>
	[JsonPropertyName("os")]
	public required string Os { get; init; } // TODO: Enum maybe

	// TODO: Improve description
	/// <summary>
	/// Your library name.
	/// </summary>
	[JsonPropertyName("browser")]
	public required string Browser { get; init; }

	// TODO: Improve description
	/// <summary>
	/// Your library name.
	/// </summary>
	[JsonPropertyName("device")]
	public required string Device { get; init; }
}
