using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway;

public class IdentifyConnectionProperties
{
    /// <summary>
    /// Your operating system.
    /// </summary>
    [JsonProperty("os", Required = Required.Always)]
    public string OS { get; set; }

    /// <summary>
    /// Your browser name.
    /// </summary>
    [JsonProperty("browser", Required = Required.Always)]
    public string Browser { get; set; }

    /// <summary>
    /// Your device name.
    /// </summary>
    [JsonProperty("device", Required = Required.Always)]
    public string Device { get; set; }

    [JsonProperty("system_locale", Required = Required.Always)]
    public string Locale { get; set; }

    [JsonProperty("browser_user_agent", Required = Required.Always)]
    public string UserAgent { get; set; }

    [JsonProperty("browser_version", Required = Required.Always)]
    public string BrowserVersion { get; set; }

    [JsonProperty("os_version", Required = Required.Always)]
    public string OSVersion { get; set; }

    [JsonProperty("referrer", Required = Required.Always)]
    public string Referrer { get; set; }

    [JsonProperty("referring_domain", Required = Required.Always)]
    public string ReferringDomain { get; set; }

    [JsonProperty("referrer_current", Required = Required.Always)]
    public string ReferrerCurrent { get; set; }

    [JsonProperty("referring_domain_current", Required = Required.Always)]
    public string ReferringDomainCurrent { get; set; }

    [JsonProperty("release_channel", Required = Required.Always)]
    public string ReleaseChannel { get; set; }

    [JsonProperty("client_build_number", Required = Required.Always)]
    public int ClientBuildNumber { get; set; }

    [JsonProperty("client_event_source", NullValueHandling = NullValueHandling.Include)]
    public string? ClientEventSource { get; set; }
}