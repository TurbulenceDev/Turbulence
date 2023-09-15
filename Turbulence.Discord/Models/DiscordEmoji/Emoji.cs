using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordPermissions;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Models.DiscordEmoji;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/emoji#emoji-object">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Emoji.md#emoji-object">GitHub</a>.
/// </summary>
public record Emoji {
	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Emoji snowflake ID</a>.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake? Id { get; init; }

	/// <summary>
	/// Emoji name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string? Name { get; init; }

	/// <summary>
	/// Roles allowed to use this emoji.
	/// </summary>
	[JsonPropertyName("roles")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Role[]? Roles { get; init; }

	/// <summary>
	/// User that created this emoji.
	/// </summary>
	[JsonPropertyName("user")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public User? User { get; init; }

	/// <summary>
	/// Whether this emoji must be wrapped in colons.
	/// </summary>
	[JsonPropertyName("require_colons")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? RequireColons { get; init; }

	/// <summary>
	/// Whether this emoji is managed.
	/// </summary>
	[JsonPropertyName("managed")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Managed { get; init; }

	/// <summary>
	/// Whether this emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Animated { get; init; }

	/// <summary>
	/// Whether this emoji can be used, may be false due to loss of Server Boosts.
	/// </summary>
	[JsonPropertyName("available")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Available { get; init; }
}
