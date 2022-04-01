using Newtonsoft.Json;
using Turbulence.API.Models.DiscordUser;

namespace Turbulence.API.Models.Guild;

public class PresenceUpdateEvent
{
    /// <summary>
    /// The user presence is being updated for
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public User User { get; set; }

    /// <summary>
    /// Id of the guild
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; set; }

    /// <summary>
    /// Either "idle", "dnd", "online", or "offline"
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// User's current activities
    /// </summary>
    [JsonProperty("activities", Required = Required.Always)]
    public /*array of activity objects*/ object[] Activities { get; set; }

    /// <summary>
    /// User's platform-dependent status
    /// </summary>
    [JsonProperty("client_status", Required = Required.Always)]
    public /*client_status*/ object ClientStatus { get; set; }
}