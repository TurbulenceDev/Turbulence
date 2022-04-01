public class GatewayPayload  # Remove "Structure" suffix
{
    /// <summary>
    /// Opcode for the payload.
    /// </summary>
    [JsonProperty("op", Required = Required.Always)]
    public int Opcode { get; set; }

    /// <summary>
    /// Event data.
    /// </summary>
    [JsonProperty("d", Required = Required.AllowNull)]
    public object? Data { get; set; }

    /// <summary>
    /// Sequence number, used for resuming sessions and heartbeats.
    /// </summary>
    [JsonProperty("s", Required = Required.AllowNull)]
    public int? Sequence { get; set; }

    /// <summary>
    /// The event name for this payload.
    /// </summary>
    [JsonProperty("t", Required = Required.AllowNull)]
    public string? EventData { get; set; }
}

public class GatewayUrlQueryStringParams  # Remove "Structure" suffix
{
    /// <summary>
    /// Gateway version to use.
    /// </summary>
    [JsonProperty("v", Required = Required.Always)]
    public int V { get; set; }

    /// <summary>
    /// The encoding of received gateway packets.
    /// </summary>
    [JsonProperty("encoding", Required = Required.Always)]
    public string Encoding { get; set; } = null!;

    /// <summary>
    /// The (optional) compression of gateway packets.
    /// </summary>
    [JsonProperty("compress", Required = Required.DisallowNull)]
    public string Compress { get; set; } = null!;
}

public class Identify  # Remove "Structure" suffix
{
    /// <summary>
    /// Authentication token.
    /// </summary>
    [JsonProperty("token", Required = Required.Always)]
    public string Token { get; set; } = null!;

    /// <summary>
    /// Connection properties.
    /// </summary>
    [JsonProperty("properties", Required = Required.Always)]
    public object Properties { get; set; }

    /// <summary>
    /// Whether this connection supports compression of packets.
    /// </summary>
    [JsonProperty("compress", Required = Required.DisallowNull)]
    public bool Compress { get; set; }

    /// <summary>
    /// Value between 50 and 250, total number of members where the gateway will stop sending offline members in the guild member list.
    /// </summary>
    [JsonProperty("large_threshold", Required = Required.DisallowNull)]
    public int LargeThreshold { get; set; }

    /// <summary>
    /// Used for guild sharding.
    /// </summary>
    [JsonProperty("shard", Required = Required.DisallowNull)]
    public object Shard { get; set; }

    /// <summary>
    /// Presence structure for initial presence information.
    /// </summary>
    [JsonProperty("presence", Required = Required.DisallowNull)]
    public UpdatePresence Presence { get; set; }

    /// <summary>
    /// The gateway intents you wish to receive.
    /// </summary>
    [JsonProperty("intents", Required = Required.Always)]
    public int Intents { get; set; }
}

public class IdentifyConnectionProperties  # Remove "Structure" suffix
{
    /// <summary>
    /// Your operating system.
    /// </summary>
    [JsonProperty("$os", Required = Required.Always)]
    public string $os { get; set; } = null!;

    /// <summary>
    /// Your library name.
    /// </summary>
    [JsonProperty("$browser", Required = Required.Always)]
    public string $browser { get; set; } = null!;

    /// <summary>
    /// Your library name.
    /// </summary>
    [JsonProperty("$device", Required = Required.Always)]
    public string $device { get; set; } = null!;
}

public class Resume  # Remove "Structure" suffix
{
    /// <summary>
    /// Session token.
    /// </summary>
    [JsonProperty("token", Required = Required.Always)]
    public string Token { get; set; } = null!;

    /// <summary>
    /// Session id.
    /// </summary>
    [JsonProperty("session_id", Required = Required.Always)]
    public string SessionId { get; set; } = null!;

    /// <summary>
    /// Last sequence number received.
    /// </summary>
    [JsonProperty("seq", Required = Required.Always)]
    public int Seq { get; set; }
}

public class GuildRequestMembers  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild to get members for.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// String that username starts with, or an empty string to return all members.
    /// </summary>
    [JsonProperty("query", Required = Required.DisallowNull)]
    public string Query { get; set; } = null!;

    /// <summary>
    /// Maximum number of members to send matching the query; a limit of 0 can be used with an empty string query to return all members.
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; set; }

    /// <summary>
    /// Used to specify if we want the presences of the matched members.
    /// </summary>
    [JsonProperty("presences", Required = Required.DisallowNull)]
    public bool Presences { get; set; }

    /// <summary>
    /// Used to specify which users you wish to fetch.
    /// </summary>
    [JsonProperty("user_ids", Required = Required.DisallowNull)]
    public snowflake or array of snowflakes UserIds { get; set; }

    /// <summary>
    /// Nonce to identify the guild members chunk response.
    /// </summary>
    [JsonProperty("nonce", Required = Required.DisallowNull)]
    public string Nonce { get; set; } = null!;
}

public class GatewayVoiceStateUpdate  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Id of the voice channel client wants to join (null if disconnecting).
    /// </summary>
    [JsonProperty("channel_id", Required = Required.AllowNull)]
    public ulong? ChannelId { get; set; }

    /// <summary>
    /// Is the client muted.
    /// </summary>
    [JsonProperty("self_mute", Required = Required.Always)]
    public bool SelfMute { get; set; }

    /// <summary>
    /// Is the client deafened.
    /// </summary>
    [JsonProperty("self_deaf", Required = Required.Always)]
    public bool SelfDeaf { get; set; }
}

public class GatewayPresenceUpdate  # Remove "Structure" suffix
{
    /// <summary>
    /// Unix time (in milliseconds) of when the client went idle, or null if the client is not idle.
    /// </summary>
    [JsonProperty("since", Required = Required.AllowNull)]
    public int? Since { get; set; }

    /// <summary>
    /// The user's activities.
    /// </summary>
    [JsonProperty("activities", Required = Required.Always)]
    public Activity[] Activities { get; set; }

    /// <summary>
    /// The user's new status.
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Whether or not the client is afk.
    /// </summary>
    [JsonProperty("afk", Required = Required.Always)]
    public bool Afk { get; set; }
}

public class Hello  # Remove "Structure" suffix
{
    /// <summary>
    /// The interval (in milliseconds) the client should heartbeat with.
    /// </summary>
    [JsonProperty("heartbeat_interval", Required = Required.Always)]
    public int HeartbeatInterval { get; set; }
}

public class ReadyEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Gateway version.
    /// </summary>
    [JsonProperty("v", Required = Required.Always)]
    public int V { get; set; }

    /// <summary>
    /// Information about the user including email.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }

    /// <summary>
    /// The guilds the user is in.
    /// </summary>
    [JsonProperty("guilds", Required = Required.Always)]
    public UnavailableGuild[] Guilds { get; set; }

    /// <summary>
    /// Used for resuming connections.
    /// </summary>
    [JsonProperty("session_id", Required = Required.Always)]
    public string SessionId { get; set; } = null!;

    /// <summary>
    /// The shard information associated with this session, if sent when identifying.
    /// </summary>
    [JsonProperty("shard", Required = Required.DisallowNull)]
    public object Shard { get; set; }

    /// <summary>
    /// Contains id and flags.
    /// </summary>
    [JsonProperty("application", Required = Required.Always)]
    public PartialApplication Application { get; set; }
}

public class ThreadListSyncEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The parent channel ids whose threads are being synced.  if omitted, then threads were synced for the entire guild.  this array may contain channel_ids that have no active threads as well, so you know to clear that data..
    /// </summary>
    [JsonProperty("channel_ids", Required = Required.DisallowNull)]
    public Snowflake[] ChannelIds { get; set; }

    /// <summary>
    /// All active threads in the given channels that the current user can access.
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public Channel[] Threads { get; set; }

    /// <summary>
    /// All thread member objects from the synced threads for the current user, indicating which threads the current user has been added to.
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public ThreadMember[] Members { get; set; }
}

public class ThreadMemberUpdateEventExtraFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class ThreadMembersUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the thread.
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The approximate number of members in the thread, capped at 50.
    /// </summary>
    [JsonProperty("member_count", Required = Required.Always)]
    public int MemberCount { get; set; }

    /// <summary>
    /// The users who were added to the thread.
    /// </summary>
    [JsonProperty("added_members", Required = Required.DisallowNull)]
    public ThreadMember[] AddedMembers { get; set; }

    /// <summary>
    /// The id of the users who were removed from the thread.
    /// </summary>
    [JsonProperty("removed_member_ids", Required = Required.DisallowNull)]
    public Snowflake[] RemovedMemberIds { get; set; }
}

public class ChannelPinsUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The time at which the most recent pinned message was pinned.
    /// </summary>
    [JsonProperty("last_pin_timestamp")]
    public string? LastPinTimestamp { get; set; }
}

public class GuildBanAddEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The banned user.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }
}

public class GuildBanRemoveEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The unbanned user.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }
}

public class GuildEmojisUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Array of emojis.
    /// </summary>
    [JsonProperty("emojis", Required = Required.Always)]
    public object Emojis { get; set; }
}

public class GuildStickersUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Array of stickers.
    /// </summary>
    [JsonProperty("stickers", Required = Required.Always)]
    public object Stickers { get; set; }
}

public class GuildIntegrationsUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild whose integrations were updated.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class GuildMemberAddExtraFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class GuildMemberRemoveEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The user who was removed.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }
}

public class GuildMemberUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// User role ids.
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public Snowflake[] Roles { get; set; }

    /// <summary>
    /// The user.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }

    /// <summary>
    /// Nickname of the user in the guild.
    /// </summary>
    [JsonProperty("nick")]
    public string? Nick { get; set; }

    /// <summary>
    /// The member's guild avatar hash.
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public string? Avatar { get; set; }

    /// <summary>
    /// When the user joined the guild.
    /// </summary>
    [JsonProperty("joined_at", Required = Required.AllowNull)]
    public string? JoinedAt { get; set; }

    /// <summary>
    /// When the user starting boosting the guild.
    /// </summary>
    [JsonProperty("premium_since")]
    public string? PremiumSince { get; set; }

    /// <summary>
    /// Whether the user is deafened in voice channels.
    /// </summary>
    [JsonProperty("deaf", Required = Required.DisallowNull)]
    public bool Deaf { get; set; }

    /// <summary>
    /// Whether the user is muted in voice channels.
    /// </summary>
    [JsonProperty("mute", Required = Required.DisallowNull)]
    public bool Mute { get; set; }

    /// <summary>
    /// Whether the user has not yet passed the guild's membership screening requirements.
    /// </summary>
    [JsonProperty("pending", Required = Required.DisallowNull)]
    public bool Pending { get; set; }

    /// <summary>
    /// When the user's timeout will expire and the user will be able to communicate in the guild again, null or a time in the past if the user is not timed out.
    /// </summary>
    [JsonProperty("communication_disabled_until")]
    public string? CommunicationDisabledUntil { get; set; }
}

public class GuildMembersChunkEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Set of guild members.
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public GuildMember[] Members { get; set; }

    /// <summary>
    /// The chunk index in the expected chunks for this response (0 <= chunk_index < chunk_count).
    /// </summary>
    [JsonProperty("chunk_index", Required = Required.Always)]
    public int ChunkIndex { get; set; }

    /// <summary>
    /// The total number of expected chunks for this response.
    /// </summary>
    [JsonProperty("chunk_count", Required = Required.Always)]
    public int ChunkCount { get; set; }

    /// <summary>
    /// If passing an invalid id to request_guild_members, it will be returned here.
    /// </summary>
    [JsonProperty("not_found", Required = Required.DisallowNull)]
    public object NotFound { get; set; }

    /// <summary>
    /// If passing true to request_guild_members, presences of the returned members will be here.
    /// </summary>
    [JsonProperty("presences", Required = Required.DisallowNull)]
    public Presence[] Presences { get; set; }

    /// <summary>
    /// The nonce used in the guild members request.
    /// </summary>
    [JsonProperty("nonce", Required = Required.DisallowNull)]
    public string Nonce { get; set; } = null!;
}

public class GuildRoleCreateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The role created.
    /// </summary>
    [JsonProperty("role", Required = Required.Always)]
    public Role Role { get; set; }
}

public class GuildRoleUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The role updated.
    /// </summary>
    [JsonProperty("role", Required = Required.Always)]
    public Role Role { get; set; }
}

public class GuildRoleDeleteEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Id of the role.
    /// </summary>
    [JsonProperty("role_id", Required = Required.Always)]
    public ulong RoleId { get; set; }
}

public class GuildScheduledEventUserAddEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild scheduled event.
    /// </summary>
    [JsonProperty("guild_scheduled_event_id", Required = Required.Always)]
    public ulong GuildScheduledEventId { get; set; }

    /// <summary>
    /// Id of the user.
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; set; }

    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class GuildScheduledEventUserRemoveEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild scheduled event.
    /// </summary>
    [JsonProperty("guild_scheduled_event_id", Required = Required.Always)]
    public ulong GuildScheduledEventId { get; set; }

    /// <summary>
    /// Id of the user.
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; set; }

    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class IntegrationCreateEventAdditionalFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class IntegrationUpdateEventAdditionalFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }
}

public class IntegrationDeleteEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Integration id.
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Id of the bot/oauth2 application for this discord integration.
    /// </summary>
    [JsonProperty("application_id", Required = Required.DisallowNull)]
    public ulong ApplicationId { get; set; }
}

public class InviteCreateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The channel the invite is for.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The unique invite code.
    /// </summary>
    [JsonProperty("code", Required = Required.Always)]
    public string Code { get; set; } = null!;

    /// <summary>
    /// The time at which the invite was created.
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public string CreatedAt { get; set; } = null!;

    /// <summary>
    /// The guild of the invite.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The user that created the invite.
    /// </summary>
    [JsonProperty("inviter", Required = Required.DisallowNull)]
    public User Inviter { get; set; }

    /// <summary>
    /// How long the invite is valid for (in seconds).
    /// </summary>
    [JsonProperty("max_age", Required = Required.Always)]
    public int MaxAge { get; set; }

    /// <summary>
    /// The maximum number of times the invite can be used.
    /// </summary>
    [JsonProperty("max_uses", Required = Required.Always)]
    public int MaxUses { get; set; }

    /// <summary>
    /// The type of target for this voice channel invite.
    /// </summary>
    [JsonProperty("target_type", Required = Required.DisallowNull)]
    public int TargetType { get; set; }

    /// <summary>
    /// The user whose stream to display for this voice channel stream invite.
    /// </summary>
    [JsonProperty("target_user", Required = Required.DisallowNull)]
    public User TargetUser { get; set; }

    /// <summary>
    /// The embedded application to open for this voice channel embedded application invite.
    /// </summary>
    [JsonProperty("target_application", Required = Required.DisallowNull)]
    public PartialApplication TargetApplication { get; set; }

    /// <summary>
    /// Whether or not the invite is temporary (invited users will be kicked on disconnect unless they're assigned a role).
    /// </summary>
    [JsonProperty("temporary", Required = Required.Always)]
    public bool Temporary { get; set; }

    /// <summary>
    /// How many times the invite has been used (always will be 0).
    /// </summary>
    [JsonProperty("uses", Required = Required.Always)]
    public int Uses { get; set; }
}

public class InviteDeleteEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The channel of the invite.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The guild of the invite.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The unique invite code.
    /// </summary>
    [JsonProperty("code", Required = Required.Always)]
    public string Code { get; set; } = null!;
}

public class MessageDeleteEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the message.
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }
}

public class MessageDeleteBulkEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The ids of the messages.
    /// </summary>
    [JsonProperty("ids", Required = Required.Always)]
    public Snowflake[] Ids { get; set; }

    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }
}

public class MessageReactionAddEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; set; }

    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the message.
    /// </summary>
    [JsonProperty("message_id", Required = Required.Always)]
    public ulong MessageId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The member who reacted if this happened in a guild.
    /// </summary>
    [JsonProperty("member", Required = Required.DisallowNull)]
    public Member Member { get; set; }

    /// <summary>
    /// The emoji used to react - example.
    /// </summary>
    [JsonProperty("emoji", Required = Required.Always)]
    public PartialEmoji Emoji { get; set; }
}

public class MessageReactionRemoveEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; set; }

    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the message.
    /// </summary>
    [JsonProperty("message_id", Required = Required.Always)]
    public ulong MessageId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The emoji used to react - example.
    /// </summary>
    [JsonProperty("emoji", Required = Required.Always)]
    public PartialEmoji Emoji { get; set; }
}

public class MessageReactionRemoveAllEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the message.
    /// </summary>
    [JsonProperty("message_id", Required = Required.Always)]
    public ulong MessageId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }
}

public class MessageReactionRemoveEmojiEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// The id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The id of the message.
    /// </summary>
    [JsonProperty("message_id", Required = Required.Always)]
    public ulong MessageId { get; set; }

    /// <summary>
    /// The emoji that was removed.
    /// </summary>
    [JsonProperty("emoji", Required = Required.Always)]
    public PartialEmoji Emoji { get; set; }
}

public class PresenceUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// The user presence is being updated for.
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }

    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Either "idle", "dnd", "online", or "offline".
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// User's current activities.
    /// </summary>
    [JsonProperty("activities", Required = Required.Always)]
    public Activity[] Activities { get; set; }

    /// <summary>
    /// User's platform-dependent status.
    /// </summary>
    [JsonProperty("client_status", Required = Required.Always)]
    public ClientStatus ClientStatus { get; set; }
}

public class ActiveSessionsAreIndicatedWithAn"online","idle",Or"dnd"StringPerPlatform.IfAUserIsOfflineOrInvisible,TheCorrespondingFieldIsNotPresent.  # Remove "Structure" suffix
{
    /// <summary>
    /// The user's status set for an active desktop (windows, linux, mac) application session.
    /// </summary>
    [JsonProperty("desktop", Required = Required.DisallowNull)]
    public string Desktop { get; set; } = null!;

    /// <summary>
    /// The user's status set for an active mobile (ios, android) application session.
    /// </summary>
    [JsonProperty("mobile", Required = Required.DisallowNull)]
    public string Mobile { get; set; } = null!;

    /// <summary>
    /// The user's status set for an active web (browser, bot account) application session.
    /// </summary>
    [JsonProperty("web", Required = Required.DisallowNull)]
    public string Web { get; set; } = null!;
}

public class Activity  # Remove "Structure" suffix
{
    /// <summary>
    /// The activity's name.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Activity type.
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; set; }

    /// <summary>
    /// Stream url, is validated when type is 1.
    /// </summary>
    [JsonProperty("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Unix timestamp (in milliseconds) of when the activity was added to the user's session.
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public int CreatedAt { get; set; }

    /// <summary>
    /// Unix timestamps for start and/or end of the game.
    /// </summary>
    [JsonProperty("timestamps", Required = Required.DisallowNull)]
    public Timestamps Timestamps { get; set; }

    /// <summary>
    /// Application id for the game.
    /// </summary>
    [JsonProperty("application_id", Required = Required.DisallowNull)]
    public ulong ApplicationId { get; set; }

    /// <summary>
    /// What the player is currently doing.
    /// </summary>
    [JsonProperty("details")]
    public string? Details { get; set; }

    /// <summary>
    /// The user's current party status.
    /// </summary>
    [JsonProperty("state")]
    public string? State { get; set; }

    /// <summary>
    /// The emoji used for a custom status.
    /// </summary>
    [JsonProperty("emoji")]
    public Emoji? Emoji { get; set; }

    /// <summary>
    /// Information for the current party of the player.
    /// </summary>
    [JsonProperty("party", Required = Required.DisallowNull)]
    public Party Party { get; set; }

    /// <summary>
    /// Images for the presence and their hover texts.
    /// </summary>
    [JsonProperty("assets", Required = Required.DisallowNull)]
    public Assets Assets { get; set; }

    /// <summary>
    /// Secrets for rich presence joining and spectating.
    /// </summary>
    [JsonProperty("secrets", Required = Required.DisallowNull)]
    public Secrets Secrets { get; set; }

    /// <summary>
    /// Whether or not the activity is an instanced game session.
    /// </summary>
    [JsonProperty("instance", Required = Required.DisallowNull)]
    public bool Instance { get; set; }

    /// <summary>
    /// Activity flags ord together, describes what the payload includes.
    /// </summary>
    [JsonProperty("flags", Required = Required.DisallowNull)]
    public int Flags { get; set; }

    /// <summary>
    /// The custom buttons shown in the rich presence (max 2).
    /// </summary>
    [JsonProperty("buttons", Required = Required.DisallowNull)]
    public Button[] Buttons { get; set; }
}

public class ActivityTimestamps  # Remove "Structure" suffix
{
    /// <summary>
    /// Unix time (in milliseconds) of when the activity started.
    /// </summary>
    [JsonProperty("start", Required = Required.DisallowNull)]
    public int Start { get; set; }

    /// <summary>
    /// Unix time (in milliseconds) of when the activity ends.
    /// </summary>
    [JsonProperty("end", Required = Required.DisallowNull)]
    public int End { get; set; }
}

public class ActivityEmoji  # Remove "Structure" suffix
{
    /// <summary>
    /// The name of the emoji.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// The id of the emoji.
    /// </summary>
    [JsonProperty("id", Required = Required.DisallowNull)]
    public ulong Id { get; set; }

    /// <summary>
    /// Whether this emoji is animated.
    /// </summary>
    [JsonProperty("animated", Required = Required.DisallowNull)]
    public bool Animated { get; set; }
}

public class ActivityParty  # Remove "Structure" suffix
{
    /// <summary>
    /// The id of the party.
    /// </summary>
    [JsonProperty("id", Required = Required.DisallowNull)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Used to show the party's current and maximum size.
    /// </summary>
    [JsonProperty("size", Required = Required.DisallowNull)]
    public object Size { get; set; }
}

public class ActivityAssets  # Remove "Structure" suffix
{
    /// <summary>
    /// See activity asset image.
    /// </summary>
    [JsonProperty("large_image", Required = Required.DisallowNull)]
    public string LargeImage { get; set; } = null!;

    /// <summary>
    /// Text displayed when hovering over the large image of the activity.
    /// </summary>
    [JsonProperty("large_text", Required = Required.DisallowNull)]
    public string LargeText { get; set; } = null!;

    /// <summary>
    /// See activity asset image.
    /// </summary>
    [JsonProperty("small_image", Required = Required.DisallowNull)]
    public string SmallImage { get; set; } = null!;

    /// <summary>
    /// Text displayed when hovering over the small image of the activity.
    /// </summary>
    [JsonProperty("small_text", Required = Required.DisallowNull)]
    public string SmallText { get; set; } = null!;
}

public class ActivitySecrets  # Remove "Structure" suffix
{
    /// <summary>
    /// The secret for joining a party.
    /// </summary>
    [JsonProperty("join", Required = Required.DisallowNull)]
    public string Join { get; set; } = null!;

    /// <summary>
    /// The secret for spectating a game.
    /// </summary>
    [JsonProperty("spectate", Required = Required.DisallowNull)]
    public string Spectate { get; set; } = null!;

    /// <summary>
    /// The secret for a specific instanced match.
    /// </summary>
    [JsonProperty("match", Required = Required.DisallowNull)]
    public string Match { get; set; } = null!;
}

public class WhenReceivedOverTheGateway,The  # Remove "Structure" suffix
{
    /// <summary>
    /// The text shown on the button (1-32 characters).
    /// </summary>
    [JsonProperty("label", Required = Required.Always)]
    public string Label { get; set; } = null!;

    /// <summary>
    /// The url opened when clicking the button (1-512 characters).
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; set; } = null!;
}

public class TypingStartEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }

    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.DisallowNull)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Id of the user.
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; set; }

    /// <summary>
    /// Unix time (in seconds) of when the user started typing.
    /// </summary>
    [JsonProperty("timestamp", Required = Required.Always)]
    public int Timestamp { get; set; }

    /// <summary>
    /// The member who started typing if this happened in a guild.
    /// </summary>
    [JsonProperty("member", Required = Required.DisallowNull)]
    public Member Member { get; set; }
}

public class VoiceServerUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Voice connection token.
    /// </summary>
    [JsonProperty("token", Required = Required.Always)]
    public string Token { get; set; } = null!;

    /// <summary>
    /// The guild this voice server update is for.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// The voice server host.
    /// </summary>
    [JsonProperty("endpoint", Required = Required.AllowNull)]
    public string? Endpoint { get; set; }
}

public class WebhooksUpdateEventFields  # Remove "Structure" suffix
{
    /// <summary>
    /// Id of the guild.
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Id of the channel.
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; set; }
}

public class JsonResponse  # Remove "Structure" suffix
{
    /// <summary>
    /// The wss url that can be used for connecting to the gateway.
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// The recommended number of shards to use when connecting.
    /// </summary>
    [JsonProperty("shards", Required = Required.Always)]
    public int Shards { get; set; }

    /// <summary>
    /// Information on the current session start limit.
    /// </summary>
    [JsonProperty("session_start_limit", Required = Required.Always)]
    public SessionStartLimit SessionStartLimit { get; set; }
}

public class SessionStartLimit  # Remove "Structure" suffix
{
    /// <summary>
    /// The total number of session starts the current user is allowed.
    /// </summary>
    [JsonProperty("total", Required = Required.Always)]
    public int Total { get; set; }

    /// <summary>
    /// The remaining number of session starts the current user is allowed.
    /// </summary>
    [JsonProperty("remaining", Required = Required.Always)]
    public int Remaining { get; set; }

    /// <summary>
    /// The number of milliseconds after which the limit resets.
    /// </summary>
    [JsonProperty("reset_after", Required = Required.Always)]
    public int ResetAfter { get; set; }

    /// <summary>
    /// The number of identify requests allowed per 5 seconds.
    /// </summary>
    [JsonProperty("max_concurrency", Required = Required.Always)]
    public int MaxConcurrency { get; set; }
}

