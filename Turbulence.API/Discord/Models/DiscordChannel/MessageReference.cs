using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the
/// <a href="https://discord.com/developers/docs/resources/channel#message-reference-object-message-reference-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#message-reference-structure">
/// GitHub</a>.
/// </summary>
public record MessageReference {
	/// <summary>
	/// Snowflake ID of the originating message.
	/// </summary>
	[JsonPropertyName("message_id")]
	public ulong? MessageId { get; init; }

	/// <summary>
	/// Snowflake ID of the originating message's channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required ulong ChannelId { get; init; }

	/// <summary>
	/// Snowflake ID of the originating message's guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public ulong? GuildId { get; init; }

	/// <summary>
	/// When sending, whether to error if the referenced message doesn't exist instead of sending as a normal
	/// (non-reply) message, default true.
	/// </summary>
	[JsonPropertyName("fail_if_not_exists")]
	public bool? FailIfNotExists { get; init; }
}
