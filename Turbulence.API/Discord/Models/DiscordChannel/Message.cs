using Turbulence.API.Discord.Models.DiscordApplication;
// using Turbulence.API.Discord.Models.DiscordMessageComponents;
using Turbulence.API.Discord.Models.DiscordPermissions;
using Turbulence.API.Discord.Models.DiscordReceivingAndResponding;
using Turbulence.API.Discord.Models.DiscordSticker;
using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// Represents a message sent in a channel within Discord.
/// 
/// See the <a href="https://discord.com/developers/docs/resources/channel#message-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#message-object">GitHub
/// </a>.
/// </summary>
public record Message {
	/// <summary>
	/// Snowflake ID of the message.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong Id { get; init; }

	/// <summary>
	/// Snowflake ID of the channel the message was sent in.
	/// </summary>
	[JsonPropertyName("channel_id")]
	public required ulong ChannelId { get; init; }

	/// <summary>
	/// The author of this message.
	///
	/// The author object follows the structure of the user object, but is only a valid user in the case where the
	/// message is generated by a user or bot user. If the message is generated by a webhook, the author object
	/// corresponds to the webhook's snowflake ID, username, and avatar. You can tell if a message is generated by a
	/// webhook by checking for the <see cref="WebhookId" /> on the message object.
	/// </summary>
	[JsonPropertyName("author")]
	public required User Author { get; init; }

	/// <summary>
	/// Contents of the message.
	/// </summary>
	[JsonPropertyName("content")]
	public required string Content { get; init; }

	// TODO: Deserialize ISO8601 timestamp to something usable
	/// <summary>
	/// When this message was sent.
	/// </summary>
	[JsonPropertyName("timestamp")]
	public required string Timestamp { get; init; }

	// TODO: Deserialize ISO8601 timestamp to something usable
	/// <summary>
	/// When this message was edited (or null if never).
	/// </summary>
	[JsonPropertyName("edited_timestamp")]
	public required string? EditedTimestamp { get; init; }

	/// <summary>
	/// Whether this was a TTS message.
	/// </summary>
	[JsonPropertyName("tts")]
	public required bool Tts { get; init; }

	/// <summary>
	/// Whether this message mentions everyone.
	/// </summary>
	[JsonPropertyName("mention_everyone")]
	public required bool MentionEveryone { get; init; }

	/// <summary>
	/// Users specifically mentioned in the message.
	/// </summary>
	[JsonPropertyName("mentions")]
	public required User[] Mentions { get; init; }

	/// <summary>
	/// Roles specifically mentioned in this message.
	/// </summary>
	[JsonPropertyName("mention_roles")]
	public required Role[] MentionRoles { get; init; }

	/// <summary>
	/// Channels specifically mentioned in this message.
	///
	/// Not all channel mentions in a message will appear in here. Only textual channels that are visible to everyone in
	/// a lurkable guild will ever be included. Only crossposted messages (via Channel Following) currently include this
	/// at all. If no mentions in the message meet these requirements, this field will not be sent.
	/// </summary>
	[JsonPropertyName("mention_channels")]
	public required ChannelMention[] MentionChannels { get; init; }

	/// <summary>
	/// Any attached files.
	/// </summary>
	[JsonPropertyName("attachments")]
	public required Attachment[] Attachments { get; init; }

	/// <summary>
	/// Any embedded content.
	/// </summary>
	[JsonPropertyName("embeds")]
	public required Embed[] Embeds { get; init; }

	/// <summary>
	/// Reactions to the message.
	/// </summary>
	[JsonPropertyName("reactions")]
	public Reaction[]? Reactions { get; init; }

	// TODO: Deserialize this into an actual type
	/// <summary>
	/// Used for validating a message was sent.
	/// </summary>
	[JsonPropertyName("nonce")]
	public /* integer or string */ dynamic? Nonce { get; init; }

	/// <summary>
	/// Whether this message is pinned.
	/// </summary>
	[JsonPropertyName("pinned")]
	public required bool Pinned { get; init; }

	/// <summary>
	/// If the message is generated by a webhook, this is the webhook's snowflake ID.
	/// </summary>
	[JsonPropertyName("webhook_id")]
	public ulong? WebhookId { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-types">Type of message</a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; } // TODO: Make enum

	/// <summary>
	/// Sent with Rich Presence-related chat embeds.
	/// </summary>
	[JsonPropertyName("activity")]
	public MessageActivity? Activity { get; init; }

	/// <summary>
	/// Sent with Rich Presence-related chat embeds.
	/// </summary>
	[JsonPropertyName("application")]
	public Application? Application { get; init; }

	/// <summary>
	/// If the message is an
	/// <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object">
	/// Interaction</a> or application-owned webhook, this is the snowflake ID of the application.
	/// </summary>
	[JsonPropertyName("application_id")]
	public ulong? ApplicationId { get; init; }

	/// <summary>
	/// Data showing the source of a crosspost, channel follow add, pin, or reply message.
	/// </summary>
	[JsonPropertyName("message_reference")]
	public MessageReference? MessageReference { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-flags">Message flags</a>
	/// combined as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	public int? Flags { get; init; }

	/// <summary>
	/// The message associated with the <see cref="MessageReference"/>.
	///
	/// This field is only returned for messages with a type of <c>19 (REPLY)</c> or <c>21 (THREAD_STARTER_MESSAGE)</c>.
	/// If the message is a reply but this field is not present, the backend did not attempt to fetch the message that
	/// was being replied to, so its state is unknown. If the field exists but is null, the referenced message was
	/// deleted.
	/// </summary>
	[JsonPropertyName("referenced_message")]
	public required Message? ReferencedMessage { get; init; }

	/// <summary>
	/// Sent if the message is a response to an
	/// <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object">
	/// Interaction</a>.
	/// </summary>
	[JsonPropertyName("interaction")]
	public MessageInteraction? Interaction { get; init; }

	/// <summary>
	/// The thread that was started from this message, includes
	/// <a href="https://discord.com/developers/docs/resources/channel#thread-member-object">thread member</a> object.
	/// </summary>
	[JsonPropertyName("thread")]
	public Channel? Thread { get; init; }

	/// <summary>
	/// Sent if the message contains components like buttons, action rows, or other interactive components.
	/// </summary>
	[JsonPropertyName("components")]
	public required /*MessageComponent[]*/ dynamic Components { get; init; } // TODO: Implement

	/// <summary>
	/// Sent if the message contains stickers.
	/// </summary>
	[JsonPropertyName("sticker_items")]
	public StickerItem[]? StickerItems { get; init; }

	/// <summary>
	/// The stickers sent with the message.
	/// </summary>
	[JsonPropertyName("stickers")]
	[Obsolete("Use StickerItems")]
	public Sticker[]? Stickers { get; init; }

	// TODO: Set <see cref=""> for total_message_sent
	/// <summary>
	/// A generally increasing integer (there may be gaps or duplicates) that represents the approximate position of the
	/// message in a thread, it can be used to estimate the relative position of the message in a thread in company with
	/// <c>total_message_sent</c> on parent thread.
	/// </summary>
	[JsonPropertyName("position")]
	public int? Position { get; init; }

	/// <summary>
	/// Data of the role subscription purchase or renewal that prompted this <c>ROLE_SUBSCRIPTION_PURCHASE</c> message.
	/// </summary>
	[JsonPropertyName("role_subscription_data")]
	public RoleSubscriptionData? RoleSubscriptionData { get; init; }
}
