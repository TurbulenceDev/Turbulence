using Newtonsoft.Json;

namespace Turbulence.API.Models.Guild;

/// <summary>
/// Taken from https://discord.com/developers/docs/resources/user#user-object-premium-types
/// </summary>
public class Guild : Snowflake
{
    /// <summary>
    /// Guild name (2-100 characters, excluding trailing and leading whitespace)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Icon hash
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public string? Icon { get; set; }

    /// <summary>
    /// Icon hash, returned when in the template object
    /// </summary>
    [JsonProperty("icon_hash")]
    public string? IconHash { get; set; }

    /// <summary>
    /// Splash hash
    /// </summary>
    [JsonProperty("splash", Required = Required.AllowNull)]
    public string? Splash { get; set; }

    /// <summary>
    /// Discovery splash hash; only present for guilds with the "discoverable" feature
    /// </summary>
    [JsonProperty("discovery_splash", Required = Required.AllowNull)]
    public string? DiscoverySplash { get; set; }

    /// <summary>
    /// True if the user is the owner of the guild
    /// </summary>
    [JsonProperty("owner", Required = Required.DisallowNull)]
    public bool Owner { get; set; }

    /// <summary>
    /// Id of owner
    /// </summary>
    [JsonProperty("owner_id", Required = Required.Always)]
    public ulong OwnerId { get; set; }

    /// <summary>
    /// Total permissions for the user in the guild (excludes overwrites)
    /// </summary>
    [JsonProperty("permissions", Required = Required.DisallowNull)]
    public string Permissions { get; set; } = null!;

    /// <summary>
    /// Voice region id for the guild (deprecated)
    /// </summary>
    [JsonProperty("region")]
    public string? Region { get; set; }

    /// <summary>
    /// Id of afk channel
    /// </summary>
    [JsonProperty("afk_channel_id", Required = Required.AllowNull)]
    public ulong? AfkChannelId { get; set; }

    /// <summary>
    /// Afk timeout in seconds
    /// </summary>
    [JsonProperty("afk_timeout", Required = Required.Always)]
    public int AfkTimeout { get; set; }

    /// <summary>
    /// True if the server widget is enabled
    /// </summary>
    [JsonProperty("widget_enabled", Required = Required.DisallowNull)]
    public bool WidgetEnabled { get; set; }

    /// <summary>
    /// The channel id that the widget will generate an invite to, or null if set to no invite
    /// </summary>
    [JsonProperty("widget_channel_id")]
    public ulong? WidgetChannelId { get; set; }

    /// <summary>
    /// Verification level required for the guild
    /// </summary>
    [JsonProperty("verification_level", Required = Required.Always)]
    public int VerificationLevel { get; set; }

    /// <summary>
    /// Default message notifications level
    /// </summary>
    [JsonProperty("default_message_notifications", Required = Required.Always)]
    public int DefaultMessageNotifications { get; set; }

    /// <summary>
    /// Explicit content filter level
    /// </summary>
    [JsonProperty("explicit_content_filter", Required = Required.Always)]
    public int ExplicitContentFilter { get; set; }

    /// <summary>
    /// Roles in the guild
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public Role[] Roles { get; set; } = null!;

    /// <summary>
    /// Custom guild emojis
    /// </summary>
    [JsonProperty("emojis", Required = Required.Always)]
    public Emoji[] Emojis { get; set; } = null!;

    /// <summary>
    /// Enabled guild features
    /// </summary>
    [JsonProperty("features", Required = Required.Always)]
    public string[] Features { get; set; } = null!;

    /// <summary>
    /// Required mfa level for the guild
    /// </summary>
    [JsonProperty("mfa_level", Required = Required.Always)]
    public int MfaLevel { get; set; }

    /// <summary>
    /// Application id of the guild creator if it is bot-created
    /// </summary>
    [JsonProperty("application_id", Required = Required.AllowNull)]
    public ulong? ApplicationId { get; set; }

    /// <summary>
    /// The id of the channel where guild notices such as welcome messages and boost events are posted
    /// </summary>
    [JsonProperty("system_channel_id", Required = Required.AllowNull)]
    public ulong? SystemChannelId { get; set; }

    /// <summary>
    /// System channel flags
    /// </summary>
    [JsonProperty("system_channel_flags", Required = Required.Always)]
    public int SystemChannelFlags { get; set; }

    /// <summary>
    /// The id of the channel where community guilds can display rules and/or guidelines
    /// </summary>
    [JsonProperty("rules_channel_id", Required = Required.AllowNull)]
    public ulong? RulesChannelId { get; set; }

    /// <summary>
    /// When this guild was joined at. ISO8601 timestamp
    /// </summary>
    [JsonProperty("joined_at", Required = Required.DisallowNull)]
    public string JoinedAt { get; set; }

    /// <summary>
    /// True if this is considered a large guild
    /// </summary>
    [JsonProperty("large", Required = Required.DisallowNull)]
    public bool Large { get; set; }

    /// <summary>
    /// True if this guild is unavailable due to an outage
    /// </summary>
    [JsonProperty("unavailable", Required = Required.DisallowNull)]
    public bool Unavailable { get; set; }

    /// <summary>
    /// Total number of members in this guild
    /// </summary>
    [JsonProperty("member_count", Required = Required.DisallowNull)]
    public int MemberCount { get; set; }

    /// <summary>
    /// States of members currently in voice channels; lacks the guild_id key
    /// </summary>
    [JsonProperty("voice_states", Required = Required.DisallowNull)]
    public VoiceState[] VoiceStates { get; set; } = null!;

    /// <summary>
    /// Users in the guild
    /// </summary>
    [JsonProperty("members", Required = Required.DisallowNull)]
    public GuildMember[] Members { get; set; } = null!;

    /// <summary>
    /// Channels in the guild
    /// </summary>
    [JsonProperty("channels", Required = Required.DisallowNull)]
    public Channel[] Channels { get; set; } = null!;

    /// <summary>
    /// All active threads in the guild that current user has permission to view
    /// </summary>
    [JsonProperty("threads", Required = Required.DisallowNull)]
    public Channel[] Threads { get; set; } = null!;

    /// <summary>
    /// Presences of the members in the guild, will only include non-offline members if the size is greater than large threshold
    /// </summary>
    [JsonProperty("presences", Required = Required.DisallowNull)]
    public /*partial presence update objects*/ object[] Presences { get; set; }

    /// <summary>
    /// The maximum number of presences for the guild (null is always returned, apart from the largest of guilds)
    /// </summary>
    [JsonProperty("max_presences")]
    public int? MaxPresences { get; set; }

    /// <summary>
    /// The maximum number of members for the guild
    /// </summary>
    [JsonProperty("max_members", Required = Required.DisallowNull)]
    public int MaxMembers { get; set; }

    /// <summary>
    /// The vanity url code for the guild
    /// </summary>
    [JsonProperty("vanity_url_code", Required = Required.AllowNull)]
    public string? VanityUrlCode { get; set; }

    /// <summary>
    /// The description of a community guild
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Banner hash
    /// </summary>
    [JsonProperty("banner", Required = Required.AllowNull)]
    public string? Banner { get; set; }

    /// <summary>
    /// Premium tier (server boost level)
    /// </summary>
    [JsonProperty("premium_tier", Required = Required.Always)]
    public int PremiumTier { get; set; }

    /// <summary>
    /// The number of boosts this guild currently has
    /// </summary>
    [JsonProperty("premium_subscription_count", Required = Required.DisallowNull)]
    public int PremiumSubscriptionCount { get; set; }

    /// <summary>
    /// The preferred locale of a community guild; used in server discovery and notices from discord; defaults to "en-us"
    /// </summary>
    [JsonProperty("preferred_locale", Required = Required.Always)]
    public string PreferredLocale { get; set; } = null!;

    /// <summary>
    /// The id of the channel where admins and moderators of community guilds receive notices from discord
    /// </summary>
    [JsonProperty("public_updates_channel_id", Required = Required.AllowNull)]
    public ulong? PublicUpdatesChannelId { get; set; }

    /// <summary>
    /// The maximum amount of users in a video channel
    /// </summary>
    [JsonProperty("max_video_channel_users", Required = Required.DisallowNull)]
    public int MaxVideoChannelUsers { get; set; }

    /// <summary>
    /// Approximate number of members in this guild, returned from the get /guilds/<id> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_member_count", Required = Required.DisallowNull)]
    public int ApproximateMemberCount { get; set; }

    /// <summary>
    /// Approximate number of non-offline members in this guild, returned from the get /guilds/<id> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_presence_count", Required = Required.DisallowNull)]
    public int ApproximatePresenceCount { get; set; }

    /// <summary>
    /// The welcome screen of a community guild, shown to new members, returned in an invite's guild object
    /// </summary>
    [JsonProperty("welcome_screen", Required = Required.DisallowNull)]
    public /*welcome screen*/ object WelcomeScreen { get; set; }

    /// <summary>
    /// Guild nsfw level
    /// </summary>
    [JsonProperty("nsfw_level", Required = Required.Always)]
    public int NsfwLevel { get; set; }

    /// <summary>
    /// Stage instances in the guild
    /// </summary>
    [JsonProperty("stage_instances", Required = Required.DisallowNull)]
    public /*stage instance objects*/ object[] StageInstances { get; set; }

    /// <summary>
    /// Custom guild stickers
    /// </summary>
    [JsonProperty("stickers", Required = Required.DisallowNull)]
    public /*array of sticker objects*/ object[] Stickers { get; set; }

    /// <summary>
    /// The scheduled events in the guild
    /// </summary>
    [JsonProperty("guild_scheduled_events", Required = Required.DisallowNull)]
    public /*guild scheduled event objects*/ object[] GuildScheduledEvents { get; set; }

    /// <summary>
    /// Whether the guild has the boost progress bar enabled
    /// </summary>
    [JsonProperty("premium_progress_bar_enabled", Required = Required.Always)]
    public bool PremiumProgressBarEnabled { get; set; }
}