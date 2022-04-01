using System.Text;
using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#payloads-gateway-payload-structure
/// </summary>
public class GatewayPayload
{
    public byte[] ToBytes()
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
    }

    /// <summary>
    /// Opcode for the payload.
    /// </summary>
    [JsonProperty("op", Required = Required.Always)]
    public int Opcode { get; set; }

    /// <summary>
    /// Event data.
    /// </summary>
    [JsonProperty("d", Required = Required.AllowNull)]
    public dynamic? Data { get; set; }

    /// <summary>
    /// Sequence number, used for resuming sessions and heartbeats.
    /// </summary>
    [JsonProperty("s", Required = Required.AllowNull)]
    public int? Sequence { get; set; }

    /// <summary>
    /// The event name for this payload.
    /// </summary>
    [JsonProperty("t", Required = Required.AllowNull)]
    public string? Name { get; set; }
}
