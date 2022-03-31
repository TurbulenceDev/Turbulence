using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#identify-identify-structure
/// </summary>
public class Identify
{
    /// <summary>
    /// Authentication token
    /// </summary>
    [JsonProperty("token", Required = Required.Always)]
    public string Token { get; set; }

    /// <summary>
    /// Capabilities
    /// </summary>
    [JsonProperty("capabilities", Required = Required.Always)]
    public int Capabilities { get; set; }

    /// <summary>
    /// Connection properties
    /// </summary>
    [JsonProperty("properties", Required = Required.Always)]
    public IdentifyConnectionProperties Properties { get; set; }

    /// <summary>
    /// Presence structure for initial presence information
    /// </summary>
    [JsonProperty("presence", Required = Required.Always)]
    public GatewayPresenceUpdate Presence { get; set; }

    /// <summary>
    /// Whether this connection supports compression of packets
    /// </summary>
    [JsonProperty("compress", Required = Required.Always)]
    public bool Compress { get; set; } = false;

    [JsonProperty("client_state", Required = Required.Always)]
    public GatewayClientState ClientState { get; set; }
}