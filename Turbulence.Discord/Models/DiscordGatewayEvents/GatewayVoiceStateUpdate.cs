using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGatewayEvents;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>.
/// </summary>
public record GatewayVoiceStateUpdate {
	/// <summary>
	/// ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Snowflake? GuildId { get; init; }

	/// <summary>
	/// ID of the voice channel client wants to join (null if disconnecting).
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Snowflake? ChannelId { get; init; }

	/// <summary>
	/// Whether the client is muted.
	/// </summary>
	[JsonPropertyName("self_mute")]
	public required bool SelfMute { get; init; }

	/// <summary>
	/// Whether the client deafened.
	/// </summary>
	[JsonPropertyName("self_deaf")]
	public required bool SelfDeaf { get; init; }

    [JsonPropertyName("self_video")]
    public required bool SelfVideo { get; init; }
}
