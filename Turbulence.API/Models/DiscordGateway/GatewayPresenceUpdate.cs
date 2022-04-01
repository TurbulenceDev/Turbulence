using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#update-presence-gateway-presence-update-structure
/// </summary>
public class GatewayPresenceUpdate
{
    /// <summary>
    /// The user's new status.
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Unix time (in milliseconds) of when the client went idle, or null if the client is not idle.
    /// </summary>
    [JsonProperty("since")]
    public int? Since { get; set; }

    /// <summary>
    /// The user's activities.
    /// </summary>
    [JsonProperty("activities", Required = Required.Always)]
    public Activity[] Activities { get; set; }

    /// <summary>
    /// Whether or not the client is afk.
    /// </summary>
    [JsonProperty("afk", Required = Required.Always)]
    public bool Afk { get; set; }
}