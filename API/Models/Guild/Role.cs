using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class Role
{
    /// <summary>
    /// Role id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Role name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Integer representation of hexadecimal color code
    /// </summary>
    [JsonProperty("color", Required = Required.Always)]
    public int Color { get; internal set; }

    /// <summary>
    /// If this role is pinned in the user listing
    /// </summary>
    [JsonProperty("hoist", Required = Required.Always)]
    public bool Hoist { get; internal set; }

    /// <summary>
    /// Role icon hash
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; internal set; }

    /// <summary>
    /// Role unicode emoji
    /// </summary>
    [JsonProperty("unicode_emoji")]
    public string? UnicodeEmoji { get; internal set; }

    /// <summary>
    /// Position of this role
    /// </summary>
    [JsonProperty("position", Required = Required.Always)]
    public int Position { get; internal set; }

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("permissions", Required = Required.Always)]
    public string Permissions { get; internal set; } = null!;

    /// <summary>
    /// Whether this role is managed by an integration
    /// </summary>
    [JsonProperty("managed", Required = Required.Always)]
    public bool Managed { get; internal set; }

    /// <summary>
    /// Whether this role is mentionable
    /// </summary>
    [JsonProperty("mentionable", Required = Required.Always)]
    public bool Mentionable { get; internal set; }

    /// <summary>
    /// The tags this role has
    /// </summary>
    [JsonProperty("tags", Required = Required.DisallowNull)]
    public RoleTag Tags { get; internal set; }


}