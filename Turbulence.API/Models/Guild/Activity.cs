using Newtonsoft.Json;

namespace Turbulence.API.Models.Guild;

// TODO: Add "taken from [url]" comments to every class in Guild
public class Activity
{
    /// <summary>
    /// The activity's name
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Activity type
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public ActivityType Type { get; set; }

    /// <summary>
    /// Stream url, is validated when type is 1
    /// </summary>
    [JsonProperty("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Unix timestamp (in milliseconds) of when the activity was added to the user's session
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public int CreatedAt { get; set; }

    /// <summary>
    /// Unix timestamps for start and/or end of the game
    /// </summary>
    [JsonProperty("timestamps", Required = Required.DisallowNull)]
    public TimestampsObject Timestamps { get; set; } = null!;

    /// <summary>
    /// Application id for the game
    /// </summary>
    [JsonProperty("application_id", Required = Required.DisallowNull)]
    public ulong ApplicationId { get; set; }

    /// <summary>
    /// What the player is currently doing
    /// </summary>
    [JsonProperty("details")]
    public string? Details { get; set; }

    /// <summary>
    /// The user's current party status
    /// </summary>
    [JsonProperty("state")]
    public string? State { get; set; }

    /// <summary>
    /// The emoji used for a custom status
    /// </summary>
    [JsonProperty("emoji")]
    public Emoji? Emoji { get; set; }

    /// <summary>
    /// Information for the current party of the player
    /// </summary>
    [JsonProperty("party", Required = Required.DisallowNull)]
    public /*party*/ object Party { get; set; }

    /// <summary>
    /// Images for the presence and their hover texts
    /// </summary>
    [JsonProperty("assets", Required = Required.DisallowNull)]
    public /*assets*/ object Assets { get; set; }

    /// <summary>
    /// Secrets for rich presence joining and spectating
    /// </summary>
    [JsonProperty("secrets", Required = Required.DisallowNull)]
    public /*secrets*/ object Secrets { get; set; }

    /// <summary>
    /// Whether or not the activity is an instanced game session
    /// </summary>
    [JsonProperty("instance", Required = Required.DisallowNull)]
    public bool Instance { get; set; }

    /// <summary>
    /// Activity flags ord together, describes what the payload includes
    /// </summary>
    [JsonProperty("flags", Required = Required.DisallowNull)]
    public int Flags { get; set; }

    /// <summary>
    /// The custom buttons shown in the rich presence (max 2)
    /// </summary>
    [JsonProperty("buttons", Required = Required.DisallowNull)]
    public /*buttons*/ object[] Buttons { get; set; }

    public class TimestampsObject
    {
        /// <summary>
        /// Unix time (in milliseconds) of when the activity started
        /// </summary>
        [JsonProperty("start", Required = Required.DisallowNull)]
        public string Start { get; set; }

        /// <summary>
        /// Unix time (in milliseconds) of when the activity ends
        /// </summary>
        [JsonProperty("flags", Required = Required.DisallowNull)]
        public string End { get; set; }
    }

    public enum ActivityType
    {
        GAME = 0,
        STREAMING = 1,
        LISTENING = 2,
        WATCHING = 3,
        CUSTOM = 4,
        COMPETING = 5
    }
}