using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-object">GitHub</a>.
/// </summary>
public record Embed {
	/// <summary>
	/// Title of embed.
	/// </summary>
	[JsonPropertyName("title")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Title { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-types">Type of embed</a>
	/// (always "rich" for webhook embeds).
	/// </summary>
	[JsonPropertyName("type")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Type { get; init; }

	/// <summary>
	/// Description of embed.
	/// </summary>
	[JsonPropertyName("description")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Description { get; init; }

	/// <summary>
	/// URL of embed.
	/// </summary>
	[JsonPropertyName("url")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Uri? Url { get; init; }

	/// <summary>
	/// Timestamp of embed content.
	/// </summary>
	[JsonPropertyName("timestamp")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Timestamp { get; init; }

	/// <summary>
	/// Color code of the embed.
	/// </summary>
	[JsonPropertyName("color")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Color { get; init; }

	/// <summary>
	/// Footer information.
	/// </summary>
	[JsonPropertyName("footer")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedFooter? Footer { get; init; }

	/// <summary>
	/// Image information.
	/// </summary>
	[JsonPropertyName("image")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedImage? Image { get; init; }

	/// <summary>
	/// Thumbnail information.
	/// </summary>
	[JsonPropertyName("thumbnail")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedThumbnail? Thumbnail { get; init; }

	/// <summary>
	/// Video information.
	/// </summary>
	[JsonPropertyName("video")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedVideo? Video { get; init; }

	/// <summary>
	/// Provider information.
	/// </summary>
	[JsonPropertyName("provider")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedProvider? Provider { get; init; }

	/// <summary>
	/// Author information.
	/// </summary>
	[JsonPropertyName("author")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedAuthor? Author { get; init; }

	/// <summary>
	/// Fields information.
	/// </summary>
	[JsonPropertyName("fields")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public EmbedField[]? Fields { get; init; }
}
