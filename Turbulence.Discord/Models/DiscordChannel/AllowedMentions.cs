using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel">Discord API documentation</a> or <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md">GitHub</a>.
/// </summary>
public record AllowedMentions
{
    /// <summary>
    /// An array of <a href="https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types">allowed mention types</a> to parse from the content.
    /// </summary>
    [JsonPropertyName("parse")]
    public required /* TODO: Make enum */ string[] Parse { get; init; } // Allowed: "users", "roles", "everyone"

    /// <summary>
    /// Array of role_ids to mention (Max size of 100).
    /// </summary>
    [JsonPropertyName("roles")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Snowflake[]? Roles { get; init; }

    /// <summary>
    /// Array of user_ids to mention (Max size of 100).
    /// </summary>
    [JsonPropertyName("users")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Snowflake[]? Users { get; init; }

    /// <summary>
    /// For replies, whether to mention the author of the message being replied to (default false).
    /// </summary>
    [JsonPropertyName("replied_user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RepliedUser { get; init; }
}
