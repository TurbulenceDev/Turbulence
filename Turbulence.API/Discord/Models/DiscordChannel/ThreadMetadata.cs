using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// The thread metadata object contains a number of thread-specific channel fields that are not needed by other channel
/// types.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#thread-metadata-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#thread-metadata-object">
/// GitHub</a>.
/// </summary>
public record ThreadMetadata {
	/// <summary>
	/// Whether the thread is archived.
	/// </summary>
	[JsonPropertyName("archived")]
	public required bool Archived { get; init; }

	/// <summary>
	/// The thread will stop showing in the channel list after this many minutes of inactivity, can be set to: 60, 1440,
	/// 4320, 10080.
	/// </summary>
	[JsonPropertyName("auto_archive_duration")]
	public required int AutoArchiveDuration { get; init; }

	// TODO: Deserialize ISO8601 into something useful
	/// <summary>
	/// Timestamp when the thread's archive status was last changed, used for calculating recent activity.
	/// </summary>
	[JsonPropertyName("archive_timestamp")]
	public required string ArchiveTimestamp { get; init; }

	/// <summary>
	/// Whether the thread is locked; when a thread is locked, only users with <c>MANAGE_THREADS</c> can unarchive it.
	/// </summary>
	[JsonPropertyName("locked")]
	public required bool Locked { get; init; }

	/// <summary>
	/// Whether non-moderators can add other non-moderators to a thread; only available on private threads.
	/// </summary>
	[JsonPropertyName("invitable")]
	public bool? Invitable { get; init; }

	// TODO: Deserialize ISO8601 into something useful
	/// <summary>
	/// Timestamp when the thread was created; only populated for threads created after 2022-01-09.
	/// </summary>
	[JsonPropertyName("create_timestamp")]
	public string? CreateTimestamp { get; init; }
}
