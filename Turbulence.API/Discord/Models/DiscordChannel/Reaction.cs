using Turbulence.API.Discord.Models.DiscordEmoji;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#reaction-object">Discord API documentation
/// </a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#reaction-object">
/// GitHub</a>.
/// </summary>
public record Reaction {
	/// <summary>
	/// Times this emoji has been used to react.
	/// </summary>
	[JsonPropertyName("count")]
	public required int Count { get; init; }

	/// <summary>
	/// Whether the current user reacted using this emoji.
	/// </summary>
	[JsonPropertyName("me")]
	public required bool Me { get; init; }

	/// <summary>
	/// Emoji information.
	/// </summary>
	[JsonPropertyName("emoji")]
	public required Emoji Emoji { get; init; }
}
