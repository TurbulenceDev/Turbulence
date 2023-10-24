using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGatewayEvents;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>.
/// </summary>
public record MessageDeleteEvent {
	/// <summary>
	/// ID of the message.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

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
}
