using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md">GitHub</a>.
/// </summary>
public record Overwrite {
	/// <summary>
	/// Role or user snowflake ID.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonConverter(typeof(SnowflakeConverter))]
	public required Snowflake Id { get; init; }

	// TODO: Make enum
	/// <summary>
	/// Either 0 (role) or 1 (member).
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	/// <summary>
	/// Permission bit set.
	///
	/// See <a href="https://discord.com/developers/docs/topics/permissions#permissions">permissions</a> for more
	/// information.
	/// </summary>
	[JsonPropertyName("allow")]
	public required string Allow { get; init; }

	/// <summary>
	/// Permission bit set.
	/// 
	/// See <a href="https://discord.com/developers/docs/topics/permissions#permissions">permissions</a> for more
	/// information.
	/// </summary>
	[JsonPropertyName("deny")]
	public required string Deny { get; init; }
}
