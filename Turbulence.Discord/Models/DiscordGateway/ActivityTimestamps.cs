using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-timestamps">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-timestamps">
/// GitHub</a>.
/// </summary>
public record ActivityTimestamps {
	/// <summary>
	/// Unix time (in milliseconds) of when the activity started.
	/// </summary>
	[JsonPropertyName("start")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public long? Start { get; init; }

	/// <summary>
	/// Unix time (in milliseconds) of when the activity ends.
	/// </summary>
	[JsonPropertyName("end")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public long? End { get; init; }
}
