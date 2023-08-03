using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-emoji">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-emoji">
/// GitHub</a>.
/// </summary>
public record ActivityEmoji {
	/// <summary>
	/// Name of the emoji.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// Snowflake ID of the emoji.
	/// </summary>
	[JsonPropertyName("id")]
	public ulong? Id { get; init; }

	/// <summary>
	/// Whether the emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public bool? Animated { get; init; }
}
