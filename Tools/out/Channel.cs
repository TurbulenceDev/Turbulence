public class ChannelStructure
{
    /// <summary>
    /// The id of this channel
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The type of channel
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// The id of the guild (may be missing for some channel objects received over gateway guild dispatches)
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// Sorting position of the channel
    /// </summary>
    [JsonProperty("position?", Required = Required.Always)]
    public int Position? { get; internal set; }

    /// <summary>
    /// Explicit permission overwrites for members and roles
    /// </summary>
    [JsonProperty("permission_overwrites?", Required = Required.Always)]
    public overwrite objects[] PermissionOverwrites? { get; internal set; }

    /// <summary>
    /// The name of the channel (1-100 characters)
    /// </summary>
    [JsonProperty("name?", Required = Required.Always)]
    public string Name? { get; internal set; } = null!;

    /// <summary>
    /// The channel topic (0-1024 characters)
    /// </summary>
    [JsonProperty("topic?", Required = Required.AllowNull)]
    public string? Topic? { get; internal set; }

    /// <summary>
    /// Whether the channel is nsfw
    /// </summary>
    [JsonProperty("nsfw?", Required = Required.Always)]
    public bool Nsfw? { get; internal set; }

    /// <summary>
    /// The id of the last message sent in this channel (may not point to an existing or valid message)
    /// </summary>
    [JsonProperty("last_message_id?", Required = Required.AllowNull)]
    public ulong? LastMessageId? { get; internal set; }

    /// <summary>
    /// The bitrate (in bits) of the voice channel
    /// </summary>
    [JsonProperty("bitrate?", Required = Required.Always)]
    public int Bitrate? { get; internal set; }

    /// <summary>
    /// The user limit of the voice channel
    /// </summary>
    [JsonProperty("user_limit?", Required = Required.Always)]
    public int UserLimit? { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected
    /// </summary>
    [JsonProperty("rate_limit_per_user?*", Required = Required.Always)]
    public int RateLimitPerUser?* { get; internal set; }

    /// <summary>
    /// The recipients of the dm
    /// </summary>
    [JsonProperty("recipients?", Required = Required.Always)]
    public user objects[] Recipients? { get; internal set; }

    /// <summary>
    /// Icon hash of the group dm
    /// </summary>
    [JsonProperty("icon?", Required = Required.AllowNull)]
    public string? Icon? { get; internal set; }

    /// <summary>
    /// Id of the creator of the group dm or thread
    /// </summary>
    [JsonProperty("owner_id?", Required = Required.Always)]
    public ulong OwnerId? { get; internal set; }

    /// <summary>
    /// Application id of the group dm creator if it is bot-created
    /// </summary>
    [JsonProperty("application_id?", Required = Required.Always)]
    public ulong ApplicationId? { get; internal set; }

    /// <summary>
    /// For guild channels: id of the parent category for a channel (each parent category can contain up to 50 channels), for threads: id of the text channel this thread was created
    /// </summary>
    [JsonProperty("parent_id?", Required = Required.AllowNull)]
    public ulong? ParentId? { get; internal set; }

    /// <summary>
    /// When the last pinned message was pinned. this may be null in events such as guild_create when a message is not pinned.
    /// </summary>
    [JsonProperty("last_pin_timestamp?", Required = Required.AllowNull)]
    public ISO8601 timestamp? LastPinTimestamp? { get; internal set; }

    /// <summary>
    /// Voice region id for the voice channel, automatic when set to null
    /// </summary>
    [JsonProperty("rtc_region?", Required = Required.AllowNull)]
    public string? RtcRegion? { get; internal set; }

    /// <summary>
    /// The camera video quality mode of the voice channel, 1 when not present
    /// </summary>
    [JsonProperty("video_quality_mode?", Required = Required.Always)]
    public int VideoQualityMode? { get; internal set; }

    /// <summary>
    /// An approximate count of messages in a thread, stops counting at 50
    /// </summary>
    [JsonProperty("message_count?", Required = Required.Always)]
    public int MessageCount? { get; internal set; }

    /// <summary>
    /// An approximate count of users in a thread, stops counting at 50
    /// </summary>
    [JsonProperty("member_count?", Required = Required.Always)]
    public int MemberCount? { get; internal set; }

    /// <summary>
    /// Thread-specific fields not needed by other channels
    /// </summary>
    [JsonProperty("thread_metadata?", Required = Required.Always)]
    public a thread metadata object ThreadMetadata? { get; internal set; }

    /// <summary>
    /// Thread member object for the current user, if they have joined the thread, only included on certain api endpoints
    /// </summary>
    [JsonProperty("member?", Required = Required.Always)]
    public a thread member object Member? { get; internal set; }

    /// <summary>
    /// Default duration that the clients (not the api) will use for newly created threads, in minutes, to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("default_auto_archive_duration?", Required = Required.Always)]
    public int DefaultAutoArchiveDuration? { get; internal set; }

    /// <summary>
    /// Computed permissions for the invoking user in the channel, including overwrites, only included when part of the resolved data received on a slash command interaction
    /// </summary>
    [JsonProperty("permissions?", Required = Required.Always)]
    public string Permissions? { get; internal set; } = null!;

}

public class 

{
    /// <summary>
    /// Id of the message
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Id of the channel the message was sent in
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// Id of the guild the message was sent in
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// The author of this message (not guaranteed to be a valid user, see below)
    /// </summary>
    [JsonProperty("author*", Required = Required.Always)]
    public user object Author* { get; internal set; }

    /// <summary>
    /// Member properties for this message's author
    /// </summary>
    [JsonProperty("member?**", Required = Required.Always)]
    public partial guild member object Member?** { get; internal set; }

    /// <summary>
    /// Contents of the message
    /// </summary>
    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; internal set; } = null!;

    /// <summary>
    /// When this message was sent
    /// </summary>
    [JsonProperty("timestamp", Required = Required.Always)]
    public ISO8601 timestamp Timestamp { get; internal set; }

    /// <summary>
    /// When this message was edited (or null if never)
    /// </summary>
    [JsonProperty("edited_timestamp", Required = Required.AllowNull)]
    public ISO8601 timestamp? EditedTimestamp { get; internal set; }

    /// <summary>
    /// Whether this was a tts message
    /// </summary>
    [JsonProperty("tts", Required = Required.Always)]
    public bool Tts { get; internal set; }

    /// <summary>
    /// Whether this message mentions everyone
    /// </summary>
    [JsonProperty("mention_everyone", Required = Required.Always)]
    public bool MentionEveryone { get; internal set; }

    /// <summary>
    /// Users specifically mentioned in the message
    /// </summary>
    [JsonProperty("mentions***", Required = Required.Always)]
    public user objects, with an additional partial member field[] Mentions*** { get; internal set; }

    /// <summary>
    /// Roles specifically mentioned in this message
    /// </summary>
    [JsonProperty("mention_roles", Required = Required.Always)]
    public role object ids[] MentionRoles { get; internal set; }

    /// <summary>
    /// Channels specifically mentioned in this message
    /// </summary>
    [JsonProperty("mention_channels?****", Required = Required.Always)]
    public channel mention objects[] MentionChannels?**** { get; internal set; }

    /// <summary>
    /// Any attached files
    /// </summary>
    [JsonProperty("attachments", Required = Required.Always)]
    public attachment objects[] Attachments { get; internal set; }

    /// <summary>
    /// Any embedded content
    /// </summary>
    [JsonProperty("embeds", Required = Required.Always)]
    public embed objects[] Embeds { get; internal set; }

    /// <summary>
    /// Reactions to the message
    /// </summary>
    [JsonProperty("reactions?", Required = Required.Always)]
    public reaction objects[] Reactions? { get; internal set; }

    /// <summary>
    /// Used for validating a message was sent
    /// </summary>
    [JsonProperty("nonce?", Required = Required.Always)]
    public integer or string Nonce? { get; internal set; }

    /// <summary>
    /// Whether this message is pinned
    /// </summary>
    [JsonProperty("pinned", Required = Required.Always)]
    public bool Pinned { get; internal set; }

    /// <summary>
    /// If the message is generated by a webhook, this is the webhook's id
    /// </summary>
    [JsonProperty("webhook_id?", Required = Required.Always)]
    public ulong WebhookId? { get; internal set; }

    /// <summary>
    /// Type of message
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// Sent with rich presence-related chat embeds
    /// </summary>
    [JsonProperty("activity?", Required = Required.Always)]
    public message activity object Activity? { get; internal set; }

    /// <summary>
    /// Sent with rich presence-related chat embeds
    /// </summary>
    [JsonProperty("application?", Required = Required.Always)]
    public partial application object Application? { get; internal set; }

    /// <summary>
    /// If the message is an interaction or application-owned webhook, this is the id of the application
    /// </summary>
    [JsonProperty("application_id?", Required = Required.Always)]
    public ulong ApplicationId? { get; internal set; }

    /// <summary>
    /// Data showing the source of a crosspost, channel follow add, pin, or reply message
    /// </summary>
    [JsonProperty("message_reference?", Required = Required.Always)]
    public message reference object MessageReference? { get; internal set; }

    /// <summary>
    /// Message flags combined as a bitfield
    /// </summary>
    [JsonProperty("flags?", Required = Required.Always)]
    public int Flags? { get; internal set; }

    /// <summary>
    /// The message associated with the message_reference
    /// </summary>
    [JsonProperty("referenced_message?*****", Required = Required.AllowNull)]
    public message object? ReferencedMessage?***** { get; internal set; }

    /// <summary>
    /// Sent if the message is a response to an interaction
    /// </summary>
    [JsonProperty("interaction?", Required = Required.Always)]
    public message interaction object Interaction? { get; internal set; }

    /// <summary>
    /// The thread that was started from this message, includes thread member object
    /// </summary>
    [JsonProperty("thread?", Required = Required.Always)]
    public channel object Thread? { get; internal set; }

    /// <summary>
    /// Sent if the message contains components like buttons, action rows, or other interactive components
    /// </summary>
    [JsonProperty("components?", Required = Required.Always)]
    public Array of message components Components? { get; internal set; }

    /// <summary>
    /// Sent if the message contains stickers
    /// </summary>
    [JsonProperty("sticker_items?", Required = Required.Always)]
    public message sticker item objects[] StickerItems? { get; internal set; }

    /// <summary>
    /// Deprecated the stickers sent with the message
    /// </summary>
    [JsonProperty("stickers?", Required = Required.Always)]
    public sticker objects[] Stickers? { get; internal set; }

}

enum 

{
    DEFAULT = 0, // 
    RECIPIENT_ADD = 1, // 
    RECIPIENT_REMOVE = 2, // 
    CALL = 3, // 
    CHANNEL_NAME_CHANGE = 4, // 
    CHANNEL_ICON_CHANGE = 5, // 
    CHANNEL_PINNED_MESSAGE = 6, // 
    GUILD_MEMBER_JOIN = 7, // 
    USER_PREMIUM_GUILD_SUBSCRIPTION = 8, // 
    USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_1 = 9, // 
    USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_2 = 10, // 
    USER_PREMIUM_GUILD_SUBSCRIPTION_TIER_3 = 11, // 
    CHANNEL_FOLLOW_ADD = 12, // 
    GUILD_DISCOVERY_DISQUALIFIED = 14, // 
    GUILD_DISCOVERY_REQUALIFIED = 15, // 
    GUILD_DISCOVERY_GRACE_PERIOD_INITIAL_WARNING = 16, // 
    GUILD_DISCOVERY_GRACE_PERIOD_FINAL_WARNING = 17, // 
    THREAD_CREATED = 18, // 
    REPLY = 19, // 
    CHAT_INPUT_COMMAND = 20, // 
    THREAD_STARTER_MESSAGE = 21, // 
    GUILD_INVITE_REMINDER = 22, // 
    CONTEXT_MENU_COMMAND = 23, // 
}

public class MessageActivityStructure
{
    /// <summary>
    /// Type of message activity
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// Party_id from a rich presence event
    /// </summary>
    [JsonProperty("party_id?", Required = Required.Always)]
    public string PartyId? { get; internal set; } = null!;

}

enum MessageActivityTypes
{
    JOIN = 1, // 
    SPECTATE = 2, // 
    LISTEN = 3, // 
    JOIN_REQUEST = 5, // 
}

public class MessageReferenceStructure
{
    /// <summary>
    /// Id of the originating message
    /// </summary>
    [JsonProperty("message_id?", Required = Required.Always)]
    public ulong MessageId? { get; internal set; }

    /// <summary>
    /// Id of the originating message's channel
    /// </summary>
    [JsonProperty("channel_id? *", Required = Required.Always)]
    public ulong ChannelId? * { get; internal set; }

    /// <summary>
    /// Id of the originating message's guild
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// When sending, whether to error if the referenced message doesn't exist instead of sending as a normal (non-reply) message, default true
    /// </summary>
    [JsonProperty("fail_if_not_exists?", Required = Required.Always)]
    public bool FailIfNotExists? { get; internal set; }

}

public class FollowedChannelStructure
{
    /// <summary>
    /// Source channel id
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// Created target webhook id
    /// </summary>
    [JsonProperty("webhook_id", Required = Required.Always)]
    public ulong WebhookId { get; internal set; }

}

public class ReactionStructure
{
    /// <summary>
    /// Times this emoji has been used to react
    /// </summary>
    [JsonProperty("count", Required = Required.Always)]
    public int Count { get; internal set; }

    /// <summary>
    /// Whether the current user reacted using this emoji
    /// </summary>
    [JsonProperty("me", Required = Required.Always)]
    public bool Me { get; internal set; }

    /// <summary>
    /// Emoji information
    /// </summary>
    [JsonProperty("emoji", Required = Required.Always)]
    public partial emoji object Emoji { get; internal set; }

}

public class OverwriteStructure
{
    /// <summary>
    /// Role or user id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Either 0 (role) or 1 (member)
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("allow", Required = Required.Always)]
    public string Allow { get; internal set; } = null!;

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("deny", Required = Required.Always)]
    public string Deny { get; internal set; } = null!;

}

public class ThreadMetadataStructure
{
    /// <summary>
    /// Whether the thread is archived
    /// </summary>
    [JsonProperty("archived", Required = Required.Always)]
    public bool Archived { get; internal set; }

    /// <summary>
    /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("auto_archive_duration", Required = Required.Always)]
    public int AutoArchiveDuration { get; internal set; }

    /// <summary>
    /// Timestamp when the thread's archive status was last changed, used for calculating recent activity
    /// </summary>
    [JsonProperty("archive_timestamp", Required = Required.Always)]
    public ISO8601 timestamp ArchiveTimestamp { get; internal set; }

    /// <summary>
    /// Whether the thread is locked; when a thread is locked, only users with manage_threads can unarchive it
    /// </summary>
    [JsonProperty("locked", Required = Required.Always)]
    public bool Locked { get; internal set; }

    /// <summary>
    /// Whether non-moderators can add other non-moderators to a thread; only available on private threads
    /// </summary>
    [JsonProperty("invitable?", Required = Required.Always)]
    public bool Invitable? { get; internal set; }

    /// <summary>
    /// Timestamp when the thread was created; only populated for threads created after 2022-01-09
    /// </summary>
    [JsonProperty("create_timestamp?", Required = Required.Always)]
    public ISO8601 timestamp CreateTimestamp? { get; internal set; }

}

public class ThreadMemberStructure
{
    /// <summary>
    /// The id of the thread
    /// </summary>
    [JsonProperty("id? *", Required = Required.Always)]
    public ulong Id? * { get; internal set; }

    /// <summary>
    /// The id of the user
    /// </summary>
    [JsonProperty("user_id? *", Required = Required.Always)]
    public ulong UserId? * { get; internal set; }

    /// <summary>
    /// The time the current user last joined the thread
    /// </summary>
    [JsonProperty("join_timestamp", Required = Required.Always)]
    public ISO8601 timestamp JoinTimestamp { get; internal set; }

    /// <summary>
    /// Any user-thread settings, currently only used for notifications
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; internal set; }

}

public class EmbedStructure
{
    /// <summary>
    /// Title of embed
    /// </summary>
    [JsonProperty("title?", Required = Required.Always)]
    public string Title? { get; internal set; } = null!;

    /// <summary>
    /// Type of embed (always "rich" for webhook embeds)
    /// </summary>
    [JsonProperty("type?", Required = Required.Always)]
    public string Type? { get; internal set; } = null!;

    /// <summary>
    /// Description of embed
    /// </summary>
    [JsonProperty("description?", Required = Required.Always)]
    public string Description? { get; internal set; } = null!;

    /// <summary>
    /// Url of embed
    /// </summary>
    [JsonProperty("url?", Required = Required.Always)]
    public string Url? { get; internal set; } = null!;

    /// <summary>
    /// Timestamp of embed content
    /// </summary>
    [JsonProperty("timestamp?", Required = Required.Always)]
    public ISO8601 timestamp Timestamp? { get; internal set; }

    /// <summary>
    /// Color code of the embed
    /// </summary>
    [JsonProperty("color?", Required = Required.Always)]
    public int Color? { get; internal set; }

    /// <summary>
    /// Footer information
    /// </summary>
    [JsonProperty("footer?", Required = Required.Always)]
    public embed footer object Footer? { get; internal set; }

    /// <summary>
    /// Image information
    /// </summary>
    [JsonProperty("image?", Required = Required.Always)]
    public embed image object Image? { get; internal set; }

    /// <summary>
    /// Thumbnail information
    /// </summary>
    [JsonProperty("thumbnail?", Required = Required.Always)]
    public embed thumbnail object Thumbnail? { get; internal set; }

    /// <summary>
    /// Video information
    /// </summary>
    [JsonProperty("video?", Required = Required.Always)]
    public embed video object Video? { get; internal set; }

    /// <summary>
    /// Provider information
    /// </summary>
    [JsonProperty("provider?", Required = Required.Always)]
    public embed provider object Provider? { get; internal set; }

    /// <summary>
    /// Author information
    /// </summary>
    [JsonProperty("author?", Required = Required.Always)]
    public embed author object Author? { get; internal set; }

    /// <summary>
    /// Fields information
    /// </summary>
    [JsonProperty("fields?", Required = Required.Always)]
    public embed field objects[] Fields? { get; internal set; }

}

public class EmbedThumbnailStructure
{
    /// <summary>
    /// Source url of thumbnail (only supports http(s) and attachments)
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of the thumbnail
    /// </summary>
    [JsonProperty("proxy_url?", Required = Required.Always)]
    public string ProxyUrl? { get; internal set; } = null!;

    /// <summary>
    /// Height of thumbnail
    /// </summary>
    [JsonProperty("height?", Required = Required.Always)]
    public int Height? { get; internal set; }

    /// <summary>
    /// Width of thumbnail
    /// </summary>
    [JsonProperty("width?", Required = Required.Always)]
    public int Width? { get; internal set; }

}

public class EmbedVideoStructure
{
    /// <summary>
    /// Source url of video
    /// </summary>
    [JsonProperty("url?", Required = Required.Always)]
    public string Url? { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of the video
    /// </summary>
    [JsonProperty("proxy_url?", Required = Required.Always)]
    public string ProxyUrl? { get; internal set; } = null!;

    /// <summary>
    /// Height of video
    /// </summary>
    [JsonProperty("height?", Required = Required.Always)]
    public int Height? { get; internal set; }

    /// <summary>
    /// Width of video
    /// </summary>
    [JsonProperty("width?", Required = Required.Always)]
    public int Width? { get; internal set; }

}

public class EmbedImageStructure
{
    /// <summary>
    /// Source url of image (only supports http(s) and attachments)
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of the image
    /// </summary>
    [JsonProperty("proxy_url?", Required = Required.Always)]
    public string ProxyUrl? { get; internal set; } = null!;

    /// <summary>
    /// Height of image
    /// </summary>
    [JsonProperty("height?", Required = Required.Always)]
    public int Height? { get; internal set; }

    /// <summary>
    /// Width of image
    /// </summary>
    [JsonProperty("width?", Required = Required.Always)]
    public int Width? { get; internal set; }

}

public class EmbedProviderStructure
{
    /// <summary>
    /// Name of provider
    /// </summary>
    [JsonProperty("name?", Required = Required.Always)]
    public string Name? { get; internal set; } = null!;

    /// <summary>
    /// Url of provider
    /// </summary>
    [JsonProperty("url?", Required = Required.Always)]
    public string Url? { get; internal set; } = null!;

}

public class EmbedAuthorStructure
{
    /// <summary>
    /// Name of author
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Url of author
    /// </summary>
    [JsonProperty("url?", Required = Required.Always)]
    public string Url? { get; internal set; } = null!;

    /// <summary>
    /// Url of author icon (only supports http(s) and attachments)
    /// </summary>
    [JsonProperty("icon_url?", Required = Required.Always)]
    public string IconUrl? { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of author icon
    /// </summary>
    [JsonProperty("proxy_icon_url?", Required = Required.Always)]
    public string ProxyIconUrl? { get; internal set; } = null!;

}

public class EmbedFooterStructure
{
    /// <summary>
    /// Footer text
    /// </summary>
    [JsonProperty("text", Required = Required.Always)]
    public string Text { get; internal set; } = null!;

    /// <summary>
    /// Url of footer icon (only supports http(s) and attachments)
    /// </summary>
    [JsonProperty("icon_url?", Required = Required.Always)]
    public string IconUrl? { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of footer icon
    /// </summary>
    [JsonProperty("proxy_icon_url?", Required = Required.Always)]
    public string ProxyIconUrl? { get; internal set; } = null!;

}

public class EmbedFieldStructure
{
    /// <summary>
    /// Name of the field
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Value of the field
    /// </summary>
    [JsonProperty("value", Required = Required.Always)]
    public string Value { get; internal set; } = null!;

    /// <summary>
    /// Whether or not this field should display inline
    /// </summary>
    [JsonProperty("inline?", Required = Required.Always)]
    public bool Inline? { get; internal set; }

}

public class 

{
    /// <summary>
    /// Attachment id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Name of file attached
    /// </summary>
    [JsonProperty("filename", Required = Required.Always)]
    public string Filename { get; internal set; } = null!;

    /// <summary>
    /// Description for the file
    /// </summary>
    [JsonProperty("description?", Required = Required.Always)]
    public string Description? { get; internal set; } = null!;

    /// <summary>
    /// The attachment's media type
    /// </summary>
    [JsonProperty("content_type?", Required = Required.Always)]
    public string ContentType? { get; internal set; } = null!;

    /// <summary>
    /// Size of file in bytes
    /// </summary>
    [JsonProperty("size", Required = Required.Always)]
    public int Size { get; internal set; }

    /// <summary>
    /// Source url of file
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; internal set; } = null!;

    /// <summary>
    /// A proxied url of file
    /// </summary>
    [JsonProperty("proxy_url", Required = Required.Always)]
    public string ProxyUrl { get; internal set; } = null!;

    /// <summary>
    /// Height of file (if image)
    /// </summary>
    [JsonProperty("height?", Required = Required.AllowNull)]
    public int? Height? { get; internal set; }

    /// <summary>
    /// Width of file (if image)
    /// </summary>
    [JsonProperty("width?", Required = Required.AllowNull)]
    public int? Width? { get; internal set; }

    /// <summary>
    /// Whether this attachment is ephemeral
    /// </summary>
    [JsonProperty("ephemeral? *", Required = Required.Always)]
    public bool Ephemeral? * { get; internal set; }

}

public class ChannelMentionStructure
{
    /// <summary>
    /// Id of the channel
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Id of the guild containing the channel
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; internal set; }

    /// <summary>
    /// The type of channel
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// The name of the channel
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

}

enum AllowedMentionTypes
{
    Role Mentions = "roles", // Controls role mentions
    User Mentions = "users", // Controls user mentions
    Everyone Mentions = "everyone", // Controls @everyone and @here mentions
}

public class AllowedMentionsStructure
{
    /// <summary>
    /// An array of allowed mention types to parse from the content.
    /// </summary>
    [JsonProperty("parse", Required = Required.Always)]
    public allowed mention types[] Parse { get; internal set; }

    /// <summary>
    /// Array of role_ids to mention (max size of 100)
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public list of snowflakes Roles { get; internal set; }

    /// <summary>
    /// Array of user_ids to mention (max size of 100)
    /// </summary>
    [JsonProperty("users", Required = Required.Always)]
    public list of snowflakes Users { get; internal set; }

    /// <summary>
    /// For replies, whether to mention the author of the message being replied to (default false)
    /// </summary>
    [JsonProperty("replied_user", Required = Required.Always)]
    public bool RepliedUser { get; internal set; }

}

public class FiresA
{
    /// <summary>
    /// 1-100 character channel name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Base64 encoded icon
    /// </summary>
    [JsonProperty("icon", Required = Required.Always)]
    public binary Icon { get; internal set; }

}

public class RequiresThe
{
    /// <summary>
    /// 1-100 character channel name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The type of channel; only conversion between text and news is supported and only in guilds with the "news" feature
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// The position of the channel in the left-hand listing
    /// </summary>
    [JsonProperty("position", Required = Required.AllowNull)]
    public int? Position { get; internal set; }

    /// <summary>
    /// 0-1024 character channel topic
    /// </summary>
    [JsonProperty("topic", Required = Required.AllowNull)]
    public string? Topic { get; internal set; }

    /// <summary>
    /// Whether the channel is nsfw
    /// </summary>
    [JsonProperty("nsfw", Required = Required.AllowNull)]
    public bool? Nsfw { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected
    /// </summary>
    [JsonProperty("rate_limit_per_user", Required = Required.AllowNull)]
    public int? RateLimitPerUser { get; internal set; }

    /// <summary>
    /// The bitrate (in bits) of the voice channel; 8000 to 96000 (128000 for vip servers)
    /// </summary>
    [JsonProperty("bitrate", Required = Required.AllowNull)]
    public int? Bitrate { get; internal set; }

    /// <summary>
    /// The user limit of the voice channel; 0 refers to no limit, 1 to 99 refers to a user limit
    /// </summary>
    [JsonProperty("user_limit", Required = Required.AllowNull)]
    public int? UserLimit { get; internal set; }

    /// <summary>
    /// Channel or category-specific permissions
    /// </summary>
    [JsonProperty("permission_overwrites", Required = Required.AllowNull)]
    public overwrite objects[]? PermissionOverwrites { get; internal set; }

    /// <summary>
    /// Id of the new parent category for a channel
    /// </summary>
    [JsonProperty("parent_id", Required = Required.AllowNull)]
    public ulong? ParentId { get; internal set; }

    /// <summary>
    /// Channel voice region id, automatic when set to null
    /// </summary>
    [JsonProperty("rtc_region", Required = Required.AllowNull)]
    public string? RtcRegion { get; internal set; }

    /// <summary>
    /// The camera video quality mode of the voice channel
    /// </summary>
    [JsonProperty("video_quality_mode", Required = Required.AllowNull)]
    public int? VideoQualityMode { get; internal set; }

    /// <summary>
    /// The default duration that the clients use (not the api) for newly created threads in the channel, in minutes, to automatically archive the thread after recent activity
    /// </summary>
    [JsonProperty("default_auto_archive_duration", Required = Required.AllowNull)]
    public int? DefaultAutoArchiveDuration { get; internal set; }

}

public class Otherwise,RequiresThe
{
    /// <summary>
    /// 1-100 character channel name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Whether the thread is archived
    /// </summary>
    [JsonProperty("archived", Required = Required.Always)]
    public bool Archived { get; internal set; }

    /// <summary>
    /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("auto_archive_duration*", Required = Required.Always)]
    public int AutoArchiveDuration* { get; internal set; }

    /// <summary>
    /// Whether the thread is locked; when a thread is locked, only users with manage_threads can unarchive it
    /// </summary>
    [JsonProperty("locked", Required = Required.Always)]
    public bool Locked { get; internal set; }

    /// <summary>
    /// Whether non-moderators can add other non-moderators to a thread; only available on private threads
    /// </summary>
    [JsonProperty("invitable", Required = Required.Always)]
    public bool Invitable { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages, manage_thread, or manage_channel, are unaffected
    /// </summary>
    [JsonProperty("rate_limit_per_user", Required = Required.AllowNull)]
    public int? RateLimitPerUser { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Get messages around this message id
    /// </summary>
    [JsonProperty("around", Required = Required.Always)]
    public ulong Around { get; internal set; }

    /// <summary>
    /// Get messages before this message id
    /// </summary>
    [JsonProperty("before", Required = Required.Always)]
    public ulong Before { get; internal set; }

    /// <summary>
    /// Get messages after this message id
    /// </summary>
    [JsonProperty("after", Required = Required.Always)]
    public ulong After { get; internal set; }

    /// <summary>
    /// Max number of messages to return (1-100)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

}

public class Json/formParams
{
    /// <summary>
    /// The message contents (up to 2000 characters)
    /// </summary>
    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; internal set; } = null!;

    /// <summary>
    /// True if this is a tts message
    /// </summary>
    [JsonProperty("tts", Required = Required.Always)]
    public bool Tts { get; internal set; }

    /// <summary>
    /// Embedded rich content (up to 6000 characters)
    /// </summary>
    [JsonProperty("embeds", Required = Required.Always)]
    public embed objects[] Embeds { get; internal set; }

    /// <summary>
    /// Embedded rich content, deprecated in favor of embeds
    /// </summary>
    [JsonProperty("embed (deprecated)", Required = Required.Always)]
    public embed object Embed (deprecated) { get; internal set; }

    /// <summary>
    /// Allowed mentions for the message
    /// </summary>
    [JsonProperty("allowed_mentions", Required = Required.Always)]
    public allowed mention object AllowedMentions { get; internal set; }

    /// <summary>
    /// Include to make your message a reply
    /// </summary>
    [JsonProperty("message_reference", Required = Required.Always)]
    public message reference MessageReference { get; internal set; }

    /// <summary>
    /// The components to include with the message
    /// </summary>
    [JsonProperty("components", Required = Required.Always)]
    public message component objects[] Components { get; internal set; }

    /// <summary>
    /// Ids of up to 3 stickers in the server to send in the message
    /// </summary>
    [JsonProperty("sticker_ids", Required = Required.Always)]
    public snowflakes[] StickerIds { get; internal set; }

    /// <summary>
    /// The contents of the file being sent
    /// </summary>
    [JsonProperty("files[n] *", Required = Required.Always)]
    public file contents Files[n] * { get; internal set; }

    /// <summary>
    /// Json encoded body of non-file params
    /// </summary>
    [JsonProperty("payload_json *", Required = Required.Always)]
    public string PayloadJson * { get; internal set; } = null!;

    /// <summary>
    /// Attachment objects with filename and description
    /// </summary>
    [JsonProperty("attachments *", Required = Required.Always)]
    public partial attachment objects[] Attachments * { get; internal set; }

    /// <summary>
    /// Message flags combined as a bitfield
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Get users after this user id
    /// </summary>
    [JsonProperty("after", Required = Required.Always)]
    public ulong After { get; internal set; }

    /// <summary>
    /// Max number of users to return (1-100)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

}

public class Json/formParams
{
    /// <summary>
    /// The message contents (up to 2000 characters)
    /// </summary>
    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; internal set; } = null!;

    /// <summary>
    /// Embedded rich content (up to 6000 characters)
    /// </summary>
    [JsonProperty("embeds", Required = Required.Always)]
    public embed objects[] Embeds { get; internal set; }

    /// <summary>
    /// Embedded rich content, deprecated in favor of embeds
    /// </summary>
    [JsonProperty("embed (deprecated)", Required = Required.Always)]
    public embed object Embed (deprecated) { get; internal set; }

    /// <summary>
    /// Edit the flags of a message (only suppress_embeds can currently be set/unset)
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; internal set; }

    /// <summary>
    /// Allowed mentions for the message
    /// </summary>
    [JsonProperty("allowed_mentions", Required = Required.Always)]
    public allowed mention object AllowedMentions { get; internal set; }

    /// <summary>
    /// The components to include with the message
    /// </summary>
    [JsonProperty("components", Required = Required.Always)]
    public message component[] Components { get; internal set; }

    /// <summary>
    /// The contents of the file being sent/edited
    /// </summary>
    [JsonProperty("files[n] *", Required = Required.Always)]
    public file contents Files[n] * { get; internal set; }

    /// <summary>
    /// Json encoded body of non-file params (multipart/form-data only)
    /// </summary>
    [JsonProperty("payload_json *", Required = Required.Always)]
    public string PayloadJson * { get; internal set; } = null!;

    /// <summary>
    /// Attached files to keep and possible descriptions for new files
    /// </summary>
    [JsonProperty("attachments *", Required = Required.Always)]
    public attachment objects[] Attachments * { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// An array of message ids to delete (2-100)
    /// </summary>
    [JsonProperty("messages", Required = Required.Always)]
    public snowflakes[] Messages { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The bitwise value of all allowed permissions
    /// </summary>
    [JsonProperty("allow", Required = Required.Always)]
    public string Allow { get; internal set; } = null!;

    /// <summary>
    /// The bitwise value of all disallowed permissions
    /// </summary>
    [JsonProperty("deny", Required = Required.Always)]
    public string Deny { get; internal set; } = null!;

    /// <summary>
    /// 0 for a role or 1 for a member
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Duration of invite in seconds before expiry, or 0 for never. between 0 and 604800 (7 days)
    /// </summary>
    [JsonProperty("max_age", Required = Required.Always)]
    public int MaxAge { get; internal set; }

    /// <summary>
    /// Max number of uses or 0 for unlimited. between 0 and 100
    /// </summary>
    [JsonProperty("max_uses", Required = Required.Always)]
    public int MaxUses { get; internal set; }

    /// <summary>
    /// Whether this invite only grants temporary membership
    /// </summary>
    [JsonProperty("temporary", Required = Required.Always)]
    public bool Temporary { get; internal set; }

    /// <summary>
    /// If true, don't try to reuse a similar invite (useful for creating many unique one time use invites)
    /// </summary>
    [JsonProperty("unique", Required = Required.Always)]
    public bool Unique { get; internal set; }

    /// <summary>
    /// The type of target for this voice channel invite
    /// </summary>
    [JsonProperty("target_type", Required = Required.Always)]
    public int TargetType { get; internal set; }

    /// <summary>
    /// The id of the user whose stream to display for this invite, required if target_type is 1, the user must be streaming in the channel
    /// </summary>
    [JsonProperty("target_user_id", Required = Required.Always)]
    public ulong TargetUserId { get; internal set; }

    /// <summary>
    /// The id of the embedded application to open for this invite, required if target_type is 2, the application must have the embedded flag
    /// </summary>
    [JsonProperty("target_application_id", Required = Required.Always)]
    public ulong TargetApplicationId { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Id of target channel
    /// </summary>
    [JsonProperty("webhook_channel_id", Required = Required.Always)]
    public ulong WebhookChannelId { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Access token of a user that has granted your app the gdm.join scope
    /// </summary>
    [JsonProperty("access_token", Required = Required.Always)]
    public string AccessToken { get; internal set; } = null!;

    /// <summary>
    /// Nickname of the user being added
    /// </summary>
    [JsonProperty("nick", Required = Required.Always)]
    public string Nick { get; internal set; } = null!;

}

public class JsonParams
{
    /// <summary>
    /// 1-100 character channel name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("auto_archive_duration?*", Required = Required.Always)]
    public int AutoArchiveDuration?* { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600)
    /// </summary>
    [JsonProperty("rate_limit_per_user?", Required = Required.AllowNull)]
    public int? RateLimitPerUser? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// 1-100 character channel name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("auto_archive_duration?**", Required = Required.Always)]
    public int AutoArchiveDuration?** { get; internal set; }

    /// <summary>
    /// The type of thread to create
    /// </summary>
    [JsonProperty("type?***", Required = Required.Always)]
    public int Type?*** { get; internal set; }

    /// <summary>
    /// Whether non-moderators can add other non-moderators to a thread; only available when creating a private thread
    /// </summary>
    [JsonProperty("invitable?", Required = Required.Always)]
    public bool Invitable? { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600)
    /// </summary>
    [JsonProperty("rate_limit_per_user?", Required = Required.AllowNull)]
    public int? RateLimitPerUser? { get; internal set; }

}

public class ResponseBody
{
    /// <summary>
    /// The active threads
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public channel objects[] Threads { get; internal set; }

    /// <summary>
    /// A thread member object for each returned thread the current user has joined
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public thread members objects[] Members { get; internal set; }

    /// <summary>
    /// Whether there are potentially additional threads that could be returned on a subsequent call
    /// </summary>
    [JsonProperty("has_more", Required = Required.Always)]
    public bool HasMore { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Returns threads before this timestamp
    /// </summary>
    [JsonProperty("before?", Required = Required.Always)]
    public ISO8601 timestamp Before? { get; internal set; }

    /// <summary>
    /// Optional maximum number of threads to return
    /// </summary>
    [JsonProperty("limit?", Required = Required.Always)]
    public int Limit? { get; internal set; }

}

public class ResponseBody
{
    /// <summary>
    /// The public, archived threads
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public channel objects[] Threads { get; internal set; }

    /// <summary>
    /// A thread member object for each returned thread the current user has joined
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public thread members objects[] Members { get; internal set; }

    /// <summary>
    /// Whether there are potentially additional threads that could be returned on a subsequent call
    /// </summary>
    [JsonProperty("has_more", Required = Required.Always)]
    public bool HasMore { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Returns threads before this timestamp
    /// </summary>
    [JsonProperty("before?", Required = Required.Always)]
    public ISO8601 timestamp Before? { get; internal set; }

    /// <summary>
    /// Optional maximum number of threads to return
    /// </summary>
    [JsonProperty("limit?", Required = Required.Always)]
    public int Limit? { get; internal set; }

}

public class ResponseBody
{
    /// <summary>
    /// The private, archived threads
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public channel objects[] Threads { get; internal set; }

    /// <summary>
    /// A thread member object for each returned thread the current user has joined
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public thread members objects[] Members { get; internal set; }

    /// <summary>
    /// Whether there are potentially additional threads that could be returned on a subsequent call
    /// </summary>
    [JsonProperty("has_more", Required = Required.Always)]
    public bool HasMore { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Returns threads before this id
    /// </summary>
    [JsonProperty("before?", Required = Required.Always)]
    public ulong Before? { get; internal set; }

    /// <summary>
    /// Optional maximum number of threads to return
    /// </summary>
    [JsonProperty("limit?", Required = Required.Always)]
    public int Limit? { get; internal set; }

}

public class ResponseBody
{
    /// <summary>
    /// The private, archived threads the current user has joined
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public channel objects[] Threads { get; internal set; }

    /// <summary>
    /// A thread member object for each returned thread the current user has joined
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public thread members objects[] Members { get; internal set; }

    /// <summary>
    /// Whether there are potentially additional threads that could be returned on a subsequent call
    /// </summary>
    [JsonProperty("has_more", Required = Required.Always)]
    public bool HasMore { get; internal set; }

}

