using Newtonsoft.Json;

namespace Accord.API.Models.Guild;

public class ThreadMetadata
{
    /// <summary>
    /// Whether the thread is archived
    /// </summary>
    [JsonProperty("archived", Required = Required.Always)]
    public bool Archived { get; internal set; }

    /// <summary>
    /// Duration in minutes to automatically archive the thread after recent activity, can be set to: 60, 1440, 4320, 10080
    /// </summary>
    [JsonProperty("auto_archive_duration", Required = Required.Always)]
    public int AutoArchiveDuration { get; internal set; }

    /// <summary>
    /// Timestamp when the thread's archive status was last changed, used for calculating recent activity. ISO8601 timestamp
    /// </summary>
    [JsonProperty("archive_timestamp", Required = Required.Always)]
    public string ArchiveTimestamp { get; internal set; } = null!;

    /// <summary>
    /// Whether the thread is locked; when a thread is locked, only users with manage_threads can unarchive it
    /// </summary>
    [JsonProperty("locked", Required = Required.Always)]
    public bool Locked { get; internal set; }

    /// <summary>
    /// Whether non-moderators can add other non-moderators to a thread; only available on private threads
    /// </summary>
    [JsonProperty("invitable", Required = Required.DisallowNull)]
    public bool Invitable { get; internal set; }
}