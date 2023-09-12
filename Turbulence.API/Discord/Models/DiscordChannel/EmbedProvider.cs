using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#embed-object-embed-provider-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#embed-provider-structure">
/// GitHub</a>.
/// </summary>
public record EmbedProvider {
	/// <summary>
	/// Name of provider.
	/// </summary>
	[JsonPropertyName("name")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Name { get; init; }

	/// <summary>
	/// URL of provider.
	/// </summary>
	[JsonPropertyName("url")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Uri? Url { get; init; }
}
