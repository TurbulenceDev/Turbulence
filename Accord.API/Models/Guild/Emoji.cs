using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class Emoji
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
    public string? Name { get; internal set; }

    /// <summary>
    /// Roles allowed to use this emoji
    /// </summary>
    [JsonProperty("roles", Required = Required.DisallowNull)]
    public ulong[] Roles { get; internal set; } = null!;

    /// <summary>
    /// User that created this emoji
    /// </summary>
    [JsonProperty("user", Required = Required.DisallowNull)]
    public User.User User { get; internal set; } = null!;

    /// <summary>
    /// Whether this emoji must be wrapped in colons
    /// </summary>
    [JsonProperty("require_colons", Required = Required.DisallowNull)]
    public bool RequireColons { get; internal set; }

    /// <summary>
    /// Whether this emoji is managed
    /// </summary>
    [JsonProperty("managed", Required = Required.DisallowNull)]
    public bool Managed { get; internal set; }

    /// <summary>
    /// Whether this emoji is animated
    /// </summary>
    [JsonProperty("animated", Required = Required.DisallowNull)]
    public bool Animated { get; internal set; }

    /// <summary>
    /// Whether this emoji can be used, may be false due to loss of server boosts
    /// </summary>
    [JsonProperty("available", Required = Required.DisallowNull)]
    public bool Available { get; internal set; }


}