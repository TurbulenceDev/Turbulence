using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGuild;

/// <summary>
/// See the
/// <a href="https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-structure">Discord
/// API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Guild.md#welcome-screen-structure">
/// GitHub</a>.
/// </summary>
public record WelcomeScreen {
	/// <summary>
	/// The server description shown in the welcome screen.
	/// </summary>
	[JsonPropertyName("description")]
	public required string? Description { get; init; }

	/// <summary>
	/// The channels shown in the welcome screen, up to 5.
	/// </summary>
	[JsonPropertyName("welcome_channels")]
	public required WelcomeScreenChannel[] WelcomeChannels { get; init; }
}
