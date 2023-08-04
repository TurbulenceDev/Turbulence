using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md">GitHub</a>.
/// </summary>
public record DefaultReaction {
	/// <summary>
	/// The ID of a guild's custom emoji.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	[JsonConverter(typeof(SnowflakeConverter))]
	public required Snowflake? EmojiId { get; init; }

	/// <summary>
	/// The unicode character of the emoji.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public required string? EmojiName { get; init; }
}
