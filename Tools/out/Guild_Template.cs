public class GuildTemplateStructure
{
    /// <summary>
    /// The template code (unique id)
    /// </summary>
    [JsonProperty("code", Required = Required.Always)]
    public string Code { get; internal set; } = null!;

    /// <summary>
    /// Template name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The description for the template
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Number of times this template has been used
    /// </summary>
    [JsonProperty("usage_count", Required = Required.Always)]
    public int UsageCount { get; internal set; }

    /// <summary>
    /// The id of the user who created the template
    /// </summary>
    [JsonProperty("creator_id", Required = Required.Always)]
    public ulong CreatorId { get; internal set; }

    /// <summary>
    /// The user who created the template
    /// </summary>
    [JsonProperty("creator", Required = Required.Always)]
    public user object Creator { get; internal set; }

    /// <summary>
    /// When this template was created
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public ISO8601 timestamp CreatedAt { get; internal set; }

    /// <summary>
    /// When this template was last synced to the source guild
    /// </summary>
    [JsonProperty("updated_at", Required = Required.Always)]
    public ISO8601 timestamp UpdatedAt { get; internal set; }

    /// <summary>
    /// The id of the guild this template is based on
    /// </summary>
    [JsonProperty("source_guild_id", Required = Required.Always)]
    public ulong SourceGuildId { get; internal set; }

    /// <summary>
    /// The guild snapshot this template contains
    /// </summary>
    [JsonProperty("serialized_source_guild", Required = Required.Always)]
    public partial guild object SerializedSourceGuild { get; internal set; }

    /// <summary>
    /// Whether the template has unsynced changes
    /// </summary>
    [JsonProperty("is_dirty", Required = Required.AllowNull)]
    public bool? IsDirty { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the guild (2-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Base64 128x128 image for the guild icon
    /// </summary>
    [JsonProperty("icon?", Required = Required.Always)]
    public image data Icon? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the template (1-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Description for the template (0-120 characters)
    /// </summary>
    [JsonProperty("description?", Required = Required.AllowNull)]
    public string? Description? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the template (1-100 characters)
    /// </summary>
    [JsonProperty("name?", Required = Required.Always)]
    public string Name? { get; internal set; } = null!;

    /// <summary>
    /// Description for the template (0-120 characters)
    /// </summary>
    [JsonProperty("description?", Required = Required.AllowNull)]
    public string? Description? { get; internal set; }

}

