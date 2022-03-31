using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#get-gateway-example-response
/// </summary>
public class Gateway
{
    /// <summary>
    /// The URL to the gateway. Should be cached and only retrieved again when cached URL does not work anymore.
    /// </summary>
    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; set; }
}