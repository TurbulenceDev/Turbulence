using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class PresenceUpdateEvent
{
    /// <summary>
    /// The user presence is being updated for
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User.User User { get; internal set; }

    /// <summary>
    /// Id of the guild
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; internal set; }

    /// <summary>
    /// Either "idle", "dnd", "online", or "offline"
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; internal set; } = null!;

    /// <summary>
    /// User's current activities
    /// </summary>
    [JsonProperty("activities", Required = Required.Always)]
    public array of activity objects Activities { get; internal set; }

/// <summary>
/// User's platform-dependent status
/// </summary>
[JsonProperty("client_status", Required = Required.Always)]
public client_status object ClientStatus { get; internal set; }


}