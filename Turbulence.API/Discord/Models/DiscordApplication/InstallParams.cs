using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordApplication;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/application#install-params-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Application.md#install-params-object">
/// GitHub</a>.
/// </summary>
public record InstallParams {
	/// <summary>
	/// The <a href="https://discord.com/developers/docs/topics/oauth2#shared-resources-oauth2-scopes">scopes</a> to add
	/// the application to the server with.
	/// </summary>
	[JsonPropertyName("scopes")]
	public required string[] Scopes { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/topics/permissions">permissions</a> to request for the bot role.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required string Permissions { get; init; }
}
