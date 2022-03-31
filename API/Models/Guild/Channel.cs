using Newtonsoft.Json;
using Turbulence.API.Models.DiscordUser;

namespace Turbulence.API.Models.Guild;

public class Channel
{
    /// <summary>
    /// The id of this channel
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// The type of channel
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; set; }

    /// <summary>
    /// The id of the guild (may be missing for some channel objects received over gateway guild dispatches)
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Sorting position of the channel
    /// </summary>
    [JsonProperty("position", Required = Required.DisallowNull)]
    public int Position { get; set; }

    /// <summary>
    /// Explicit permission overwrites for members and roles
    /// </summary>
    [JsonProperty("permission_overwrites", Required = Required.DisallowNull)]
    public Overwrite[] PermissionOverwrites { get; set; } = null!;

    /// <summary>
    /// The name of the channel (1-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.DisallowNull)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The channel topic (0-1024 characters)
    /// </summary>
    [JsonProperty("topic")]
    public string? Topic { get; set; }

    /// <summary>
    /// Whether the channel is NSFW
    /// </summary>
    [JsonProperty("nsfw", Required = Required.DisallowNull)]
    public bool Nsfw { get; set; }

    /// <summary>
    /// The id of the last message sent in this channel (may not point to an existing or valid message)
    /// </summary>
    [JsonProperty("last_message_id")]
    public ulong? LastMessageId { get; set; }

    /// <summary>
    /// The bitrate (in bits) of the voice channel
    /// </summary>
    [JsonProperty("bitrate", Required = Required.DisallowNull)]
    public int Bitrate { get; set; }

    /// <summary>
    /// The user limit of the voice channel
    /// </summary>
    [JsonProperty("user_limit", Required = Required.DisallowNull)]
    public int UserLimit { get; set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected
    /// </summary>
    [JsonProperty("rate_limit_per_user", Required = Required.Always)]
    public int RateLimitPerUser { get; set; }

    /// <summary>
    /// The recipients of the DM
    /// </summary>
    [JsonProperty("recipients", Required = Required.DisallowNull)]
    public User[] Recipients { get; set; } = null!;

    /// <summary>
    /// Icon hash
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Id of the creator of the group dm or thread
    /// </summary>
    [JsonProperty("owner_id", Required = Required.DisallowNull)]
    public ulong OwnerId { get; set; }

    /// <summary>
    /// Application id of the group dm creator if it is bot-created
    /// </summary>
    [JsonProperty("application_id", Required = Required.DisallowNull)]
    public ulong ApplicationId { get; set; }

    /// <summary>
    /// For guild channels: id of the parent category for a channel (each parent
    /// category can contain up to 50 channels), for threads: id of the text
    /// channel this thread was created
    /// </summary>
    [JsonProperty("parent_id")]
    public ulong? ParentId { get; set; }

    /// <summary>
    /// When the last pinned message was pinned. this may be null in events such as guild_create when a message is not pinned. ISO8601 timestamp
    /// </summary>
    [JsonProperty("last_pin_timestamp")]
    public string? LastPinTimestamp { get; set; }

    /// <summary>
    /// Voice region id for the voice channel, automatic when set to null
    /// </summary>
    [JsonProperty("rtc_region")]
    public string? RtcRegion { get; set; }

    /// <summary>
    /// The camera video quality mode of the voice channel, 1 when not present
    /// </summary>
    [JsonProperty("video_quality_mode", Required = Required.DisallowNull)]
    public int VideoQualityMode { get; set; }

    /// <summary>
    /// An approximate count of messages in a thread, stops counting at 50
    /// </summary>
    [JsonProperty("message_count", Required = Required.DisallowNull)]
    public int MessageCount { get; set; }

    /// <summary>
    /// An approximate count of users in a thread, stops counting at 50
    /// </summary>
    [JsonProperty("member_count", Required = Required.DisallowNull)]
    public int MemberCount { get; set; }

    /// <summary>
    /// Thread-specific fields not needed by other channels
    /// </summary>
    [JsonProperty("thread_metadata", Required = Required.DisallowNull)]
    public ThreadMetadata ThreadMetadata { get; set; } = null!;

    /// <summary>
    /// Thread member object for the current user, if they have joined the thread, only included on certain api endpoints
    /// </summary>
    [JsonProperty("member", Required = Required.DisallowNull)]
    public ThreadMember Member { get; set; } = null!;

    /// <summary>
    /// Default duration that the clients (not the api) will use for newly created
    /// threads, in minutes, to automatically archive the thread after recent
    /// activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("default_auto_archive_duration", Required = Required.DisallowNull)]
    public int DefaultAutoArchiveDuration { get; set; }

    /// <summary>
    /// Computed permissions for the invoking user in the channel, including overwrites, only included when part of the resolved data received on a slash command interaction
    /// </summary>
    [JsonProperty("permissions", Required = Required.DisallowNull)]
    public string Permissions { get; set; } = null!;


}