public class GuildStructure
{
    /// <summary>
    /// Guild id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Guild name (2-100 characters, excluding trailing and leading whitespace)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Icon hash
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public string? Icon { get; internal set; }

    /// <summary>
    /// Icon hash, returned when in the template object
    /// </summary>
    [JsonProperty("icon_hash?", Required = Required.AllowNull)]
    public string? IconHash? { get; internal set; }

    /// <summary>
    /// Splash hash
    /// </summary>
    [JsonProperty("splash", Required = Required.AllowNull)]
    public string? Splash { get; internal set; }

    /// <summary>
    /// Discovery splash hash; only present for guilds with the "discoverable" feature
    /// </summary>
    [JsonProperty("discovery_splash", Required = Required.AllowNull)]
    public string? DiscoverySplash { get; internal set; }

    /// <summary>
    /// True if the user is the owner of the guild
    /// </summary>
    [JsonProperty("owner? **", Required = Required.Always)]
    public bool Owner? ** { get; internal set; }

    /// <summary>
    /// Id of owner
    /// </summary>
    [JsonProperty("owner_id", Required = Required.Always)]
    public ulong OwnerId { get; internal set; }

    /// <summary>
    /// Total permissions for the user in the guild (excludes overwrites)
    /// </summary>
    [JsonProperty("permissions? **", Required = Required.Always)]
    public string Permissions? ** { get; internal set; } = null!;

    /// <summary>
    /// Voice region id for the guild (deprecated)
    /// </summary>
    [JsonProperty("region? ***", Required = Required.AllowNull)]
    public string? Region? *** { get; internal set; }

    /// <summary>
    /// Id of afk channel
    /// </summary>
    [JsonProperty("afk_channel_id", Required = Required.AllowNull)]
    public ulong? AfkChannelId { get; internal set; }

    /// <summary>
    /// Afk timeout in seconds
    /// </summary>
    [JsonProperty("afk_timeout", Required = Required.Always)]
    public int AfkTimeout { get; internal set; }

    /// <summary>
    /// True if the server widget is enabled
    /// </summary>
    [JsonProperty("widget_enabled?", Required = Required.Always)]
    public bool WidgetEnabled? { get; internal set; }

    /// <summary>
    /// The channel id that the widget will generate an invite to, or null if set to no invite
    /// </summary>
    [JsonProperty("widget_channel_id?", Required = Required.AllowNull)]
    public ulong? WidgetChannelId? { get; internal set; }

    /// <summary>
    /// Verification level required for the guild
    /// </summary>
    [JsonProperty("verification_level", Required = Required.Always)]
    public int VerificationLevel { get; internal set; }

    /// <summary>
    /// Default message notifications level
    /// </summary>
    [JsonProperty("default_message_notifications", Required = Required.Always)]
    public int DefaultMessageNotifications { get; internal set; }

    /// <summary>
    /// Explicit content filter level
    /// </summary>
    [JsonProperty("explicit_content_filter", Required = Required.Always)]
    public int ExplicitContentFilter { get; internal set; }

    /// <summary>
    /// Roles in the guild
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public role objects[] Roles { get; internal set; }

    /// <summary>
    /// Custom guild emojis
    /// </summary>
    [JsonProperty("emojis", Required = Required.Always)]
    public emoji objects[] Emojis { get; internal set; }

    /// <summary>
    /// Enabled guild features
    /// </summary>
    [JsonProperty("features", Required = Required.Always)]
    public guild feature strings[] Features { get; internal set; }

    /// <summary>
    /// Required mfa level for the guild
    /// </summary>
    [JsonProperty("mfa_level", Required = Required.Always)]
    public int MfaLevel { get; internal set; }

    /// <summary>
    /// Application id of the guild creator if it is bot-created
    /// </summary>
    [JsonProperty("application_id", Required = Required.AllowNull)]
    public ulong? ApplicationId { get; internal set; }

    /// <summary>
    /// The id of the channel where guild notices such as welcome messages and boost events are posted
    /// </summary>
    [JsonProperty("system_channel_id", Required = Required.AllowNull)]
    public ulong? SystemChannelId { get; internal set; }

    /// <summary>
    /// System channel flags
    /// </summary>
    [JsonProperty("system_channel_flags", Required = Required.Always)]
    public int SystemChannelFlags { get; internal set; }

    /// <summary>
    /// The id of the channel where community guilds can display rules and/or guidelines
    /// </summary>
    [JsonProperty("rules_channel_id", Required = Required.AllowNull)]
    public ulong? RulesChannelId { get; internal set; }

    /// <summary>
    /// When this guild was joined at
    /// </summary>
    [JsonProperty("joined_at? *", Required = Required.Always)]
    public ISO8601 timestamp JoinedAt? * { get; internal set; }

    /// <summary>
    /// True if this is considered a large guild
    /// </summary>
    [JsonProperty("large? *", Required = Required.Always)]
    public bool Large? * { get; internal set; }

    /// <summary>
    /// True if this guild is unavailable due to an outage
    /// </summary>
    [JsonProperty("unavailable? *", Required = Required.Always)]
    public bool Unavailable? * { get; internal set; }

    /// <summary>
    /// Total number of members in this guild
    /// </summary>
    [JsonProperty("member_count? *", Required = Required.Always)]
    public int MemberCount? * { get; internal set; }

    /// <summary>
    /// States of members currently in voice channels; lacks the guild_id key
    /// </summary>
    [JsonProperty("voice_states? *", Required = Required.Always)]
    public partial voice state objects[] VoiceStates? * { get; internal set; }

    /// <summary>
    /// Users in the guild
    /// </summary>
    [JsonProperty("members? *", Required = Required.Always)]
    public guild member objects[] Members? * { get; internal set; }

    /// <summary>
    /// Channels in the guild
    /// </summary>
    [JsonProperty("channels? *", Required = Required.Always)]
    public channel objects[] Channels? * { get; internal set; }

    /// <summary>
    /// All active threads in the guild that current user has permission to view
    /// </summary>
    [JsonProperty("threads? *", Required = Required.Always)]
    public channel objects[] Threads? * { get; internal set; }

    /// <summary>
    /// Presences of the members in the guild, will only include non-offline members if the size is greater than large threshold
    /// </summary>
    [JsonProperty("presences? *", Required = Required.Always)]
    public partial presence update objects[] Presences? * { get; internal set; }

    /// <summary>
    /// The maximum number of presences for the guild (null is always returned, apart from the largest of guilds)
    /// </summary>
    [JsonProperty("max_presences?", Required = Required.AllowNull)]
    public int? MaxPresences? { get; internal set; }

    /// <summary>
    /// The maximum number of members for the guild
    /// </summary>
    [JsonProperty("max_members?", Required = Required.Always)]
    public int MaxMembers? { get; internal set; }

    /// <summary>
    /// The vanity url code for the guild
    /// </summary>
    [JsonProperty("vanity_url_code", Required = Required.AllowNull)]
    public string? VanityUrlCode { get; internal set; }

    /// <summary>
    /// The description of a community guild
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Banner hash
    /// </summary>
    [JsonProperty("banner", Required = Required.AllowNull)]
    public string? Banner { get; internal set; }

    /// <summary>
    /// Premium tier (server boost level)
    /// </summary>
    [JsonProperty("premium_tier", Required = Required.Always)]
    public int PremiumTier { get; internal set; }

    /// <summary>
    /// The number of boosts this guild currently has
    /// </summary>
    [JsonProperty("premium_subscription_count?", Required = Required.Always)]
    public int PremiumSubscriptionCount? { get; internal set; }

    /// <summary>
    /// The preferred locale of a community guild; used in server discovery and notices from discord, and sent in interactions; defaults to "en-us"
    /// </summary>
    [JsonProperty("preferred_locale", Required = Required.Always)]
    public string PreferredLocale { get; internal set; } = null!;

    /// <summary>
    /// The id of the channel where admins and moderators of community guilds receive notices from discord
    /// </summary>
    [JsonProperty("public_updates_channel_id", Required = Required.AllowNull)]
    public ulong? PublicUpdatesChannelId { get; internal set; }

    /// <summary>
    /// The maximum amount of users in a video channel
    /// </summary>
    [JsonProperty("max_video_channel_users?", Required = Required.Always)]
    public int MaxVideoChannelUsers? { get; internal set; }

    /// <summary>
    /// Approximate number of members in this guild, returned from the get /guilds/<id> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_member_count?", Required = Required.Always)]
    public int ApproximateMemberCount? { get; internal set; }

    /// <summary>
    /// Approximate number of non-offline members in this guild, returned from the get /guilds/<id> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_presence_count?", Required = Required.Always)]
    public int ApproximatePresenceCount? { get; internal set; }

    /// <summary>
    /// The welcome screen of a community guild, shown to new members, returned in an invite's guild object
    /// </summary>
    [JsonProperty("welcome_screen?", Required = Required.Always)]
    public welcome screen object WelcomeScreen? { get; internal set; }

    /// <summary>
    /// Guild nsfw level
    /// </summary>
    [JsonProperty("nsfw_level", Required = Required.Always)]
    public int NsfwLevel { get; internal set; }

    /// <summary>
    /// Stage instances in the guild
    /// </summary>
    [JsonProperty("stage_instances? *", Required = Required.Always)]
    public stage instance objects[] StageInstances? * { get; internal set; }

    /// <summary>
    /// Custom guild stickers
    /// </summary>
    [JsonProperty("stickers?", Required = Required.Always)]
    public sticker objects[] Stickers? { get; internal set; }

    /// <summary>
    /// The scheduled events in the guild
    /// </summary>
    [JsonProperty("guild_scheduled_events? *", Required = Required.Always)]
    public guild scheduled event objects[] GuildScheduledEvents? * { get; internal set; }

    /// <summary>
    /// Whether the guild has the boost progress bar enabled
    /// </summary>
    [JsonProperty("premium_progress_bar_enabled", Required = Required.Always)]
    public bool PremiumProgressBarEnabled { get; internal set; }

}

public class GuildPreviewStructure
{
    /// <summary>
    /// Guild id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Guild name (2-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Icon hash
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public string? Icon { get; internal set; }

    /// <summary>
    /// Splash hash
    /// </summary>
    [JsonProperty("splash", Required = Required.AllowNull)]
    public string? Splash { get; internal set; }

    /// <summary>
    /// Discovery splash hash
    /// </summary>
    [JsonProperty("discovery_splash", Required = Required.AllowNull)]
    public string? DiscoverySplash { get; internal set; }

    /// <summary>
    /// Custom guild emojis
    /// </summary>
    [JsonProperty("emojis", Required = Required.Always)]
    public emoji objects[] Emojis { get; internal set; }

    /// <summary>
    /// Enabled guild features
    /// </summary>
    [JsonProperty("features", Required = Required.Always)]
    public guild feature strings[] Features { get; internal set; }

    /// <summary>
    /// Approximate number of members in this guild
    /// </summary>
    [JsonProperty("approximate_member_count", Required = Required.Always)]
    public int ApproximateMemberCount { get; internal set; }

    /// <summary>
    /// Approximate number of online members in this guild
    /// </summary>
    [JsonProperty("approximate_presence_count", Required = Required.Always)]
    public int ApproximatePresenceCount { get; internal set; }

    /// <summary>
    /// The description for the guild, if the guild is discoverable
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Custom guild stickers
    /// </summary>
    [JsonProperty("stickers", Required = Required.Always)]
    public sticker objects[] Stickers { get; internal set; }

}

public class GuildWidgetSettingsStructure
{
    /// <summary>
    /// Whether the widget is enabled
    /// </summary>
    [JsonProperty("enabled", Required = Required.Always)]
    public bool Enabled { get; internal set; }

    /// <summary>
    /// The widget channel id
    /// </summary>
    [JsonProperty("channel_id", Required = Required.AllowNull)]
    public ulong? ChannelId { get; internal set; }

}

public class GetGuildWidgetStructure
{
    /// <summary>
    /// Guild id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Guild name (2-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Instant invite for the guilds specified widget invite channel
    /// </summary>
    [JsonProperty("instant_invite", Required = Required.AllowNull)]
    public string? InstantInvite { get; internal set; }

    /// <summary>
    /// Voice and stage channels which are accessible by @everyone
    /// </summary>
    [JsonProperty("channels", Required = Required.Always)]
    public partial channel objects[] Channels { get; internal set; }

    /// <summary>
    /// Special widget user objects that includes users presence (limit 100)
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public partial user objects[] Members { get; internal set; }

    /// <summary>
    /// Number of online members in this guild
    /// </summary>
    [JsonProperty("presence_count", Required = Required.Always)]
    public int PresenceCount { get; internal set; }

}

public class GuildMemberStructure
{
    /// <summary>
    /// The user this guild member represents
    /// </summary>
    [JsonProperty("user?", Required = Required.Always)]
    public user object User? { get; internal set; }

    /// <summary>
    /// This user's guild nickname
    /// </summary>
    [JsonProperty("nick?", Required = Required.AllowNull)]
    public string? Nick? { get; internal set; }

    /// <summary>
    /// The member's guild avatar hash
    /// </summary>
    [JsonProperty("avatar?", Required = Required.AllowNull)]
    public string? Avatar? { get; internal set; }

    /// <summary>
    /// Array of role object ids
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public snowflakes[] Roles { get; internal set; }

    /// <summary>
    /// When the user joined the guild
    /// </summary>
    [JsonProperty("joined_at", Required = Required.Always)]
    public ISO8601 timestamp JoinedAt { get; internal set; }

    /// <summary>
    /// When the user started boosting the guild
    /// </summary>
    [JsonProperty("premium_since?", Required = Required.AllowNull)]
    public ISO8601 timestamp? PremiumSince? { get; internal set; }

    /// <summary>
    /// Whether the user is deafened in voice channels
    /// </summary>
    [JsonProperty("deaf", Required = Required.Always)]
    public bool Deaf { get; internal set; }

    /// <summary>
    /// Whether the user is muted in voice channels
    /// </summary>
    [JsonProperty("mute", Required = Required.Always)]
    public bool Mute { get; internal set; }

    /// <summary>
    /// Whether the user has not yet passed the guild's membership screening requirements
    /// </summary>
    [JsonProperty("pending?", Required = Required.Always)]
    public bool Pending? { get; internal set; }

    /// <summary>
    /// Total permissions of the member in the channel, including overwrites, returned when in the interaction object
    /// </summary>
    [JsonProperty("permissions?", Required = Required.Always)]
    public string Permissions? { get; internal set; } = null!;

    /// <summary>
    /// When the user's timeout will expire and the user will be able to communicate in the guild again, null or a time in the past if the user is not timed out
    /// </summary>
    [JsonProperty("communication_disabled_until?", Required = Required.AllowNull)]
    public ISO8601 timestamp? CommunicationDisabledUntil? { get; internal set; }

}

public class IntegrationStructure
{
    /// <summary>
    /// Integration id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Integration name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Integration type (twitch, youtube, or discord)
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; internal set; } = null!;

    /// <summary>
    /// Is this integration enabled
    /// </summary>
    [JsonProperty("enabled", Required = Required.Always)]
    public bool Enabled { get; internal set; }

    /// <summary>
    /// Is this integration syncing
    /// </summary>
    [JsonProperty("syncing? *", Required = Required.Always)]
    public bool Syncing? * { get; internal set; }

    /// <summary>
    /// Id that this integration uses for "subscribers"
    /// </summary>
    [JsonProperty("role_id? *", Required = Required.Always)]
    public ulong RoleId? * { get; internal set; }

    /// <summary>
    /// Whether emoticons should be synced for this integration (twitch only currently)
    /// </summary>
    [JsonProperty("enable_emoticons? *", Required = Required.Always)]
    public bool EnableEmoticons? * { get; internal set; }

    /// <summary>
    /// The behavior of expiring subscribers
    /// </summary>
    [JsonProperty("expire_behavior? *", Required = Required.Always)]
    public integration expire behavior ExpireBehavior? * { get; internal set; }

    /// <summary>
    /// The grace period (in days) before expiring subscribers
    /// </summary>
    [JsonProperty("expire_grace_period? *", Required = Required.Always)]
    public int ExpireGracePeriod? * { get; internal set; }

    /// <summary>
    /// User for this integration
    /// </summary>
    [JsonProperty("user? *", Required = Required.Always)]
    public user object User? * { get; internal set; }

    /// <summary>
    /// Integration account information
    /// </summary>
    [JsonProperty("account", Required = Required.Always)]
    public account object Account { get; internal set; }

    /// <summary>
    /// When this integration was last synced
    /// </summary>
    [JsonProperty("synced_at? *", Required = Required.Always)]
    public ISO8601 timestamp SyncedAt? * { get; internal set; }

    /// <summary>
    /// How many subscribers this integration has
    /// </summary>
    [JsonProperty("subscriber_count? *", Required = Required.Always)]
    public int SubscriberCount? * { get; internal set; }

    /// <summary>
    /// Has this integration been revoked
    /// </summary>
    [JsonProperty("revoked? *", Required = Required.Always)]
    public bool Revoked? * { get; internal set; }

    /// <summary>
    /// The bot/oauth2 application for discord integrations
    /// </summary>
    [JsonProperty("application?", Required = Required.Always)]
    public application object Application? { get; internal set; }

}

public class IntegrationAccountStructure
{
    /// <summary>
    /// Id of the account
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; internal set; } = null!;

    /// <summary>
    /// Name of the account
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

}

public class IntegrationApplicationStructure
{
    /// <summary>
    /// The id of the app
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The name of the app
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The icon hash of the app
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public string? Icon { get; internal set; }

    /// <summary>
    /// The description of the app
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// The summary of the app
    /// </summary>
    [JsonProperty("summary", Required = Required.Always)]
    public string Summary { get; internal set; } = null!;

    /// <summary>
    /// The bot associated with this application
    /// </summary>
    [JsonProperty("bot?", Required = Required.Always)]
    public user object Bot? { get; internal set; }

}

public class BanStructure
{
    /// <summary>
    /// The reason for the ban
    /// </summary>
    [JsonProperty("reason", Required = Required.AllowNull)]
    public string? Reason { get; internal set; }

    /// <summary>
    /// The banned user
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public user object User { get; internal set; }

}

public class WelcomeScreenStructure
{
    /// <summary>
    /// The server description shown in the welcome screen
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// The channels shown in the welcome screen, up to 5
    /// </summary>
    [JsonProperty("welcome_channels", Required = Required.Always)]
    public welcome screen channel objects[] WelcomeChannels { get; internal set; }

}

public class WelcomeScreenChannelStructure
{
    /// <summary>
    /// The channel's id
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// The description shown for the channel
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// The emoji id, if the emoji is custom
    /// </summary>
    [JsonProperty("emoji_id", Required = Required.AllowNull)]
    public ulong? EmojiId { get; internal set; }

    /// <summary>
    /// The emoji name if custom, the unicode character if standard, or null if no emoji is set
    /// </summary>
    [JsonProperty("emoji_name", Required = Required.AllowNull)]
    public string? EmojiName { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the guild (2-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Voice region id (deprecated)
    /// </summary>
    [JsonProperty("region?", Required = Required.AllowNull)]
    public string? Region? { get; internal set; }

    /// <summary>
    /// Base64 128x128 image for the guild icon
    /// </summary>
    [JsonProperty("icon?", Required = Required.Always)]
    public image data Icon? { get; internal set; }

    /// <summary>
    /// Verification level
    /// </summary>
    [JsonProperty("verification_level?", Required = Required.Always)]
    public int VerificationLevel? { get; internal set; }

    /// <summary>
    /// Default message notification level
    /// </summary>
    [JsonProperty("default_message_notifications?", Required = Required.Always)]
    public int DefaultMessageNotifications? { get; internal set; }

    /// <summary>
    /// Explicit content filter level
    /// </summary>
    [JsonProperty("explicit_content_filter?", Required = Required.Always)]
    public int ExplicitContentFilter? { get; internal set; }

    /// <summary>
    /// New guild roles
    /// </summary>
    [JsonProperty("roles?", Required = Required.Always)]
    public role objects[] Roles? { get; internal set; }

    /// <summary>
    /// New guild's channels
    /// </summary>
    [JsonProperty("channels?", Required = Required.Always)]
    public partial channel objects[] Channels? { get; internal set; }

    /// <summary>
    /// Id for afk channel
    /// </summary>
    [JsonProperty("afk_channel_id?", Required = Required.Always)]
    public ulong AfkChannelId? { get; internal set; }

    /// <summary>
    /// Afk timeout in seconds
    /// </summary>
    [JsonProperty("afk_timeout?", Required = Required.Always)]
    public int AfkTimeout? { get; internal set; }

    /// <summary>
    /// The id of the channel where guild notices such as welcome messages and boost events are posted
    /// </summary>
    [JsonProperty("system_channel_id?", Required = Required.Always)]
    public ulong SystemChannelId? { get; internal set; }

    /// <summary>
    /// System channel flags
    /// </summary>
    [JsonProperty("system_channel_flags?", Required = Required.Always)]
    public int SystemChannelFlags? { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// When true, will return approximate member and presence counts for the guild
    /// </summary>
    [JsonProperty("with_counts?", Required = Required.Always)]
    public bool WithCounts? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Guild name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Guild voice region id (deprecated)
    /// </summary>
    [JsonProperty("region", Required = Required.AllowNull)]
    public string? Region { get; internal set; }

    /// <summary>
    /// Verification level
    /// </summary>
    [JsonProperty("verification_level", Required = Required.AllowNull)]
    public int? VerificationLevel { get; internal set; }

    /// <summary>
    /// Default message notification level
    /// </summary>
    [JsonProperty("default_message_notifications", Required = Required.AllowNull)]
    public int? DefaultMessageNotifications { get; internal set; }

    /// <summary>
    /// Explicit content filter level
    /// </summary>
    [JsonProperty("explicit_content_filter", Required = Required.AllowNull)]
    public int? ExplicitContentFilter { get; internal set; }

    /// <summary>
    /// Id for afk channel
    /// </summary>
    [JsonProperty("afk_channel_id", Required = Required.AllowNull)]
    public ulong? AfkChannelId { get; internal set; }

    /// <summary>
    /// Afk timeout in seconds
    /// </summary>
    [JsonProperty("afk_timeout", Required = Required.Always)]
    public int AfkTimeout { get; internal set; }

    /// <summary>
    /// Base64 1024x1024 png/jpeg/gif image for the guild icon (can be animated gif when the server has the animated_icon feature)
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public image data? Icon { get; internal set; }

    /// <summary>
    /// User id to transfer guild ownership to (must be owner)
    /// </summary>
    [JsonProperty("owner_id", Required = Required.Always)]
    public ulong OwnerId { get; internal set; }

    /// <summary>
    /// Base64 16:9 png/jpeg image for the guild splash (when the server has the invite_splash feature)
    /// </summary>
    [JsonProperty("splash", Required = Required.AllowNull)]
    public image data? Splash { get; internal set; }

    /// <summary>
    /// Base64 16:9 png/jpeg image for the guild discovery splash (when the server has the discoverable feature)
    /// </summary>
    [JsonProperty("discovery_splash", Required = Required.AllowNull)]
    public image data? DiscoverySplash { get; internal set; }

    /// <summary>
    /// Base64 16:9 png/jpeg image for the guild banner (when the server has the banner feature)
    /// </summary>
    [JsonProperty("banner", Required = Required.AllowNull)]
    public image data? Banner { get; internal set; }

    /// <summary>
    /// The id of the channel where guild notices such as welcome messages and boost events are posted
    /// </summary>
    [JsonProperty("system_channel_id", Required = Required.AllowNull)]
    public ulong? SystemChannelId { get; internal set; }

    /// <summary>
    /// System channel flags
    /// </summary>
    [JsonProperty("system_channel_flags", Required = Required.Always)]
    public int SystemChannelFlags { get; internal set; }

    /// <summary>
    /// The id of the channel where community guilds display rules and/or guidelines
    /// </summary>
    [JsonProperty("rules_channel_id", Required = Required.AllowNull)]
    public ulong? RulesChannelId { get; internal set; }

    /// <summary>
    /// The id of the channel where admins and moderators of community guilds receive notices from discord
    /// </summary>
    [JsonProperty("public_updates_channel_id", Required = Required.AllowNull)]
    public ulong? PublicUpdatesChannelId { get; internal set; }

    /// <summary>
    /// The preferred locale of a community guild used in server discovery and notices from discord; defaults to "en-us"
    /// </summary>
    [JsonProperty("preferred_locale", Required = Required.AllowNull)]
    public string? PreferredLocale { get; internal set; }

    /// <summary>
    /// Enabled guild features
    /// </summary>
    [JsonProperty("features", Required = Required.Always)]
    public guild feature strings[] Features { get; internal set; }

    /// <summary>
    /// The description for the guild, if the guild is discoverable
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Whether the guild's boost progress bar should be enabled.
    /// </summary>
    [JsonProperty("premium_progress_bar_enabled", Required = Required.Always)]
    public bool PremiumProgressBarEnabled { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Channel name (1-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The type of channel
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// Channel topic (0-1024 characters)
    /// </summary>
    [JsonProperty("topic", Required = Required.Always)]
    public string Topic { get; internal set; } = null!;

    /// <summary>
    /// The bitrate (in bits) of the voice channel (voice only)
    /// </summary>
    [JsonProperty("bitrate", Required = Required.Always)]
    public int Bitrate { get; internal set; }

    /// <summary>
    /// The user limit of the voice channel (voice only)
    /// </summary>
    [JsonProperty("user_limit", Required = Required.Always)]
    public int UserLimit { get; internal set; }

    /// <summary>
    /// Amount of seconds a user has to wait before sending another message (0-21600); bots, as well as users with the permission manage_messages or manage_channel, are unaffected
    /// </summary>
    [JsonProperty("rate_limit_per_user", Required = Required.Always)]
    public int RateLimitPerUser { get; internal set; }

    /// <summary>
    /// Sorting position of the channel
    /// </summary>
    [JsonProperty("position", Required = Required.Always)]
    public int Position { get; internal set; }

    /// <summary>
    /// The channel's permission overwrites
    /// </summary>
    [JsonProperty("permission_overwrites", Required = Required.Always)]
    public overwrite objects[] PermissionOverwrites { get; internal set; }

    /// <summary>
    /// Id of the parent category for a channel
    /// </summary>
    [JsonProperty("parent_id", Required = Required.Always)]
    public ulong ParentId { get; internal set; }

    /// <summary>
    /// Whether the channel is nsfw
    /// </summary>
    [JsonProperty("nsfw", Required = Required.Always)]
    public bool Nsfw { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Channel id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Sorting position of the channel
    /// </summary>
    [JsonProperty("position", Required = Required.AllowNull)]
    public int? Position { get; internal set; }

    /// <summary>
    /// Syncs the permission overwrites with the new parent, if moving to a new category
    /// </summary>
    [JsonProperty("lock_permissions", Required = Required.AllowNull)]
    public bool? LockPermissions { get; internal set; }

    /// <summary>
    /// The new parent id for the channel that is moved
    /// </summary>
    [JsonProperty("parent_id", Required = Required.AllowNull)]
    public ulong? ParentId { get; internal set; }

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

}

public class QueryStringParams
{
    /// <summary>
    /// Max number of members to return (1-1000)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

    /// <summary>
    /// The highest user id in the previous page
    /// </summary>
    [JsonProperty("after", Required = Required.Always)]
    public ulong After { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Query string to match username(s) and nickname(s) against.
    /// </summary>
    [JsonProperty("query", Required = Required.Always)]
    public string Query { get; internal set; } = null!;

    /// <summary>
    /// Max number of members to return (1-1000)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// An oauth2 access token granted with the guilds.join to the bot's application for the user you want to add to the guild
    /// </summary>
    [JsonProperty("access_token", Required = Required.Always)]
    public string AccessToken { get; internal set; } = null!;

    /// <summary>
    /// Value to set user's nickname to
    /// </summary>
    [JsonProperty("nick", Required = Required.Always)]
    public string Nick { get; internal set; } = null!;

    /// <summary>
    /// Array of role ids the member is assigned
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public snowflakes[] Roles { get; internal set; }

    /// <summary>
    /// Whether the user is muted in voice channels
    /// </summary>
    [JsonProperty("mute", Required = Required.Always)]
    public bool Mute { get; internal set; }

    /// <summary>
    /// Whether the user is deafened in voice channels
    /// </summary>
    [JsonProperty("deaf", Required = Required.Always)]
    public bool Deaf { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Value to set user's nickname to
    /// </summary>
    [JsonProperty("nick", Required = Required.Always)]
    public string Nick { get; internal set; } = null!;

    /// <summary>
    /// Array of role ids the member is assigned
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public snowflakes[] Roles { get; internal set; }

    /// <summary>
    /// Whether the user is muted in voice channels. will throw a 400 if the user is not in a voice channel
    /// </summary>
    [JsonProperty("mute", Required = Required.Always)]
    public bool Mute { get; internal set; }

    /// <summary>
    /// Whether the user is deafened in voice channels. will throw a 400 if the user is not in a voice channel
    /// </summary>
    [JsonProperty("deaf", Required = Required.Always)]
    public bool Deaf { get; internal set; }

    /// <summary>
    /// Id of channel to move user to (if they are connected to voice)
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// When the user's timeout will expire and the user will be able to communicate in the guild again (up to 28 days in the future), set to null to remove timeout
    /// </summary>
    [JsonProperty("communication_disabled_until", Required = Required.AllowNull)]
    public ISO8601 timestamp? CommunicationDisabledUntil { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Value to set user's nickname to
    /// </summary>
    [JsonProperty("nick?", Required = Required.AllowNull)]
    public string? Nick? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Value to set user's nickname to
    /// </summary>
    [JsonProperty("nick?", Required = Required.AllowNull)]
    public string? Nick? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Number of days to delete messages for (0-7)
    /// </summary>
    [JsonProperty("delete_message_days?", Required = Required.Always)]
    public int DeleteMessageDays? { get; internal set; }

    /// <summary>
    /// Reason for the ban (deprecated)
    /// </summary>
    [JsonProperty("reason?", Required = Required.Always)]
    public string Reason? { get; internal set; } = null!;

}

public class JsonParams
{
    /// <summary>
    /// Name of the role
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Bitwise value of the enabled/disabled permissions
    /// </summary>
    [JsonProperty("permissions", Required = Required.Always)]
    public string Permissions { get; internal set; } = null!;

    /// <summary>
    /// Rgb color value
    /// </summary>
    [JsonProperty("color", Required = Required.Always)]
    public int Color { get; internal set; }

    /// <summary>
    /// Whether the role should be displayed separately in the sidebar
    /// </summary>
    [JsonProperty("hoist", Required = Required.Always)]
    public bool Hoist { get; internal set; }

    /// <summary>
    /// The role's icon image (if the guild has the role_icons feature)
    /// </summary>
    [JsonProperty("icon", Required = Required.Always)]
    public image data Icon { get; internal set; }

    /// <summary>
    /// The role's unicode emoji as a standard emoji (if the guild has the role_icons feature)
    /// </summary>
    [JsonProperty("unicode_emoji", Required = Required.Always)]
    public string UnicodeEmoji { get; internal set; } = null!;

    /// <summary>
    /// Whether the role should be mentionable
    /// </summary>
    [JsonProperty("mentionable", Required = Required.Always)]
    public bool Mentionable { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Role
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Sorting position of the role
    /// </summary>
    [JsonProperty("position?", Required = Required.AllowNull)]
    public int? Position? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the role
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Bitwise value of the enabled/disabled permissions
    /// </summary>
    [JsonProperty("permissions", Required = Required.Always)]
    public string Permissions { get; internal set; } = null!;

    /// <summary>
    /// Rgb color value
    /// </summary>
    [JsonProperty("color", Required = Required.Always)]
    public int Color { get; internal set; }

    /// <summary>
    /// Whether the role should be displayed separately in the sidebar
    /// </summary>
    [JsonProperty("hoist", Required = Required.Always)]
    public bool Hoist { get; internal set; }

    /// <summary>
    /// The role's icon image (if the guild has the role_icons feature)
    /// </summary>
    [JsonProperty("icon", Required = Required.Always)]
    public image data Icon { get; internal set; }

    /// <summary>
    /// The role's unicode emoji as a standard emoji (if the guild has the role_icons feature)
    /// </summary>
    [JsonProperty("unicode_emoji", Required = Required.Always)]
    public string UnicodeEmoji { get; internal set; } = null!;

    /// <summary>
    /// Whether the role should be mentionable
    /// </summary>
    [JsonProperty("mentionable", Required = Required.Always)]
    public bool Mentionable { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Number of days to count prune for (1-30)
    /// </summary>
    [JsonProperty("days", Required = Required.Always)]
    public int Days { get; internal set; }

    /// <summary>
    /// Role(s) to include
    /// </summary>
    [JsonProperty("include_roles", Required = Required.Always)]
    public string; comma-delimited array of snowflakes IncludeRoles { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Number of days to prune (1-30)
    /// </summary>
    [JsonProperty("days", Required = Required.Always)]
    public int Days { get; internal set; }

    /// <summary>
    /// Whether 'pruned' is returned, discouraged for large guilds
    /// </summary>
    [JsonProperty("compute_prune_count", Required = Required.Always)]
    public bool ComputePruneCount { get; internal set; }

    /// <summary>
    /// Role(s) to include
    /// </summary>
    [JsonProperty("include_roles", Required = Required.Always)]
    public snowflakes[] IncludeRoles { get; internal set; }

    /// <summary>
    /// Reason for the prune (deprecated)
    /// </summary>
    [JsonProperty("reason?", Required = Required.Always)]
    public string Reason? { get; internal set; } = null!;

}

public class QueryStringParams
{
    /// <summary>
    /// Style of the widget image returned (see below)
    /// </summary>
    [JsonProperty("style", Required = Required.Always)]
    public string Style { get; internal set; } = null!;

}

public class JsonParams
{
    /// <summary>
    /// Whether the welcome screen is enabled
    /// </summary>
    [JsonProperty("enabled", Required = Required.Always)]
    public bool Enabled { get; internal set; }

    /// <summary>
    /// Channels linked in the welcome screen and their display options
    /// </summary>
    [JsonProperty("welcome_channels", Required = Required.Always)]
    public welcome screen channel objects[] WelcomeChannels { get; internal set; }

    /// <summary>
    /// The server description to show in the welcome screen
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

}

public class JsonParams
{
    /// <summary>
    /// The id of the channel the user is currently in
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// Toggles the user's suppress state
    /// </summary>
    [JsonProperty("suppress?", Required = Required.Always)]
    public bool Suppress? { get; internal set; }

    /// <summary>
    /// Sets the user's request to speak
    /// </summary>
    [JsonProperty("request_to_speak_timestamp?", Required = Required.AllowNull)]
    public ISO8601 timestamp? RequestToSpeakTimestamp? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The id of the channel the user is currently in
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// Toggles the user's suppress state
    /// </summary>
    [JsonProperty("suppress?", Required = Required.Always)]
    public bool Suppress? { get; internal set; }

}

