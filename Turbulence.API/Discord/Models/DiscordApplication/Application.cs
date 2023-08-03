using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordTeams;
using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordApplication;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/application#application-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Application.md#application-object">
/// GitHub</a>.
/// </summary>
public record Application {
	/// <summary>
	/// The snowflake ID of the app.
	/// </summary>
	[JsonPropertyName("id")]
	public required ulong Id { get; init; }

	/// <summary>
	/// The name of the app.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/reference#image-formatting">icon hash</a> of the app.
	/// </summary>
	[JsonPropertyName("icon")]
	public required string? Icon { get; init; }

	/// <summary>
	/// The description of the app.
	/// </summary>
	[JsonPropertyName("description")]
	public required string Description { get; init; }

	/// <summary>
	/// An array of RPC origin URLs, if RPC is enabled.
	/// </summary>
	[JsonPropertyName("rpc_origins")]
	public string[]? RpcOrigins { get; init; }

	/// <summary>
	/// When false only app owner can join the app's bot to guilds.
	/// </summary>
	[JsonPropertyName("bot_public")]
	public required bool BotPublic { get; init; }

	/// <summary>
	/// When true the app's bot will only join upon completion of the full OAuth2  code grant flow.
	/// </summary>
	[JsonPropertyName("bot_require_code_grant")]
	public required bool BotRequireCodeGrant { get; init; }

	/// <summary>
	/// The URL of the app's terms of service.
	/// </summary>
	[JsonPropertyName("terms_of_service_url")]
	public Uri? TermsOfServiceUrl { get; init; }

	/// <summary>
	/// The URL of the app's privacy policy.
	/// </summary>
	[JsonPropertyName("privacy_policy_url")]
	public Uri? PrivacyPolicyUrl { get; init; }

	/// <summary>
	/// Partial user object containing info on the owner of the application.
	/// </summary>
	[JsonPropertyName("owner")]
	public User? Owner { get; init; }

	/// <summary>
	/// An empty string.
	/// </summary>
	[Obsolete("Will be removed in API v11")]
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// The hex encoded key for verification in interactions and the GameSDK's
	/// <a href="https://discord.com/developers/docs/game-sdk/applications#getticket">GetTicket</a>.
	/// </summary>
	[JsonPropertyName("verify_key")]
	public required string VerifyKey { get; init; }

	/// <summary>
	/// If the application belongs to a team, this will be a list of the members of that team.
	/// </summary>
	[JsonPropertyName("team")]
	public required Team? Team { get; init; }

	/// <summary>
	/// Guild associated with the app. For example, a developer support server.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public ulong? GuildId { get; init; }

	/// <summary>
	/// A partial object of the associated guild.
	/// </summary>
	[JsonPropertyName("guild")]
	public Guild? Guild { get; init; }

	/// <summary>
	/// If this application is a game sold on Discord, this field will be the snowflake ID of the "Game SKU" that is
	/// created, if exists.
	/// </summary>
	[JsonPropertyName("primary_sku_id")]
	public ulong? PrimarySkuId { get; init; }

	/// <summary>
	/// If this application is a game sold on Discord, this field will be the URL slug that links to the store page.
	/// </summary>
	[JsonPropertyName("slug")]
	public Uri? Slug { get; init; }

	/// <summary>
	/// The application's default rich presence invite
	/// <a href="https://discord.com/developers/docs/reference#image-formatting">cover image hash</a>.
	/// </summary>
	[JsonPropertyName("cover_image")]
	public string? CoverImage { get; init; }

	// TODO: Make enum
	/// <summary>
	/// The application's public
	/// <a href="https://discord.com/developers/docs/resources/application#application-object-application-flags">flags</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	public int? Flags { get; init; }

	/// <summary>
	/// An approximate count of the app's guild membership.
	/// </summary>
	[JsonPropertyName("approximate_guild_count")]
	public int? ApproximateGuildCount { get; init; }

	/// <summary>
	/// Up to 5 tags describing the content and functionality of the application.
	/// </summary>
	[JsonPropertyName("tags")]
	public string[]? Tags { get; init; }

	/// <summary>
	/// Settings for the application's default in-app authorization link, if enabled.
	/// </summary>
	[JsonPropertyName("install_params")]
	public InstallParams? InstallParams { get; init; }

	/// <summary>
	/// The application's default custom authorization link, if enabled.
	/// </summary>
	[JsonPropertyName("custom_install_url")]
	public Uri? CustomInstallUrl { get; init; }

	/// <summary>
	/// The application's role connection verification entry point, which when configured will render the app as a
	/// verification method in the guild role verification configuration.
	/// </summary>
	[JsonPropertyName("role_connections_verification_url")]
	public Uri? RoleConnectionsVerificationUrl { get; init; }
}
