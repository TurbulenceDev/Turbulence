using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/reference#error-messages">Discord API documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/Reference.md#error-messages">GitHub</a>.
/// </summary>
public record Error {
    /// <summary>
    /// The <a href="https://discord.com/developers/docs/topics/opcodes-and-status-codes#json">JSON error code</a>.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }

    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    /// <summary>
    /// The errors. "We will be frequently adding new error messages, so a complete list of errors is not feasible and
    /// would be almost instantly out of date." Thanks, Discord. Probably best to just JSON pretty print this if it
    /// exists.
    /// </summary>
    [JsonPropertyName("errors")]
    public dynamic? Errors { get; init; }
}
