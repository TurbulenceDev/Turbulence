using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
///	See the
/// <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-party"> Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-party">
/// GitHub</a>.
/// </summary>
public record ActivityParty {
	/// <summary>
	/// ID of the party.
	/// </summary>
	[JsonPropertyName("id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Id { get; init; }

	/// <summary>
	/// Used to show the party's current and maximum size. The array contains two integers, <c>current_size</c> and
	/// <c>max_size</c>.
	/// </summary>
	[JsonPropertyName("size")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int[]? Size { get; init; } // TODO: Maybe this can be serialized from and deserialized into two properties
}
