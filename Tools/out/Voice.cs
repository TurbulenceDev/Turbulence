public class VoiceStateStructure
{
    /// <summary>
    /// The guild id this voice state is for
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// The channel id this user is connected to
    /// </summary>
    [JsonProperty("channel_id", Required = Required.AllowNull)]
    public ulong? ChannelId { get; internal set; }

    /// <summary>
    /// The user id this voice state is for
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; internal set; }

    /// <summary>
    /// The guild member this voice state is for
    /// </summary>
    [JsonProperty("member?", Required = Required.Always)]
    public guild member object Member? { get; internal set; }

    /// <summary>
    /// The session id for this voice state
    /// </summary>
    [JsonProperty("session_id", Required = Required.Always)]
    public string SessionId { get; internal set; } = null!;

    /// <summary>
    /// Whether this user is deafened by the server
    /// </summary>
    [JsonProperty("deaf", Required = Required.Always)]
    public bool Deaf { get; internal set; }

    /// <summary>
    /// Whether this user is muted by the server
    /// </summary>
    [JsonProperty("mute", Required = Required.Always)]
    public bool Mute { get; internal set; }

    /// <summary>
    /// Whether this user is locally deafened
    /// </summary>
    [JsonProperty("self_deaf", Required = Required.Always)]
    public bool SelfDeaf { get; internal set; }

    /// <summary>
    /// Whether this user is locally muted
    /// </summary>
    [JsonProperty("self_mute", Required = Required.Always)]
    public bool SelfMute { get; internal set; }

    /// <summary>
    /// Whether this user is streaming using "go live"
    /// </summary>
    [JsonProperty("self_stream?", Required = Required.Always)]
    public bool SelfStream? { get; internal set; }

    /// <summary>
    /// Whether this user's camera is enabled
    /// </summary>
    [JsonProperty("self_video", Required = Required.Always)]
    public bool SelfVideo { get; internal set; }

    /// <summary>
    /// Whether this user is muted by the current user
    /// </summary>
    [JsonProperty("suppress", Required = Required.Always)]
    public bool Suppress { get; internal set; }

    /// <summary>
    /// The time at which the user requested to speak
    /// </summary>
    [JsonProperty("request_to_speak_timestamp", Required = Required.AllowNull)]
    public ISO8601 timestamp? RequestToSpeakTimestamp { get; internal set; }

}

public class VoiceRegionStructure
{
    /// <summary>
    /// Unique id for the region
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; internal set; } = null!;

    /// <summary>
    /// Name of the region
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// True for a single server that is closest to the current user's client
    /// </summary>
    [JsonProperty("optimal", Required = Required.Always)]
    public bool Optimal { get; internal set; }

    /// <summary>
    /// Whether this is a deprecated voice region (avoid switching to these)
    /// </summary>
    [JsonProperty("deprecated", Required = Required.Always)]
    public bool Deprecated { get; internal set; }

    /// <summary>
    /// Whether this is a custom voice region (used for events/etc)
    /// </summary>
    [JsonProperty("custom", Required = Required.Always)]
    public bool Custom { get; internal set; }

}

