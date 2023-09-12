using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordSticker;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/sticker#sticker-item-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Sticker.md#sticker-item-object">GitHub
/// </a>.
/// </summary>
public record StickerItem {
	/// <summary>
	/// Snowflake ID of the sticker.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// Name of the sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-format-types">Type of
	/// sticker format</a>.
	/// </summary>
	[JsonPropertyName("format_type")]
	public required int FormatType { get; init; }
}
