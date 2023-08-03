using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-video-structure">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-video-structure">
/// GitHub</a>.
/// </summary>
public record EmbedVideo {
	/// <summary>
	/// Source URL of video.
	/// </summary>
	[JsonPropertyName("url")]
	public Uri? Url { get; init; }

	/// <summary>
	/// A proxied URL of the video.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public Uri? ProxyUrl { get; init; }

	/// <summary>
	/// Height of video.
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; init; }

	/// <summary>
	/// Width of video.
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; init; }
}
