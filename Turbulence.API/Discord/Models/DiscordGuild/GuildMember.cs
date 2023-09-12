using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordGuild;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/guild#guild-member-object">Discord API documentation
/// </a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Guild.md#guild-member-object">
/// GitHub</a>.
/// </summary>
public record GuildMember {
	/// <summary>
	/// The user this guild member represents.
	/// </summary>
	[JsonPropertyName("user")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public User? User { get; init; }

	/// <summary>
	/// This user's guild nickname.
	/// </summary>
	[JsonPropertyName("nick")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Nick { get; init; }

	/// <summary>
	/// The member's <a href="https://discord.com/developers/docs/reference#image-formatting">guild avatar hash</a>.
	/// </summary>
	[JsonPropertyName("avatar")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Avatar { get; init; }

	/// <summary>
	/// Array of <a href="https://discord.com/developers/docs/topics/permissions#role-object">role</a> object IDs.
	/// </summary>
	[JsonPropertyName("roles")]
	public required Snowflake[] Roles { get; init; }

	/// <summary>
	/// When the user joined the guild.
	/// </summary>
	[JsonPropertyName("joined_at")]
	public required string JoinedAt { get; init; }

	/// <summary>
	/// When the user started <a href="https://support.discord.com/hc/en-us/articles/360028038352-Server-Boosting-">
	/// boosting</a> the guild.
	/// </summary>
	[JsonPropertyName("premium_since")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? PremiumSince { get; init; }

	/// <summary>
	/// Whether the user is deafened in voice channels.
	/// </summary>
	[JsonPropertyName("deaf")]
	public required bool Deaf { get; init; }

	/// <summary>
	/// Whether the user is muted in voice channels.
	/// </summary>
	[JsonPropertyName("mute")]
	public required bool Mute { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-member-object-guild-member-flags">Guild
	/// member flags</a> represented as a bit set, defaults to <c>0</c>.
	/// </summary>
	[JsonPropertyName("flags")]
	public required int Flags { get; init; }

	/// <summary>
	/// Whether the user has not yet passed the guild's
	/// <a href="https://discord.com/developers/docs/resources/guild#membership-screening-object">Membership Screening
	/// </a> requirements.
	/// </summary>
	[JsonPropertyName("pending")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Pending { get; init; }

	/// <summary>
	/// Total permissions of the member in the channel, including overwrites, returned when in the interaction object.
	/// </summary>
	[JsonPropertyName("permissions")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Permissions { get; init; }

	/// <summary>
	/// When the user's <a href="https://support.discord.com/hc/en-us/articles/4413305239191-Time-Out-FAQ">timeout</a>
	/// will expire and the user will be able to communicate in the guild again, null or a time in the past if the user
	/// is not timed out.
	/// </summary>
	[JsonPropertyName("communication_disabled_until")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public DateTimeOffset? CommunicationDisabledUntil { get; init; }
}
