using Newtonsoft.Json;

namespace Turbulence.API.Models.Guild;

public class Overwrite
{
    /// <summary>
    /// Role or user id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    /// <summary>
    /// Either 0 (role) or 1 (member)
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; set; }

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("allow", Required = Required.Always)]
    public string Allow { get; set; } = null!;

    /// <summary>
    /// Permission bit set
    /// </summary>
    [JsonProperty("deny", Required = Required.Always)]
    public string Deny { get; set; } = null!;
}