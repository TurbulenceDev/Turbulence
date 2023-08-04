using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordPermissions;

/// <summary>
/// Roles represent a set of permissions attached to a group of users. Roles have names, colors, and can be "pinned" to
/// the side bar, causing their members to be listed separately. Roles can have separate permission profiles for the
/// global context (guild) and channel context. The <c>@everyone</c> role has the same ID as the guild it belongs to.
/// 
/// See the <a href="https://discord.com/developers/docs/topics/permissions#role-object">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Permissions.md#role-object">GitHub</a>.
/// </summary>
public record Role {
	/// <summary>
	/// Role snowflake ID.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonConverter(typeof(SnowflakeConverter))]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// Role name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// Integer representation of hexadecimal color code.
	/// </summary>
	[JsonPropertyName("color")]
	public required int Color { get; init; }

	/// <summary>
	/// If this role is pinned in the user listing.
	/// </summary>
	[JsonPropertyName("hoist")]
	public required bool Hoist { get; init; }

	/// <summary>
	/// Role <a href="https://discord.com/developers/docs/reference#image-formatting">icon hash</a>.
	/// </summary>
	[JsonPropertyName("icon")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Icon { get; init; }

	/// <summary>
	/// Role unicode emoji.
	/// </summary>
	[JsonPropertyName("unicode_emoji")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? UnicodeEmoji { get; init; }

	/// <summary>
	/// Position of this role.
	/// </summary>
	[JsonPropertyName("position")]
	public required int Position { get; init; }

	// TODO: Enum?
	/// <summary>
	/// Permission bit set.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required string Permissions { get; init; }

	/// <summary>
	/// Whether this role is managed by an integration.
	/// </summary>
	[JsonPropertyName("managed")]
	public required bool Managed { get; init; }

	/// <summary>
	/// Whether this role is mentionable.
	/// </summary>
	[JsonPropertyName("mentionable")]
	public required bool Mentionable { get; init; }

	/// <summary>
	/// The tags this role has.
	/// </summary>
	[JsonPropertyName("tags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public RoleTags? Tags { get; init; }

	// TODO: Enum
	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/permissions#role-object-role-flags">Role flags</a> combined
	/// as a <a href="https://en.wikipedia.org/wiki/Bit_field">bitfield</a>.
	/// </summary>
	[JsonPropertyName("flags")]
	public required int Flags { get; init; }
}
