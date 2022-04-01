using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#get-gateway-example-response
/// </summary>
/// <param name="Url">The URL to the gateway. Should be cached and only retrieved again when cached URL does not work anymore.</param>
public record Gateway
(
    [property: JsonProperty("url", Required = Required.Always)]
    string Url
);