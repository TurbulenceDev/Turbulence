using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md">GitHub</a>.
/// </summary>
public record CreateMessageParams {
	/// <summary>
	/// Message contents (up to 2000 characters).
	/// </summary>
	[JsonPropertyName("content")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Content { get; init; }

	/// <summary>
	/// Can be used to verify a message was sent (up to 25 characters). Value will appear in the <a href="#DOCS_TOPICS_GATEWAY_EVENTS/message-create">Message Create event</a>.
	/// </summary>
	[JsonPropertyName("nonce")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public /* integer or string */ dynamic? Nonce { get; init; }

	/// <summary>
	/// <C>true</c> if this is a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Tts { get; init; }

	/// <summary>
	/// Up to 10 <c>rich</c> embeds (up to 6000 characters).
	/// </summary>
	[JsonPropertyName("embeds")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Embed[]? Embeds { get; init; }

	/// <summary>
	/// Allowed mentions for the message.
	/// </summary>
	/*[JsonPropertyName("allowed_mentions")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public AllowedMentions? AllowedMentions { get; init; }*/ // TODO: allowed mentions

	/// <summary>
	/// Include to make your message a reply.
	/// </summary>
	[JsonPropertyName("message_reference")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public MessageReference? MessageReference { get; init; }

	/// <summary>
	/// Components to include with the message.
	/// </summary>
	/*[JsonPropertyName("components")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public MessageComponent[]? Components { get; init; }*/ // TODO: message components

	/// <summary>
	/// IDs of up to 3 <a href="https://discord.com/developers/docs/resources/sticker#sticker-object">stickers</a> in the server to send in the message.
	/// </summary>
	[JsonPropertyName("sticker_ids")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake[]? StickerIds { get; init; }

	// TODO: Implement files[n] bullshit (https://discord.com/developers/docs/reference#uploading-files)
	/// <summary>
	/// JSON-encoded body of non-file params, only for <c>multipart/form-data</c> requests. See <a href="https://discord.com/developers/docs/reference#uploading-files">Uploading Files</a>.
	/// </summary>
	[JsonPropertyName("payload_json")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? PayloadJson { get; init; }

	/// <summary>
	/// Attachment objects with filename and description. See <a href="https://discord.com/developers/docs/reference#uploading-files">Uploading Files</a>.
	/// </summary>
	[JsonPropertyName("attachments")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Attachment[]? Attachments { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-flags">Message flags</a> combined as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a> (only <c>SUPPRESS_EMBEDS</c> and <c>SUPPRESS_NOTIFICATIONS</c> can be set).
	/// </summary>
	[JsonPropertyName("flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Flags { get; init; }
}
