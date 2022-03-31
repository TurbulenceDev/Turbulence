using Newtonsoft.Json;

namespace Turbulence.API.Models;

/// <summary>
/// Represents an object in Discord API.
/// </summary>
public abstract class Snowflake
{
    /// <summary>
    /// Gets the ID of this object.
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; set; }

    internal Snowflake() { }
}