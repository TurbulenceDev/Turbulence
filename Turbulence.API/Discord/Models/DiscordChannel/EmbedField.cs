using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-field-structure">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-field-structure">
/// GitHub</a>.
/// </summary>
public record EmbedField {
	/// <summary>
	/// Name of the field.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// Value of the field.
	/// </summary>
	[JsonPropertyName("value")]
	public required string Value { get; init; }

	/// <summary>
	/// Whether or not this field should display inline.
	/// </summary>
	[JsonPropertyName("inline")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Inline { get; init; }
}
