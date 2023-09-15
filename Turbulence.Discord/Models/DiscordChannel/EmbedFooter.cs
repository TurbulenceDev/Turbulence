using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-footer-structure">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-footer-structure">
/// GitHub</a>.
/// </summary>
public record EmbedFooter {
	/// <summary>
	/// Footer text.
	/// </summary>
	[JsonPropertyName("text")]
	public required string Text { get; init; }

	/// <summary>
	/// URL of footer icon (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("icon_url")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Uri? IconUrl { get; init; }

	/// <summary>
	/// A proxied URL of footer icon.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Uri? ProxyIconUrl { get; init; }
}
