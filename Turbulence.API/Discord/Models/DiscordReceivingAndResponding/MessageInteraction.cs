using Turbulence.API.Discord.Models.DiscordUser;
using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;
using Turbulence.API.Discord.Models.DiscordGuild;

namespace Turbulence.API.Discord.Models.DiscordReceivingAndResponding;

/// <summary>
/// See the
/// <a href="https://discord.com/developers/docs/interactions/receiving-and-responding#message-interaction-object-message-interaction-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/interactions/Receiving_and_Responding.md#message-interaction-structure">
/// GitHub</a>.
/// </summary>
public record MessageInteraction {
	/// <summary>
	/// Snowflake ID of the interaction.
	/// </summary>
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	// TODO: Enum
	/// <summary>
	/// Type of interaction.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; }

	/// <summary>
	/// Name of the
	/// <a href="https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure">
	/// application command</a>, including subcommands and subcommand groups.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// User who invoked the interaction.
	/// </summary>
	[JsonPropertyName("user")]
	public required User User { get; init; }

	/// <summary>
	/// Member who invoked the interaction in the guild.
	/// </summary>
	[JsonPropertyName("member")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public GuildMember? Member { get; init; }
}
