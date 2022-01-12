using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class GuildMember
{
    /// <summary>
    /// The user this guild member represents
    /// </summary>
    [JsonProperty("user", Required = Required.DisallowNull)]
    public User.User User { get; internal set; } = null!;

    /// <summary>
    /// This user's guild nickname
    /// </summary>
    [JsonProperty("nick")]
    public string? Nick { get; internal set; }

    /// <summary>
    /// The member's guild avatar hash
    /// </summary>
    [JsonProperty("avatar")]
    public string? Avatar { get; internal set; }

    /// <summary>
    /// Array of role object ids
    /// </summary>
    [JsonProperty("roles", Required = Required.Always)]
    public ulong[] Roles { get; internal set; } = null!;

    /// <summary>
    /// When the user joined the guild. ISO8601 timestamp
    /// </summary>
    [JsonProperty("joined_at", Required = Required.Always)]
    public string JoinedAt { get; internal set; } = null!;

    /// <summary>
    /// When the user started boosting the guild. ISO8601 timestamp 
    /// </summary>
    [JsonProperty("premium_since")]
    public string? PremiumSince { get; internal set; }

    /// <summary>
    /// Whether the user is deafened in voice channels
    /// </summary>
    [JsonProperty("deaf", Required = Required.Always)]
    public bool Deaf { get; internal set; }

    /// <summary>
    /// Whether the user is muted in voice channels
    /// </summary>
    [JsonProperty("mute", Required = Required.Always)]
    public bool Mute { get; internal set; }

    /// <summary>
    /// Whether the user has not yet passed the guild's Membership Screening requirements 
    /// </summary>
    [JsonProperty("pending", Required = Required.DisallowNull)]
    public bool Pending { get; internal set; }

    /// <summary>
    /// Total permissions of the member in the channel, including overwrites, returned when in the interaction object
    /// </summary>
    [JsonProperty("permissions", Required = Required.DisallowNull)]
    public string Permissions { get; internal set; } = null!;

    /// <summary>
    /// When the user's timeout will expire and the user will be able to communicate in the guild again, null or a time in the past if the user is not timed out. ISO8601 timestamp  
    /// </summary>
    [JsonProperty("communication_disabled_until")]
    public string? CommunicationDisabledUntil { get; internal set; }


}