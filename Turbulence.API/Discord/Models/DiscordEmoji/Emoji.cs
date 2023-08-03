using Turbulence.API.Discord.Models.DiscordPermissions;
using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordEmoji;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/emoji#emoji-object">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Emoji.md#emoji-object">GitHub</a>.
/// </summary>
public record Emoji {
	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Emoji snowflake ID</a>.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong? Id { get; init; }

	/// <summary>
	/// Emoji name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string? Name { get; init; }

	/// <summary>
	/// Roles allowed to use this emoji.
	/// </summary>
	[JsonPropertyName("roles")]
	public Role[]? Roles { get; init; }

	/// <summary>
	/// User that created this emoji.
	/// </summary>
	[JsonPropertyName("user")]
	public User? User { get; init; }

	/// <summary>
	/// Whether this emoji must be wrapped in colons.
	/// </summary>
	[JsonPropertyName("require_colons")]
	public bool? RequireColons { get; init; }

	/// <summary>
	/// Whether this emoji is managed.
	/// </summary>
	[JsonPropertyName("managed")]
	public bool? Managed { get; init; }

	/// <summary>
	/// Whether this emoji is animated.
	/// </summary>
	[JsonPropertyName("animated")]
	public bool? Animated { get; init; }

	/// <summary>
	/// Whether this emoji can be used, may be false due to loss of Server Boosts.
	/// </summary>
	[JsonPropertyName("available")]
	public bool? Available { get; init; }
}
