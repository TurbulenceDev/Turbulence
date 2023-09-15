using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// A thread member object contains information about a user that has joined a thread.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#thread-member-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#thread-member-object">
/// GitHub</a>.
/// </summary>
public record ThreadMember {
	/// <summary>
	/// Snowflake ID of the thread.
	///
	/// This field is omitted on the member sent within each thread in the GUILD_CREATE event.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// Snowflake ID of the user.
	///
	/// This field is omitted on the member sent within each thread in the GUILD_CREATE event.
	/// </summary>
	[JsonPropertyName("user_id")]
	public required Snowflake UserId { get; init; }

	/// <summary>
	/// Time the user last joined the thread.
	/// </summary>
	[JsonPropertyName("join_timestamp")]
	public required DateTimeOffset JoinTimestamp { get; init; }

	/// <summary>
	/// Any user-thread settings, currently only used for notifications.
	/// </summary>
	[JsonPropertyName("flags")]
	public required int Flags { get; init; }

	// TODO: Set <see cref=""> for with_member
	/// <summary>
	/// Additional information about the user.
	///
	/// This field is omitted on the member sent within each thread in the GUILD_CREATE event.
	/// This field is only present when <c>with_member</c> is set to <c>true</c> when calling <c>List Thread Members
	/// </c> or <c>Get Thread Member</c>.
	/// </summary>
	[JsonPropertyName("member")]
	public required GuildMember Member { get; init; }
}
