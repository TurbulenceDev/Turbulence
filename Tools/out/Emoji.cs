public class EmojiStructure
{
    /// <summary>
    /// Emoji id
    /// </summary>
    [JsonProperty("id", Required = Required.AllowNull)]
    public ulong? Id { get; internal set; }

    /// <summary>
    /// Emoji name
    /// </summary>
    [JsonProperty("name", Required = Required.AllowNull)]
    public string (can be null only in reaction emoji objects)? Name { get; internal set; }

    /// <summary>
    /// Roles allowed to use this emoji
    /// </summary>
    [JsonProperty("roles?", Required = Required.Always)]
    public role object ids[] Roles? { get; internal set; }

    /// <summary>
    /// User that created this emoji
    /// </summary>
    [JsonProperty("user?", Required = Required.Always)]
    public user object User? { get; internal set; }

    /// <summary>
    /// Whether this emoji must be wrapped in colons
    /// </summary>
    [JsonProperty("require_colons?", Required = Required.Always)]
    public bool RequireColons? { get; internal set; }

    /// <summary>
    /// Whether this emoji is managed
    /// </summary>
    [JsonProperty("managed?", Required = Required.Always)]
    public bool Managed? { get; internal set; }

    /// <summary>
    /// Whether this emoji is animated
    /// </summary>
    [JsonProperty("animated?", Required = Required.Always)]
    public bool Animated? { get; internal set; }

    /// <summary>
    /// Whether this emoji can be used, may be false due to loss of server boosts
    /// </summary>
    [JsonProperty("available?", Required = Required.Always)]
    public bool Available? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the emoji
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The 128x128 emoji image
    /// </summary>
    [JsonProperty("image", Required = Required.Always)]
    public image data Image { get; internal set; }

    /// <summary>
    /// Roles allowed to use this emoji
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public snowflakes[] Roles { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Name of the emoji
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Roles allowed to use this emoji
    /// </summary>
    [JsonProperty("roles", Required = Required.AllowNull)]
    public snowflakes[]? Roles { get; internal set; }

}

