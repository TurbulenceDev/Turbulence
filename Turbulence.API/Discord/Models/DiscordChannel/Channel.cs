using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// Represents a guild or DM channel within Discord.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#channel-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#channel-object">GitHub
/// </a>.
/// </summary>
public record Channel {
	/// <summary>
	/// The snowflake ID of this channel.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong Id { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/channel#channel-object-channel-types">type of channel
	/// </a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	/// <summary>
	/// The snowflake ID of the guild (may be missing for some channel objects received over gateway guild dispatches).
	/// </summary>
	[JsonPropertyName("guild_id")]
	public ulong? GuildId { get; init; }

	/// <summary>
	/// Sorting position of the channel.
	/// </summary>
	[JsonPropertyName("position")]
	public int? Position { get; init; }

	/// <summary>
	/// Explicit permission overwrites for members and roles.
	/// </summary>
	[JsonPropertyName("permission_overwrites")]
	public Overwrite[]? PermissionOverwrites { get; init; }

	/// <summary>
	/// The name of the channel (1-100 characters).
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// The channel topic (0-4096 characters for <c>GUILD_FORUM</c> and <c>GUILD_MEDIA</c> channels, 0-1024 characters
	/// for all others).
	/// </summary>
	[JsonPropertyName("topic")]
	public string? Topic { get; init; }

	/// <summary>
	/// Whether the channel is NSFW.
	/// </summary>
	[JsonPropertyName("nsfw")]
	public bool? Nsfw { get; init; }

	/// <summary>
	/// The snowflake ID of the last message sent in this channel (or thread for <c>GUILD_FORUM</c> or <c>GUILD_MEDIA
	/// </c> channels) (may not point to an existing or valid message or thread).
	/// </summary>
	[JsonPropertyName("last_message_id")]
	public ulong? LastMessageId { get; init; }

	/// <summary>
	/// The bitrate (in bits) of the voice channel.
	/// </summary>
	[JsonPropertyName("bitrate")]
	public int? Bitrate { get; init; }

	/// <summary>
	/// The user limit of the voice channel.
	/// </summary>
	[JsonPropertyName("user_limit")]
	public int? UserLimit { get; init; }

	/// <summary>
	/// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the
	/// permission <c>manage_messages</c> or <c>manage_channel</c>, are unaffected.
	/// </summary>
	[JsonPropertyName("rate_limit_per_user")]
	public required int RateLimitPerUser { get; init; }

	/// <summary>
	/// The recipients of the DM.
	/// </summary>
	[JsonPropertyName("recipients")]
	public User[]? Recipients { get; init; }

	/// <summary>
	/// Icon hash of the group DM.
	/// </summary>
	[JsonPropertyName("icon")]
	public string? Icon { get; init; }

	/// <summary>
	/// Snowflake ID of the creator of the group DM or thread.
	/// </summary>
	[JsonPropertyName("owner_id")]
	public ulong? OwnerId { get; init; }

	/// <summary>
	/// Application ID of the group DM creator if it is bot-created.
	/// </summary>
	[JsonPropertyName("application_id")]
	public ulong? ApplicationId { get; init; }

	/// <summary>
	/// For group DM channels: whether the channel is managed by an application via the <c>gdm.join</c> OAuth2 scope.
	/// </summary>
	[JsonPropertyName("managed")]
	public bool? Managed { get; init; }

	/// <summary>
	/// For guild channels: snowflake ID of the parent category for a channel (each parent category can contain up to 50
	/// channels), for threads: snowflake ID of the text channel this thread was created.
	/// </summary>
	[JsonPropertyName("parent_id")]
	public ulong? ParentId { get; init; }

	/// <summary>
	/// When the last pinned message was pinned. This may be <c>null</c> in events such as <c>GUILD_CREATE</c> when a
	/// message is not pinned.
	/// </summary>
	[JsonPropertyName("last_pin_timestamp")]
	public string? LastPinTimestamp { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/voice#voice-region-object">Voice region</a> ID for the
	/// voice channel, automatic when set to null.
	/// </summary>
	[JsonPropertyName("rtc_region")]
	public string? RtcRegion { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The camera <a href="https://discord.com/developers/docs/resources/channel#channel-object-video-quality-modes">
	/// video quality mode</a> of the voice channel, 1 when not present.
	/// </summary>
	[JsonPropertyName("video_quality_mode")]
	public int? VideoQualityMode { get; init; }

	/// <summary>
	/// Number of messages (not including the initial message or deleted messages) in a thread.
	/// </summary>
	[JsonPropertyName("message_count")]
	public required int MessageCount { get; init; }

	/// <summary>
	/// An approximate count of users in a thread, stops counting at 50.
	/// </summary>
	[JsonPropertyName("member_count")]
	public int? MemberCount { get; init; }

	/// <summary>
	/// Thread-specific fields not needed by other channels.
	/// </summary>
	[JsonPropertyName("thread_metadata")]
	public ThreadMetadata? ThreadMetadata { get; init; }

	/// <summary>
	/// Thread member object for the current user, if they have joined the thread, only included on certain API
	/// endpoints.
	/// </summary>
	[JsonPropertyName("member")]
	public ThreadMember? Member { get; init; }

	/// <summary>
	/// Default duration, copied onto newly created threads, in minutes, threads will stop showing in the channel list
	/// after the specified period of inactivity, can be set to: 60, 1440, 4320, 10080.
	/// </summary>
	[JsonPropertyName("default_auto_archive_duration")]
	public int? DefaultAutoArchiveDuration { get; init; }

	/// <summary>
	/// Computed permissions for the invoking user in the channel, including overwrites, only included when part of the
	/// <c>resolved</c> data received on a slash command interaction.
	/// </summary>
	[JsonPropertyName("permissions")]
	public string? Permissions { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#channel-object-channel-flags">Channel flags</a>
	/// combined as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	public int? Flags { get; init; }

	/// <summary>
	/// Number of messages ever sent in a thread, it's similar to <see cref="MessageCount"/> on message creation, but
	/// will not decrement the number when a message is deleted.
	/// </summary>
	[JsonPropertyName("total_message_sent")]
	public int? TotalMessageSent { get; init; }

	/// <summary>
	/// The set of tags that can be used in a <c>GUILD_FORUM</c> or a <c>GUILD_MEDIA</c> channel.
	/// </summary>
	[JsonPropertyName("available_tags")]
	public ForumTag[]? AvailableTags { get; init; }

	/// <summary>
	/// The snowflake IDs of the set of tags that have been applied to a thread in a <c>GUILD_FORUM</c> or a
	/// <c>GUILD_MEDIA</c> channel.
	/// </summary>
	[JsonPropertyName("applied_tags")]
	public ulong[]? AppliedTags { get; init; }

	/// <summary>
	/// The emoji to show in the add reaction button on a thread in a <c>GUILD_FORUM</c> or a <c>GUILD_MEDIA</c>
	/// channel.
	/// </summary>
	[JsonPropertyName("default_reaction_emoji")]
	public DefaultReaction? DefaultReactionEmoji { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The initial <see cref="RateLimitPerUser"/> to set on newly created threads in a channel. This field is copied to
	/// the thread at creation time and does not live update.
	/// </summary>
	[JsonPropertyName("default_thread_rate_limit_per_user")]
	public int? DefaultThreadRateLimitPerUser { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/channel#channel-object-sort-order-types">default sort
	/// order type</a> used to order posts in <c>GUILD_FORUM</c> and <c>GUILD_MEDIA</c> channels. Defaults to
	/// <c>null</c>, which indicates a preferred sort order hasn't been set by a channel admin.
	/// </summary>
	[JsonPropertyName("default_sort_order")]
	public int? DefaultSortOrder { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/channel#channel-object-forum-layout-types">default
	/// forum layout view</a> used to display posts in <c>GUILD_FORUM</c> channels. Defaults to <c>0</c>, which
	/// indicates a layout view has not been set by a channel admin.
	/// </summary>
	[JsonPropertyName("default_forum_layout")]
	public int? DefaultForumLayout { get; init; }
}
