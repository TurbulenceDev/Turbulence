using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#channel-mention-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#channel-mention-object">
/// GitHub</a>.
/// </summary>
public record ChannelMention {
	/// <summary>
	/// Snowflake ID of the channel.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// Snowflake ID of the guild containing the channel.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Snowflake GuildId { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/channel#channel-object-channel-types">type of channel
	/// </a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	/// <summary>
	/// The name of the channel.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }
}
