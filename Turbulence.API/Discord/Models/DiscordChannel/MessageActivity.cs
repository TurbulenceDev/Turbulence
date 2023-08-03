using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#message-object-message-activity-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#message-activity-structure">
/// GitHub</a>.
/// </summary>
public record MessageActivity {
	// TODO: Add enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/channel#message-object-message-activity-types">Type of
	/// message activity</a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	// TODO: Set <see cref=""> for party_id
	/// <summary>
	/// <c>party_id</c> from a
	/// <a href="https://discord.com/developers/docs/rich-presence/how-to/updating-presence-update-presence-payload-fields#updating-presence-update-presence-payload-fields">
	/// Rich Presence event</a>.
	/// </summary>
	[JsonPropertyName("party_id")]
	public string? PartyId { get; init; }
}
