using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#attachment-object">Discord API documentation
/// </a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#attachment-object">
/// GitHub</a>.
/// </summary>
public record Attachment {
	/// <summary>
	/// Attachment snowflake ID.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong Id { get; init; }

	/// <summary>
	/// Name of file attached.
	/// </summary>
	[JsonPropertyName("filename")]
	public required string Filename { get; init; }

	/// <summary>
	/// Description for the file (max 1024 characters).
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// The attachment's <a href="https://en.wikipedia.org/wiki/Media_type">media type</a>.
	/// </summary>
	[JsonPropertyName("content_type")]
	public string? ContentType { get; init; }

	/// <summary>
	/// Size of file in bytes.
	/// </summary>
	[JsonPropertyName("size")]
	public required int Size { get; init; }

	/// <summary>
	/// Source URL of file.
	/// </summary>
	[JsonPropertyName("url")]
	public required Uri Url { get; init; }

	/// <summary>
	/// A proxied URL of file.
	/// </summary>
	[JsonPropertyName("proxy_url")]
	public required Uri ProxyUrl { get; init; }

	/// <summary>
	/// Height of file (if image).
	/// </summary>
	[JsonPropertyName("height")]
	public int? Height { get; init; }

	/// <summary>
	/// Width of file (if image).
	/// </summary>
	[JsonPropertyName("width")]
	public int? Width { get; init; }

	/// <summary>
	/// Whether this attachment is ephemeral.
	/// </summary>
	[JsonPropertyName("ephemeral")]
	public required bool Ephemeral { get; init; }

	/// <summary>
	/// The duration of the audio file (currently for voice messages).
	/// </summary>
	[JsonPropertyName("duration_secs")]
	public float? DurationSecs { get; init; }

	/// <summary>
	/// Base64 encoded bytearray representing a sampled waveform (currently for voice messages).
	/// </summary>
	[JsonPropertyName("waveform")]
	public string? Waveform { get; init; }

	// TODO: Enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#attachment-object-attachment-flags">Attachment
	/// flags</a> combined as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	public int? Flags { get; init; }
}
