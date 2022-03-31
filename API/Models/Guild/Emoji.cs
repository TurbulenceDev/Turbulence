using Newtonsoft.Json;
using Turbulence.API.Models.DiscordUser;

namespace Turbulence.API.Models.Guild;

public class Emoji
{
    /// <summary>
    /// Emoji id
    /// </summary>
    [JsonProperty("id", Required = Required.AllowNull)]
    public ulong? Id { get; set; }

    /// <summary>
    /// Emoji name
    /// </summary>
    [JsonProperty("name", Required = Required.AllowNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Roles allowed to use this emoji
    /// </summary>
    [JsonProperty("roles", Required = Required.DisallowNull)]
    public ulong[] Roles { get; set; } = null!;

    /// <summary>
    /// User that created this emoji
    /// </summary>
    [JsonProperty("user", Required = Required.DisallowNull)]
    public User User { get; set; } = null!;

    /// <summary>
    /// Whether this emoji must be wrapped in colons
    /// </summary>
    [JsonProperty("require_colons", Required = Required.DisallowNull)]
    public bool RequireColons { get; set; }

    /// <summary>
    /// Whether this emoji is managed
    /// </summary>
    [JsonProperty("managed", Required = Required.DisallowNull)]
    public bool Managed { get; set; }

    /// <summary>
    /// Whether this emoji is animated
    /// </summary>
    [JsonProperty("animated", Required = Required.DisallowNull)]
    public bool Animated { get; set; }

    /// <summary>
    /// Whether this emoji can be used, may be false due to loss of server boosts
    /// </summary>
    [JsonProperty("available", Required = Required.DisallowNull)]
    public bool Available { get; set; }


}