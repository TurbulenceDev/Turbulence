using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Models.DiscordTeams;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/teams#data-models-team-member-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Teams.md#team-member-object">GitHub</a>.
/// </summary>
public record TeamMember {
	// TODO: Make enum
	/// <summary>
	/// The user's <a href="https://discord.com/developers/docs/topics/teams#data-models-membership-state-enum">
	/// membership state</a> on the team.
	/// </summary>
	[JsonPropertyName("membership_state")]
	public required int MembershipState { get; init; }

	/// <summary>
	/// Will always be <c>["*"]</c>.
	/// </summary>
	[JsonPropertyName("permissions")]
	public required string[] Permissions { get; init; }

	/// <summary>
	/// The snowflake ID of the parent team of which they are a member.
	/// </summary>
	[JsonPropertyName("team_id")]
	public required Snowflake TeamId { get; init; }

	/// <summary>
	/// The avatar, discriminator, snowflake ID, and username of the user.
	/// </summary>
	[JsonPropertyName("user")]
	public required User User { get; init; }
}
