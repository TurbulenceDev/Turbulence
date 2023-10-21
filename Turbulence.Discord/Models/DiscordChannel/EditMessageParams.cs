using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md">GitHub</a>.
/// </summary>
public record EditMessageParams {
	/// <summary>
	/// Message contents (up to 2000 characters).
	/// </summary>
	[JsonPropertyName("content")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Content { get; init; }

	/// <summary>
	/// Up to 10 <c>rich</c> embeds (up to 6000 characters).
	/// </summary>
	[JsonPropertyName("embeds")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Embed[]? Embeds { get; init; }

	/// <summary>
	/// Edit the <a href="https://discord.com/developers/docs/resources/channel#message-object-message-flags">flags</a> of a message (only <c>SUPPRESS_EMBEDS</c> can currently be set/unset).
	/// </summary>
	[JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Flags { get; init; }

	/// <summary>
	/// Allowed mentions for the message.
	/// </summary>
	[JsonPropertyName("allowed_mentions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AllowedMentions? AllowedMentions { get; init; }

	/// <summary>
	/// Components to include with the message.
	/// </summary>
	[JsonPropertyName("components")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public /*MessageComponent[]?*/ dynamic? Components { get; init; }

    // TODO: Implement files[n] bullshit (https://discord.com/developers/docs/reference#uploading-files)
    /// <summary>
    /// JSON-encoded body of non-file params (multipart/form-data only). See <a href="https://discord.com/developers/docs/reference#uploading-files">Uploading Files</a>.
    /// </summary>
    [JsonPropertyName("payload_json")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PayloadJson { get; init; }

	/// <summary>
	/// Attached files to keep and possible descriptions for new files. See <a href="https://discord.com/developers/docs/reference#uploading-files">Uploading Files</a>.
	/// </summary>
	[JsonPropertyName("attachments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Attachment[]? Attachments { get; init; }
}
