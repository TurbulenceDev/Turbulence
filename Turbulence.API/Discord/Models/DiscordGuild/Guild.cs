using Turbulence.API.Discord.Models.DiscordEmoji;
using Turbulence.API.Discord.Models.DiscordPermissions;
using Turbulence.API.Discord.Models.DiscordSticker;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGuild;

/// <summary>
/// Guilds in Discord represent an isolated collection of users and channels, and are often referred to as "servers"
/// in the UI.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/guild#guild-object">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Guild.md#guild-object">GitHub</a>.
/// </summary>
public record Guild {
	/// <summary>
	/// Guild snowflake ID.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong Id { get; init; }

	/// <summary>
	/// Guild name (2-100 characters, excluding trailing and leading whitespace).
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Icon hash</a>.
	/// </summary>
	[JsonPropertyName("icon")]
	public required string? Icon { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Icon hash</a>, returned when in the
	/// template object.
	/// </summary>
	[JsonPropertyName("icon_hash")]
	public string? IconHash { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Splash hash</a>.
	/// </summary>
	[JsonPropertyName("splash")]
	public required string? Splash { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Discovery splash hash</a>; only present
	/// for guilds with the "DISCOVERABLE" feature.
	/// </summary>
	[JsonPropertyName("discovery_splash")]
	public required string? DiscoverySplash { get; init; }

	/// <summary>
	/// True if <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">the user</a> is the
	/// owner of the guild.
	///
	/// This field is only sent when using the
	/// <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">GET Current User Guilds</a>
	/// endpoint and is relative to the requested user.
	/// </summary>
	[JsonPropertyName("owner")]
	public required bool Owner { get; init; }

	/// <summary>
	/// Snowflake ID of owner.
	/// </summary>
	[JsonPropertyName("owner_id")]
	public required ulong OwnerId { get; init; }

	/// <summary>
	/// Total permissions for <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">the
	/// user</a> in the guild (excludes overwrites).
	///
	/// This field is only sent when using the
	/// <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">GET Current User Guilds</a>
	/// endpoint and is relative to the requested user.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required string Permissions { get; init; }

	[Obsolete("Use Channel.RtcRegion instead")]
	[JsonPropertyName("region")]
	public required string? Region { get; init; }

	/// <summary>
	/// Snowflake ID of AFK channel.
	/// </summary>
	[JsonPropertyName("afk_channel_id")]
	public required ulong? AfkChannelId { get; init; }

	/// <summary>
	/// AFK timeout in seconds.
	/// </summary>
	[JsonPropertyName("afk_timeout")]
	public required int AfkTimeout { get; init; }

	/// <summary>
	/// True if the server widget is enabled.
	/// </summary>
	[JsonPropertyName("widget_enabled")]
	public bool? WidgetEnabled { get; init; }

	/// <summary>
	/// The channel snowflake ID that the widget will generate an invite to, or <c>null</c> if set to no invite.
	/// </summary>
	[JsonPropertyName("widget_channel_id")]
	public ulong? WidgetChannelId { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-verification-level">Verification level
	/// </a> required for the guild.
	/// </summary>
	[JsonPropertyName("verification_level")]
	public required int VerificationLevel { get; init; }

	// TODO: Make enum
	/// <summary>
	/// Default
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-default-message-notification-level">
	/// message notifications level</a>.
	/// </summary>
	[JsonPropertyName("default_message_notifications")]
	public required int DefaultMessageNotifications { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-explicit-content-filter-level">
	/// Explicit content filter level</a>.
	/// </summary>
	[JsonPropertyName("explicit_content_filter")]
	public required int ExplicitContentFilter { get; init; }

	/// <summary>
	/// Roles in the guild.
	/// </summary>
	[JsonPropertyName("roles")]
	public required Role[] Roles { get; init; }

	/// <summary>
	/// Custom guild emojis.
	/// </summary>
	[JsonPropertyName("emojis")]
	public required Emoji[] Emojis { get; init; }

	/// <summary>
	/// Enabled <a href="https://discord.com/developers/docs/resources/guild#guild-object-guild-features">guild
	/// features</a>.
	/// </summary>
	[JsonPropertyName("features")]
	public required string[] Features { get; init; }

	// TODO: Make enum
	/// <summary>
	/// Required <a href="https://discord.com/developers/docs/resources/guild#guild-object-mfa-level">MFA level</a> for
	/// the guild.
	/// </summary>
	[JsonPropertyName("mfa_level")]
	public required int MfaLevel { get; init; }

	/// <summary>
	/// Application snowflake ID of the guild creator if it is bot-created.
	/// </summary>
	[JsonPropertyName("application_id")]
	public required ulong? ApplicationId { get; init; }

	/// <summary>
	/// The snowflake ID of the channel where guild notices such as welcome messages and boost events are posted.
	/// </summary>
	[JsonPropertyName("system_channel_id")]
	public required ulong? SystemChannelId { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-system-channel-flags">System channel
	/// flags</a>.
	/// </summary>
	[JsonPropertyName("system_channel_flags")]
	public required int SystemChannelFlags { get; init; }

	/// <summary>
	/// The snowflake ID of the channel where Community guilds can display rules and/or guidelines.
	/// </summary>
	[JsonPropertyName("rules_channel_id")]
	public required ulong? RulesChannelId { get; init; }

	/// <summary>
	/// The maximum number of presences for the guild (<c>null</c> is always returned, apart from the largest of
	/// guilds).
	/// </summary>
	[JsonPropertyName("max_presences")]
	public int? MaxPresences { get; init; }

	/// <summary>
	/// The maximum number of members for the guild.
	/// </summary>
	[JsonPropertyName("max_members")]
	public int? MaxMembers { get; init; }

	/// <summary>
	/// The vanity URL code for the guild.
	/// </summary>
	[JsonPropertyName("vanity_url_code")]
	public required Uri? VanityUrlCode { get; init; }

	/// <summary>
	/// The description of a guild.
	/// </summary>
	[JsonPropertyName("description")]
	public required string? Description { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">Banner hash</a>.
	/// </summary>
	[JsonPropertyName("banner")]
	public required string? Banner { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-premium-tier">Premium tier</a>
	/// (Server Boost level).
	/// </summary>
	[JsonPropertyName("premium_tier")]
	public required int PremiumTier { get; init; }

	/// <summary>
	/// The number of boosts this guild currently has.
	/// </summary>
	[JsonPropertyName("premium_subscription_count")]
	public int? PremiumSubscriptionCount { get; init; }

	/// <summary>
	/// The preferred <a href="https://discord.com/developers/docs/reference#locales">locale</a> of a Community guild;
	/// used in server discovery and notices from Discord, and sent in interactions; defaults to "en-US".
	/// </summary>
	[JsonPropertyName("preferred_locale")]
	public required string PreferredLocale { get; init; }

	/// <summary>
	/// The snowflake ID of the channel where admins and moderators of Community guilds receive notices from Discord.
	/// </summary>
	[JsonPropertyName("public_updates_channel_id")]
	public required ulong? PublicUpdatesChannelId { get; init; }

	/// <summary>
	/// The maximum amount of users in a video channel.
	/// </summary>
	[JsonPropertyName("max_video_channel_users")]
	public int? MaxVideoChannelUsers { get; init; }

	/// <summary>
	/// The maximum amount of users in a stage video channel.
	/// </summary>
	[JsonPropertyName("max_stage_video_channel_users")]
	public int? MaxStageVideoChannelUsers { get; init; }

	/// <summary>
	/// Approximate number of members in this guild, returned from the <c>GET /guilds/&lt;id&gt;</c> and
	/// <c>/users/@me/guilds</c> endpoints when <c>with_counts</c> is <c>true</c>.
	/// </summary>
	[JsonPropertyName("approximate_member_count")]
	public int? ApproximateMemberCount { get; init; }

	/// <summary>
	/// Approximate number of non-offline members in this guild, returned from the <c>GET /guilds/&lt;id&gt;</c> and
	/// <c>/users/@me/guilds</c>  endpoints when <c>with_counts</c> is <c>true</c>.
	/// </summary>
	[JsonPropertyName("approximate_presence_count")]
	public int? ApproximatePresenceCount { get; init; }

	/// <summary>
	/// The welcome screen of a Community guild, shown to new members, returned in an
	/// <a href="https://discord.com/developers/docs/resources/invite#invite-object">Invite</a>'s guild object.
	/// </summary>
	[JsonPropertyName("welcome_screen")]
	public WelcomeScreen? WelcomeScreen { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/guild#guild-object-guild-nsfw-level">Guild NSFW level</a>.
	/// </summary>
	[JsonPropertyName("nsfw_level")]
	public required int NsfwLevel { get; init; }

	/// <summary>
	/// Custom guild stickers.
	/// </summary>
	[JsonPropertyName("stickers")]
	public Sticker[]? Stickers { get; init; }

	/// <summary>
	/// Whether the guild has the boost progress bar enabled.
	/// </summary>
	[JsonPropertyName("premium_progress_bar_enabled")]
	public required bool PremiumProgressBarEnabled { get; init; }

	/// <summary>
	/// The snowflake ID of the channel where admins and moderators of Community guilds receive safety alerts from
	/// Discord.
	/// </summary>
	[JsonPropertyName("safety_alerts_channel_id")]
	public required ulong? SafetyAlertsChannelId { get; init; }
}
