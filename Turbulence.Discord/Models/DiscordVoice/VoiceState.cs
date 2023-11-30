using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Discord.Models.DiscordVoice;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/voice">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Voice.md">GitHub</a>.
/// </summary>
public record VoiceState {
	/// <summary>
	/// The guild ID this voice state is for.
	/// </summary>
	[JsonPropertyName("guild_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? GuildId { get; init; }

	/// <summary>
	/// The channel ID this user is connected to.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required Snowflake? ChannelId { get; init; }

	/// <summary>
	/// The user ID this voice state is for.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Snowflake UserId { get; init; }

	/// <summary>
	/// The guild member this voice state is for.
	/// </summary>
	[JsonPropertyName("member")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public GuildMember? Member { get; init; }

	/// <summary>
	/// The session ID for this voice state.
	/// </summary>
	[JsonPropertyName("session_id")]
	public required string SessionId { get; init; }

	/// <summary>
	/// Whether this user is deafened by the server.
	/// </summary>
	[JsonPropertyName("deaf")]
	public required bool Deaf { get; init; }

	/// <summary>
	/// Whether this user is muted by the server.
	/// </summary>
	[JsonPropertyName("mute")]
	public required bool Mute { get; init; }

	/// <summary>
	/// Whether this user is locally deafened.
	/// </summary>
	[JsonPropertyName("self_deaf")]
	public required bool SelfDeaf { get; init; }

	/// <summary>
	/// Whether this user is locally muted.
	/// </summary>
	[JsonPropertyName("self_mute")]
	public required bool SelfMute { get; init; }

	/// <summary>
	/// Whether this user is streaming using "Go Live".
	/// </summary>
	[JsonPropertyName("self_stream")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? SelfStream { get; init; }

	/// <summary>
	/// Whether this user's camera is enabled.
	/// </summary>
	[JsonPropertyName("self_video")]
	public required bool SelfVideo { get; init; }

	/// <summary>
	/// Whether this user's permission to speak is denied.
	/// </summary>
	[JsonPropertyName("suppress")]
	public required bool Suppress { get; init; }

	/// <summary>
	/// The time at which the user requested to speak.
	/// </summary>
	[JsonPropertyName("request_to_speak_timestamp")]
	public required DateTimeOffset? RequestToSpeakTimestamp { get; init; }
}
