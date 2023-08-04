using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-assets">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-assets">GitHub
/// </a>.
/// </summary>
public record ActivityAsset {
	/// <summary>
	/// See <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image">
	/// Activity Asset Image</a>.
	/// </summary>
	[JsonPropertyName("large_image")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? LargeImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the large image of the activity.
	/// </summary>
	[JsonPropertyName("large_text")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? LargeText { get; init; }

	/// <summary>
	/// See <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image">
	/// Activity Asset Image</a>.
	/// </summary>
	[JsonPropertyName("small_image")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? SmallImage { get; init; }

	/// <summary>
	/// Text displayed when hovering over the small image of the activity.
	/// </summary>
	[JsonPropertyName("small_text")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? SmallText { get; init; }
}
