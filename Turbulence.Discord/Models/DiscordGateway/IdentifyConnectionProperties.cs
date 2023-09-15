using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-connection-properties">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#identify-connection-properties">
/// GitHub</a>.
/// </summary>
public record IdentifyConnectionProperties {
	/// <summary>
	/// Your operating system.
	/// </summary>
	[JsonPropertyName("os")]
	public required string Os { get; init; } // TODO: Enum maybe

	// TODO: Improve description
	/// <summary>
	/// Your library name.
	/// </summary>
	[JsonPropertyName("browser")]
	public required string Browser { get; init; }

	// TODO: Improve description
	/// <summary>
	/// Your library name.
	/// </summary>
	[JsonPropertyName("device")]
	public required string Device { get; init; }

	#region Undocumented, see https://luna.gitlab.io/discord-unofficial-docs/connection_properties.html
	
	/// <summary>
	/// System locale, for example "en-US". Undocumented.
	/// </summary>
	[JsonPropertyName("system_locale")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? SystemLocale { get ; set ; }
	
	/// <summary>
	/// The user agent for the browser or device. Undocumented.
	/// </summary>
	[JsonPropertyName("browser_user_agent")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? BrowserUserAgent { get ; set ; }
	
	/// <summary>
	/// The client or browser version. Undocumented.
	/// </summary>
	[JsonPropertyName("browser_version")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? BrowserVersion { get ; set ; }
	
	/// <summary>
	/// Your device OS version, for example "4.20-1-arch1-1-ARCH". Undocumented.
	/// </summary>
	[JsonPropertyName("os_version")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? OsVersion { get ; set ; }
	
	/// <summary>
	/// Referring page. Undocumented.
	/// </summary>
	[JsonPropertyName("referrer")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Referrer { get ; set ; }
	
	/// <summary>
	/// The domain of the referrer. Undocumented.
	/// </summary>
	[JsonPropertyName("referring_domain")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? ReferringDomain { get ; set ; }
	
	/// <summary>
	/// Unknown, undocumented.
	/// </summary>
	[JsonPropertyName("referrer_current")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? ReferrerCurrent { get ; set ; }
	
	/// <summary>
	/// Unknown, undocumented.
	/// </summary>
	[JsonPropertyName("referring_domain_current")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? ReferringDomainCurrent { get ; set ; }

	/// <summary>
	/// The client's release channel, for example "stable", "ptb", "canary". Undocumented.
	/// </summary>
	[JsonPropertyName("release_channel")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? ReleaseChannel { get ; set ; }
	
	/// <summary>
	/// The client's build number. Undocumented.
	/// </summary>
	[JsonPropertyName("client_build_number")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? ClientBuildNumber { get ; set ; }
	
	/// <summary>
	/// Unknown, undocumented.
	/// </summary>
	[JsonPropertyName("client_event_source")]
	public object? ClientEventSource { get ; set ; }
	
	#endregion
}
