using System.Runtime.Serialization;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#update-presence-gateway-presence-update-structure
/// </summary>
public class GatewayPresenceUpdate
{
    /// <summary>
    /// Unix time (in milliseconds) of when the client went idle, or null if the client is not idle.
    /// </summary>
    [DataMember(Name = "since")]
    public int? Since { get; set; }

    /// <summary>
    /// The user's activities.
    /// </summary>
    [DataMember(Name = "activities", IsRequired = true)]
    public Activity[] Activities { get; set; }

    /// <summary>
    /// The user's new status.
    /// </summary>
    [DataMember(Name = "status", IsRequired = true)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// Whether or not the client is afk.
    /// </summary>
    [DataMember(Name = "afk", IsRequired = true)]
    public bool Afk { get; set; }
}