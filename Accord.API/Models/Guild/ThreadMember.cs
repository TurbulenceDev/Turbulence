using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class ThreadMember
{
    /// <summary>
    /// The id of the thread
    /// </summary>
    [JsonProperty("id", Required = Required.DisallowNull)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The id of the user
    /// </summary>
    [JsonProperty("user_id", Required = Required.DisallowNull)]
    public ulong UserId { get; internal set; }

    /// <summary>
    /// The time the current user last joined the thread. ISO8601 timestamp
    /// </summary>
    [JsonProperty("join_timestamp", Required = Required.Always)]
    public string JoinTimestamp { get; internal set; } = null!;

    /// <summary>
    /// Any user-thread settings, currently only used for notifications
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; internal set; }
}