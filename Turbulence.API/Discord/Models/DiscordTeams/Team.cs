using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordTeams;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/teams#data-models-team-object">Discord API documentation
/// </a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Teams.md#team-object">GitHub</a>.
/// </summary>
public record Team {
	/// <summary>
	/// A hash of the image of the team's icon.
	/// </summary>
	[JsonPropertyName("icon")]
	public required string? Icon { get; init; }

	/// <summary>
	/// The unique snowflake ID of the team.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	/// <summary>
	/// The members of the team.
	/// </summary>
	[JsonPropertyName("members")]
	public required TeamMember[] Members { get; init; }

	/// <summary>
	/// The name of the team.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// The user snowflake ID of the current team owner.
	/// </summary>
	[JsonPropertyName("owner_user_id")]
	public required Snowflake OwnerUserId { get; init; }
}
