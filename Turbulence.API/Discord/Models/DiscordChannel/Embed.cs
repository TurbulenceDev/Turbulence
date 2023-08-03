using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-object">GitHub</a>.
/// </summary>
public record Embed {
	/// <summary>
	/// Title of embed.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-types">Type of embed</a>
	/// (always "rich" for webhook embeds).
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>
	/// Description of embed.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// URL of embed.
	/// </summary>
	[JsonPropertyName("url")]
	public Uri? Url { get; init; }

	/// <summary>
	/// Timestamp of embed content.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public string? Timestamp { get; init; }

	/// <summary>
	/// Color code of the embed.
	/// </summary>
	[JsonPropertyName("color")]
	public int? Color { get; init; }

	/// <summary>
	/// Footer information.
	/// </summary>
	[JsonPropertyName("footer")]
	public EmbedFooter? Footer { get; init; }

	/// <summary>
	/// Image information.
	/// </summary>
	[JsonPropertyName("image")]
	public EmbedImage? Image { get; init; }

	/// <summary>
	/// Thumbnail information.
	/// </summary>
	[JsonPropertyName("thumbnail")]
	public EmbedThumbnail? Thumbnail { get; init; }

	/// <summary>
	/// Video information.
	/// </summary>
	[JsonPropertyName("video")]
	public EmbedVideo? Video { get; init; }

	/// <summary>
	/// Provider information.
	/// </summary>
	[JsonPropertyName("provider")]
	public EmbedProvider? Provider { get; init; }

	/// <summary>
	/// Author information.
	/// </summary>
	[JsonPropertyName("author")]
	public EmbedAuthor? Author { get; init; }

	/// <summary>
	/// Fields information.
	/// </summary>
	[JsonPropertyName("fields")]
	public EmbedField[]? Fields { get; init; }
}
