using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGuild;

/// <summary>
/// See the
/// <a href="https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-channel-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Guild.md#welcome-screen-channel-structure">
/// GitHub</a>.
/// </summary>
public record WelcomeScreenChannel {
	/// <summary>
	/// The channel's snowflake ID.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Snowflake ChannelId { get; init; }

	/// <summary>
	/// The description shown for the channel.
	/// </summary>
	[JsonPropertyName("description")]
	public required string Description { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/reference#image-formatting">emoji ID</a>, if the emoji is
	/// custom.
	/// </summary>
	[JsonPropertyName("emoji_id")]
	public required Snowflake? EmojiId { get; init; }

	/// <summary>
	/// The emoji name if custom, the unicode character if standard, or <c>null</c> if no emoji is set.
	/// </summary>
	[JsonPropertyName("emoji_name")]
	public required string? EmojiName { get; init; }
}
