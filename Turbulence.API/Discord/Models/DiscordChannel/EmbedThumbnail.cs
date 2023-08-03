using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-thumbnail-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-thumbnail-structure">
/// GitHub</a>.
/// </summary>
public record EmbedThumbnail {
	/// <summary>
	/// Source URL of thumbnail (only supports http(s) and attachments).
	/// </summary>
	[JsonPropertyName("url")]
	public required Uri Url { get; init; }

	/// <summary>
	/// A proxied URL of the thumbnail.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public Uri? ProxyUrl { get; init; }

	/// <summary>
	/// Height of thumbnail.
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; init; }

	/// <summary>
	/// Width of thumbnail.
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; init; }
}
