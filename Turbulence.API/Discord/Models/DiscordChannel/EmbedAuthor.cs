using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-author-structure">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-author-structure">
/// GitHub</a>.
/// </summary>
public record EmbedAuthor {
	/// <summary>
	/// Name of author.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// URL of author (only supports http(s)).
	/// </summary>
	[JsonPropertyName("url")]
	public Uri? Url { get; init; }

	/// <summary>
	/// URL of author icon (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("icon_url")]
	public Uri? IconUrl { get; init; }

	/// <summary>
	/// A proxied URL of author icon.
	/// </summary>
	[JsonPropertyName("proxy_icon_url")]
	public Uri? ProxyIconUrl { get; init; }
}
