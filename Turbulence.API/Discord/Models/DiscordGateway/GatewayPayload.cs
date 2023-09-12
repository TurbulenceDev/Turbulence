using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
/// See the
/// <a href="https://discord.com/developers/docs/topics/gateway-events#payload-structure">
/// Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#payload-structure">
/// GitHub</a>.
/// </summary>
public record GatewayPayload {
    /// <summary>
    /// <a href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes">Gateway
    /// opcode</a>, which indicates the payload type.
    /// </summary>
    [JsonPropertyName("op")]
    public required GatewayOpcode Opcode { get; init; }

    /// <summary>
    /// Event data.
    /// </summary>
    [JsonPropertyName("d")]
    public required JsonNode? Data { get; init; }

    /// <summary>
    /// Sequence number of event used for
    /// <a href="https://discord.com/developers/docs/topics/gateway#resuming">resuming sessions</a> and
    /// <a href="https://discord.com/developers/docs/topics/gateway#sending-heartbeats">heartbeating</a>.
    /// Null if <see cref="Opcode">Opcode</see> is not 0.
    ///
    /// Required by Discord documentation, but client doesn't always include it.
    /// </summary>
    [JsonPropertyName("s")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SequenceNumber { get; init; }

    /// <summary>
    /// Event name. Null if <see cref="Opcode" /> is not 0.
    /// 
    /// Required by Discord documentation, but client doesn't always include it.
    /// </summary>
    [JsonPropertyName("t")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EventName { get; init; }
}

public enum GatewayOpcode
{
	/// <summary>
	/// An event was dispatched. Receive only.
	/// </summary>
	DISPATCH = 0,
	
	/// <summary>
	/// Fired periodically by the client to keep the connection alive. Send and receive.
	/// </summary>
	HEARTBEAT = 1,
	
	/// <summary>
	/// Starts a new session during the initial handshake. Send only.
	/// </summary>
	IDENTIFY = 2,
	
	/// <summary>
	/// Update the client's presence. Send only.
	/// </summary>
	PRESENCE_UPDATE = 3,
	
	/// <summary>
	/// Used to join/leave or move between voice channels. Send only.
	/// </summary>
	VOICE_STATE_UPDATE = 4,
	
	/// <summary>
	/// Resume a previous session that was disconnected. Send only.
	/// </summary>
	RESUME = 6,
	
	/// <summary>
	/// You should attempt to reconnect and resume immediately. Receive only.
	/// </summary>
	RECONNECT = 7,
	
	/// <summary>
	/// Request information about offline guild members in a large guild. Send only.
	/// </summary>
	REQUEST_GUILD_MEMBERS = 8,
	
	/// <summary>
	/// The session has been invalidated. You should reconnect and identify/resume accordingly. Receive only.
	/// </summary>
	INVALID_SESSION = 9,
	
	/// <summary>
	/// Sent immediately after connecting, contains the heartbeat_interval to use. Receive only.
	/// </summary>
	HELLO = 10,
	
	/// <summary>
	/// Sent in response to receiving a heartbeat to acknowledge that it has been received. Receive only.
	/// </summary>
	HEARTBEAT_ACK = 11,
}