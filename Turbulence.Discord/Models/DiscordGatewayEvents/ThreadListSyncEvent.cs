using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models;

namespace Turbulence.Discord.Models.DiscordGatewayEvents;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md">GitHub</a>.
/// </summary>
public record ThreadListSyncEvent {
	/// <summary>
	/// ID of the guild.
	/// </summary>
	[JsonPropertyName("guild_id")]
	public required Snowflake GuildId { get; init; }

	/// <summary>
	/// Parent channel IDs whose threads are being synced.  If omitted, then threads were synced for the entire guild.  This array may contain channel_ids that have no active threads as well, so you know to clear that data.
	/// </summary>
	[JsonPropertyName("channel_ids")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake[]? ChannelIds { get; init; }

	/// <summary>
	/// All active threads in the given channels that the current user can access.
	/// </summary>
	[JsonPropertyName("threads")]
	public required Channel[] Threads { get; init; }

	/// <summary>
	/// All thread member objects from the synced threads for the current user, indicating which threads the current user has been added to.
	/// </summary>
	[JsonPropertyName("members")]
	public ThreadMember[]? Members { get; init; }
}
