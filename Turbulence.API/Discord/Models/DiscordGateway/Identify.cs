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
	/// Undocumented.
	/// 
	/// Probably a bit field similar to <see cref="Intents"/>.
	/// </summary>
	[JsonPropertyName("capabilities")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Capabilities { get; init; }
	
	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway-events#identify-identify-connection-properties">
	/// Connection properties</a>.
	/// </summary>
	[JsonPropertyName("properties")]
	public required IdentifyConnectionProperties Properties { get; init; }

	/// <summary>
	/// Presence structure for initial presence information.
	/// </summary>
	[JsonPropertyName("presence")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public GatewayPresenceUpdate? Presence { get; init; }
	
	/// <summary>
	/// Whether this connection supports compression of packets.
	/// </summary>
	[JsonPropertyName("compress")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Compress { get; init; }

	/// <summary>
	/// Value between 50 and 250, total number of members where the gateway will stop sending offline members in the
	/// guild member list.
	/// </summary>
	[JsonPropertyName("large_threshold")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public byte? LargeThreshold { get; init; }

	/// <summary>
	/// Used for <a href="https://discord.com/developers/docs/topics/gateway#sharding">Guild Sharding</a>.
	/// </summary>
	[JsonPropertyName("shard")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int[]? Shard { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway#gateway-intents">Gateway Intents</a> you wish to
	/// receive.
	///
	/// Required by documentation, but doesn't seem to be used by clients. Probably only for bots.
	/// <see cref="Capabilities"/> are (probably) similar.
	/// </summary>
	[JsonPropertyName("intents")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Intents { get; init; }

	/// <summary>
	/// Undocumented.
	/// </summary>
	[JsonPropertyName("client_state")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ClientState? ClientState { get; init; }
}

/// <summary>
/// Undocumented object part of <see cref="Identify"/>.
/// </summary>
public record ClientState
{
	[JsonPropertyName("guild_versions")]
	public required object GuildVersions { get; init; } = new();

	[JsonPropertyName("highest_last_message_id")]
	public required string HighestLastMessageId { get; init; } = "0";

	[JsonPropertyName("read_state_version")]
	public required int ReadStateVersion { get; init; } = 0;

	[JsonPropertyName("user_guild_settings_version")]
	public required int UserGuildSettingsVersion { get; init; } = -1;

	[JsonPropertyName("user_settings_version")]
	public required int UserSettingsVersion { get; init; } = -1;

	[JsonPropertyName("private_channels_version")]
	public required string PrivateChannelsVersion { get; init; } = "0";

	[JsonPropertyName("api_code_version")]
	public required int ApiCodeVersion { get; init; } = 0;

}