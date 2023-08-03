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
    public required int Opcode { get; init; } // TODO: Enum

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
    /// </summary>
    [JsonPropertyName("s")]
    public required int? SequenceNumber { get; init; } // TODO: Probably isn't actually required? Check what Discord client does

    /// <summary>
    /// Event name. Null if <see cref="Opcode" /> is not 0.
    /// </summary>
    [JsonPropertyName("t")]
    public required string? EventName { get; init; } // TODO: Probably isn't actually required? Check what Discord client does
}