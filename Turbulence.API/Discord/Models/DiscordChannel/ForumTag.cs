using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// An object that represents a tag that is able to be applied to a thread in a <c>GUILD_FORUM</c> or <c>GUILD_MEDIA</c>
/// channel.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#forum-tag-object">Discord API documentation
/// </a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#forum-tag-object">
/// GitHub</a>.
/// </summary>
public record ForumTag {
	/// <summary>
	/// The snowflake ID of the tag.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// The name of the tag (0-20 characters).
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// Whether this tag can only be added to or removed from threads by a member with the <c>MANAGE_THREADS</c>
	/// permission.
	/// </summary>
	[JsonPropertyName("moderated")]
	public required bool Moderated { get; init; }

	/// <summary>
	/// The snowflake ID of a guild's custom emoji.
	///
	/// Must be <c>null</c> if <see cref="EmojiName"/> is set.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public required Snowflake? EmojiId { get; init; }

	/// <summary>
	/// The Unicode character of the emoji.
	///
	/// Must be <c>null</c> if <see cref="EmojiId"/> is set.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public required string? EmojiName { get; init; }
}
