using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordSticker;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/sticker#sticker-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Sticker.md#sticker-object">GitHub
/// </a>.
/// </summary>
public record Sticker {
	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">ID of the sticker</a>.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonConverter(typeof(SnowflakeConverter))]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// For standard stickers, snowflake ID of the pack the sticker is from.
	/// </summary>
	[JsonPropertyName("pack_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonConverter(typeof(SnowflakeConverter))]
	public Snowflake? PackId { get; init; }

	/// <summary>
	/// Name of the sticker.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// Description of the sticker.
	/// </summary>
	[JsonPropertyName("description")]
	public required string? Description { get; init; }

	/// <summary>
	/// Autocomplete/suggestion tags for the sticker (max 200 characters).
	/// </summary>
	[JsonPropertyName("tags")]
	public required string Tags { get; init; }

	/// <summary>
	/// Previously the sticker asset hash, now an empty string.
	/// </summary>
	[JsonPropertyName("asset")]
	[Obsolete]
	public string? Asset { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-types">Type of sticker</a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	// TODO: Make enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-format-types">Type of
	/// sticker format</a>.
	/// </summary>
	[JsonPropertyName("format_type")]
	public required int FormatType { get; init; }

	/// <summary>
	/// Whether this guild sticker can be used, may be false due to loss of Server Boosts.
	/// </summary>
	[JsonPropertyName("available")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Available { get; init; }

	/// <summary>
	/// Snowflake ID of the guild that owns this sticker.
	/// </summary>
	[JsonPropertyName("guild_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonConverter(typeof(SnowflakeConverter))]
	public Snowflake? GuildId { get; init; }

	/// <summary>
	/// The user that uploaded the guild sticker.
	/// </summary>
	[JsonPropertyName("user")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public User? User { get; init; }

	/// <summary>
	/// The standard sticker's sort order within its pack.
	/// </summary>
	[JsonPropertyName("sort_value")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? SortValue { get; init; }
}
