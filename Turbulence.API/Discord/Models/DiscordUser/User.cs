using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordUser;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/user#user-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/User.md#user-object">GitHub</a>. 
/// </summary>
public record User {
	/// <summary>
	/// The user's snowflake ID.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonConverter(typeof(SnowflakeConverter))]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// The user's username, not unique across the platform.
	/// </summary>
	[JsonPropertyName("username")]
	public required string Username { get; init; }

	/// <summary>
	/// The user's Discord-tag.
	/// </summary>
	[JsonPropertyName("discriminator")]
	public required string Discriminator { get; init; }

	/// <summary>
	/// The user's display name, if it is set. For bots, this is the application name.
	/// </summary>
	[JsonPropertyName("global_name")]
	public required string? GlobalName { get; init; }

	/// <summary>
	/// The user's <a href="https://discord.com/developers/docs/reference#image-formatting">avatar hash</a>.
	/// </summary>
	[JsonPropertyName("avatar")]
	public required string? Avatar { get; init; }

	/// <summary>
	/// Whether the user belongs to an OAuth2 application.
	/// </summary>
	[JsonPropertyName("bot")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Bot { get; init; }

	/// <summary>
	/// Whether the user is an Official Discord System user (part of the urgent message system).
	/// </summary>
	[JsonPropertyName("system")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? System { get; init; }

	/// <summary>
	/// Whether the user has two factor enabled on their account.
	/// </summary>
	[JsonPropertyName("mfa_enabled")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? MfaEnabled { get; init; }

	/// <summary>
	/// The user's <a href="https://discord.com/developers/docs/reference#image-formatting">banner hash</a>.
	/// </summary>
	[JsonPropertyName("banner")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Banner { get; init; }

	/// <summary>
	/// The user's banner color encoded as an integer representation of hexadecimal color code.
	/// </summary>
	[JsonPropertyName("accent_color")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? AccentColor { get; init; }

	/// <summary>
	/// The user's chosen <a href="https://discord.com/developers/docs/reference#locales">language option</a>.
	/// </summary>
	[JsonPropertyName("locale")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Locale { get; init; }

	/// <summary>
	/// Whether the email on this account has been verified.
	/// </summary>
	[JsonPropertyName("verified")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Verified { get; init; }

	/// <summary>
	/// The user's email.
	/// </summary>
	[JsonPropertyName("email")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Email { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/user#user-object-user-flags">flags</a> on a user's account.
	/// </summary>
	[JsonPropertyName("flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Flags { get; init; }

	/// <summary>
	/// The <a href="https://discord.com/developers/docs/resources/user#user-object-premium-types">type of Nitro subscription</a> on a user's account.
	/// </summary>
	[JsonPropertyName("premium_type")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? PremiumType { get; init; }

	/// <summary>
	/// The public <a href="https://discord.com/developers/docs/resources/user#user-object-user-flags">flags</a> on a user's account.
	/// </summary>
	[JsonPropertyName("public_flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? PublicFlags { get; init; }

	/// <summary>
	/// The user's <a href="https://discord.com/developers/docs/reference#image-formatting">avatar decoration hash</a>.
	/// </summary>
	[JsonPropertyName("avatar_decoration")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? AvatarDecoration { get; init; }
}
