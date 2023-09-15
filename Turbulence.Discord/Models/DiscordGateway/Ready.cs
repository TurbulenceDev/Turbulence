using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordApplication;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Models.DiscordGateway;

/// <summary>
/// The ready event is dispatched when a client has completed the initial handshake with the gateway (for new sessions).
/// The ready event can be the largest and most complex event the gateway will send, as it contains all the state
/// required for a client to begin interacting with the rest of the platform.
///
/// <see cref="Guilds"/> are the guilds of which your client is a member. They start out as unavailable when you connect
/// to the gateway. As they become available, your bot will be notified via <c>Guild Create</c> events.
/// 
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events#ready">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#ready">GitHub</a>.
/// </summary>
public record Ready {
	/// <summary>
	/// <a href="https://discord.com/developers/docs/reference#api-versioning-api-versions">API version</a>.
	/// </summary>
	[JsonPropertyName("v")]
	public required int ApiVersion { get; init; }

	/// <summary>
	/// Information about the user.
	/// </summary>
	[JsonPropertyName("user")]
	public required User User { get; init; }

	/// <summary>
	/// (Possibly unavailable) guilds the user is in.
	/// </summary>
	[JsonPropertyName("guilds")]
	public required Guild[] Guilds { get; init; }

	/// <summary>
	/// Used for resuming connections.
	/// </summary>
	[JsonPropertyName("session_id")]
	public required string SessionId { get; init; }

	/// <summary>
	/// Gateway URL for resuming connections.
	/// </summary>
	[JsonPropertyName("resume_gateway_url")]
	public required string ResumeGatewayUrl { get; init; }

	// TODO: Deserialize properly
	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway#sharding">Shard information</a> associated with this
	/// session, if sent when identifying.
	/// </summary>
	[JsonPropertyName("shard")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int[]? Shard { get; init; }

	/// <summary>
	/// Partial application object.
	///
	/// Required according to Discord documentation, but not actually available. Perhaps because we are not a bot.
	/// </summary>
	[JsonPropertyName("application")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Application? Application { get; init; }

    [JsonPropertyName("private_channels")]
    public required Channel[] PrivateChannels { get; init; }

    [JsonPropertyName("users")]
    public required User[] Users { get; init; }
}
