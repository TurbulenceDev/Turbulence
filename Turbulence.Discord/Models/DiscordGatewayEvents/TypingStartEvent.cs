using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Discord.Models.DiscordGatewayEvents;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>.
/// </summary>
public record TypingStartEvent {
	/// <summary>
	/// ID of the channel.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Snowflake ChannelId { get; init; }

	/// <summary>
	/// ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? GuildId { get; init; }

	/// <summary>
	/// ID of the user.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Snowflake UserId { get; init; }

	/// <summary>
	/// Unix time (in seconds) of when the user started typing.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public required int Timestamp { get; init; }

	/// <summary>
	/// Member who started typing if this happened in a guild.
	/// </summary>
	[JsonPropertyName("member")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public GuildMember? Member { get; init; }
}
