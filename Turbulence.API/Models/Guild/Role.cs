using Newtonsoft.Json;

namespace Turbulence.API.Models.Guild;

public class Role
{
    /// <summary>
    /// Role id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// Role name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Integer representation of hexadecimal color code
    /// </summary>
    [JsonProperty("color", Required = Required.Always)]
    public int Color { get; set; }

    /// <summary>
    /// If this role is pinned in the user listing
    /// </summary>
    [JsonProperty("hoist", Required = Required.Always)]
    public bool Hoist { get; set; }

    /// <summary>
    /// Role icon hash
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Role unicode emoji
    /// </summary>
    [JsonProperty("unicode_emoji")]
    public string? UnicodeEmoji { get; set; }

    /// <summary>
    /// Position of this role
    /// </summary>
    [JsonProperty("position", Required = Required.Always)]
    public int Position { get; set; }

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("permissions", Required = Required.Always)]
    public string Permissions { get; set; } = null!;

    /// <summary>
    /// Whether this role is managed by an integration
    /// </summary>
    [JsonProperty("managed", Required = Required.Always)]
    public bool Managed { get; set; }

    /// <summary>
    /// Whether this role is mentionable
    /// </summary>
    [JsonProperty("mentionable", Required = Required.Always)]
    public bool Mentionable { get; set; }

    /// <summary>
    /// The tags this role has
    /// </summary>
    [JsonProperty("tags", Required = Required.DisallowNull)]
    public RoleTag Tags { get; set; }


}