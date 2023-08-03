using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-buttons">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-buttons">
/// GitHub</a>.
/// </summary>
public record ActivityButton {
	/// <summary>
	/// Text shown on the button (1-32 characters).
	/// </summary>
	[JsonPropertyName("label")]
	public required string Label { get; init; }

	/// <summary>
	/// URL opened when clicking the button (1-512 characters).
	/// </summary>
	[JsonPropertyName("url")]
	public required Uri Url { get; init; }
}
