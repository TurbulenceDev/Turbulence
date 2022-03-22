public class InviteStructure
{
    /// <summary>
    /// The invite code (unique id)
    /// </summary>
    [JsonProperty("code", Required = Required.Always)]
    public string Code { get; internal set; } = null!;

    /// <summary>
    /// The guild this invite is for
    /// </summary>
    [JsonProperty("guild?", Required = Required.Always)]
    public partial guild object Guild? { get; internal set; }

    /// <summary>
    /// The channel this invite is for
    /// </summary>
    [JsonProperty("channel", Required = Required.AllowNull)]
    public partial channel object? Channel { get; internal set; }

    /// <summary>
    /// The user who created the invite
    /// </summary>
    [JsonProperty("inviter?", Required = Required.Always)]
    public user object Inviter? { get; internal set; }

    /// <summary>
    /// The type of target for this voice channel invite
    /// </summary>
    [JsonProperty("target_type?", Required = Required.Always)]
    public int TargetType? { get; internal set; }

    /// <summary>
    /// The user whose stream to display for this voice channel stream invite
    /// </summary>
    [JsonProperty("target_user?", Required = Required.Always)]
    public user object TargetUser? { get; internal set; }

    /// <summary>
    /// The embedded application to open for this voice channel embedded application invite
    /// </summary>
    [JsonProperty("target_application?", Required = Required.Always)]
    public partial application object TargetApplication? { get; internal set; }

    /// <summary>
    /// Approximate count of online members, returned from the get /invites/<code> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_presence_count?", Required = Required.Always)]
    public int ApproximatePresenceCount? { get; internal set; }

    /// <summary>
    /// Approximate count of total members, returned from the get /invites/<code> endpoint when with_counts is true
    /// </summary>
    [JsonProperty("approximate_member_count?", Required = Required.Always)]
    public int ApproximateMemberCount? { get; internal set; }

    /// <summary>
    /// The expiration date of this invite, returned from the get /invites/<code> endpoint when with_expiration is true
    /// </summary>
    [JsonProperty("expires_at?", Required = Required.AllowNull)]
    public ISO8601 timestamp? ExpiresAt? { get; internal set; }

    /// <summary>
    /// Stage instance data if there is a public stage instance in the stage channel this invite is for
    /// </summary>
    [JsonProperty("stage_instance?", Required = Required.Always)]
    public invite stage instance object StageInstance? { get; internal set; }

    /// <summary>
    /// Guild scheduled event data, only included if guild_scheduled_event_id contains a valid guild scheduled event id
    /// </summary>
    [JsonProperty("guild_scheduled_event?", Required = Required.Always)]
    public guild scheduled event object GuildScheduledEvent? { get; internal set; }

}

enum InviteTargetTypes
{
    STREAM = 1, // 
    EMBEDDED_APPLICATION = 2, // 
}

public class InviteMetadataStructure
{
    /// <summary>
    /// Number of times this invite has been used
    /// </summary>
    [JsonProperty("uses", Required = Required.Always)]
    public int Uses { get; internal set; }

    /// <summary>
    /// Max number of times this invite can be used
    /// </summary>
    [JsonProperty("max_uses", Required = Required.Always)]
    public int MaxUses { get; internal set; }

    /// <summary>
    /// Duration (in seconds) after which the invite expires
    /// </summary>
    [JsonProperty("max_age", Required = Required.Always)]
    public int MaxAge { get; internal set; }

    /// <summary>
    /// Whether this invite only grants temporary membership
    /// </summary>
    [JsonProperty("temporary", Required = Required.Always)]
    public bool Temporary { get; internal set; }

    /// <summary>
    /// When this invite was created
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public ISO8601 timestamp CreatedAt { get; internal set; }

}

public class InviteStageInstanceStructure
{
    /// <summary>
    /// The members speaking in the stage
    /// </summary>
    [JsonProperty("members", Required = Required.Always)]
    public partial guild member objects[] Members { get; internal set; }

    /// <summary>
    /// The number of users in the stage
    /// </summary>
    [JsonProperty("participant_count", Required = Required.Always)]
    public int ParticipantCount { get; internal set; }

    /// <summary>
    /// The number of users speaking in the stage
    /// </summary>
    [JsonProperty("speaker_count", Required = Required.Always)]
    public int SpeakerCount { get; internal set; }

    /// <summary>
    /// The topic of the stage instance (1-120 characters)
    /// </summary>
    [JsonProperty("topic", Required = Required.Always)]
    public string Topic { get; internal set; } = null!;

}

public class QueryStringParams
{
    /// <summary>
    /// Whether the invite should contain approximate member counts
    /// </summary>
    [JsonProperty("with_counts?", Required = Required.Always)]
    public bool WithCounts? { get; internal set; }

    /// <summary>
    /// Whether the invite should contain the expiration date
    /// </summary>
    [JsonProperty("with_expiration?", Required = Required.Always)]
    public bool WithExpiration? { get; internal set; }

    /// <summary>
    /// The guild scheduled event to include with the invite
    /// </summary>
    [JsonProperty("guild_scheduled_event_id?", Required = Required.Always)]
    public ulong GuildScheduledEventId? { get; internal set; }

}

