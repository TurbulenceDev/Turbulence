public class StageInstanceStructure
{
    /// <summary>
    /// The id of this stage instance
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The guild id of the associated stage channel
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; internal set; }

    /// <summary>
    /// The id of the associated stage channel
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// The topic of the stage instance (1-120 characters)
    /// </summary>
    [JsonProperty("topic", Required = Required.Always)]
    public string Topic { get; internal set; } = null!;

    /// <summary>
    /// The privacy level of the stage instance
    /// </summary>
    [JsonProperty("privacy_level", Required = Required.Always)]
    public int PrivacyLevel { get; internal set; }

    /// <summary>
    /// Whether or not stage discovery is disabled (deprecated)
    /// </summary>
    [JsonProperty("discoverable_disabled", Required = Required.Always)]
    public bool DiscoverableDisabled { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The id of the stage channel
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// The topic of the stage instance (1-120 characters)
    /// </summary>
    [JsonProperty("topic", Required = Required.Always)]
    public string Topic { get; internal set; } = null!;

    /// <summary>
    /// The privacy level of the stage instance (default guild_only)
    /// </summary>
    [JsonProperty("privacy_level?", Required = Required.Always)]
    public int PrivacyLevel? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The topic of the stage instance (1-120 characters)
    /// </summary>
    [JsonProperty("topic?", Required = Required.Always)]
    public string Topic? { get; internal set; } = null!;

    /// <summary>
    /// The privacy level of the stage instance
    /// </summary>
    [JsonProperty("privacy_level?", Required = Required.Always)]
    public int PrivacyLevel? { get; internal set; }

}

