using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
/// Used to trigger the initial handshake with the gateway. Details about identifying is in the
/// <a href="https://discord.com/developers/docs/topics/gateway#identifying">Gateway documentation</a>.
/// See the <a href="https://discord.com/developers/docs/topics/gateway-events#identify">Discord API documentation</a>
/// or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#identify">GitHub</a>.
/// </summary>
public record Identify {
	/// <summary>
	/// Authentication token.
	/// </summary>
	[JsonPropertyName("token")]
	public required string Token { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-connection-properties">
	/// Connection properties</a>.
	/// </summary>
	[JsonPropertyName("properties")]
	public required IdentifyConnectionProperties Properties { get; init; }

	/// <summary>
	/// Whether this connection supports compression of packets.
	/// </summary>
	[JsonPropertyName("compress")]
	public bool? Compress { get; init; }

	/// <summary>
	/// Value between 50 and 250, total number of members where the gateway will stop sending offline members in the
	/// guild member list.
	/// </summary>
	[JsonPropertyName("large_threshold")]
	public ushort? LargeThreshold { get; init; }

	/// <summary>
	/// Used for <a href="https://discord.com/developers/docs/topics/gateway#sharding">Guild Sharding</a>.
	/// </summary>
	[JsonPropertyName("shard")]
	public int[]? Shard { get; init; }

	/// <summary>
	/// Presence structure for initial presence information.
	/// </summary>
	[JsonPropertyName("presence")]
	public GatewayPresenceUpdate? Presence { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway#gateway-intents">Gateway Intents</a> you wish to
	/// receive.
	/// </summary>
	[JsonPropertyName("intents")]
	public required int Intents { get; init; } // TODO: Enum
}
