using Newtonsoft.Json;
using Turbulence.API.Models.Guild;

namespace Turbulence.API.Models.DiscordGateway;

public class Activity
{
    /// <summary>
    /// The activity's name.
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Activity type.
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; set; }

    /// <summary>
    /// Stream url, is validated when type is 1.
    /// </summary>
    [JsonProperty("url", Required = Required.AllowNull)]
    public string? Url { get; set; }

    /// <summary>
    /// Unix timestamp (in milliseconds) of when the activity was added to the user's session.
    /// </summary>
    [JsonProperty("created_at", Required = Required.Always)]
    public int CreatedAt { get; set; }

    /// <summary>
    /// Unix timestamps for start and/or end of the game.
    /// </summary>
    [JsonProperty("timestamps", Required = Required.Always)]
    public /*Timestamps*/ object Timestamps { get; set; }

    /// <summary>
    /// Application id for the game.
    /// </summary>
    [JsonProperty("application_id", Required = Required.Always)]
    public ulong ApplicationId { get; set; }

    /// <summary>
    /// What the player is currently doing.
    /// </summary>
    [JsonProperty("details", Required = Required.AllowNull)]
    public string? Details { get; set; }

    /// <summary>
    /// The user's current party status.
    /// </summary>
    [JsonProperty("state", Required = Required.AllowNull)]
    public string? State { get; set; }

    /// <summary>
    /// The emoji used for a custom status.
    /// </summary>
    [JsonProperty("emoji", Required = Required.AllowNull)]
    public Emoji? Emoji { get; set; }

    /// <summary>
    /// Information for the current party of the player.
    /// </summary>
    [JsonProperty("party", Required = Required.Always)]
    public /*Party*/ object Party { get; set; }

    /// <summary>
    /// Images for the presence and their hover texts.
    /// </summary>
    [JsonProperty("assets", Required = Required.Always)]
    public /*Assets*/ object Assets { get; set; }

    /// <summary>
    /// Secrets for rich presence joining and spectating.
    /// </summary>
    [JsonProperty("secrets", Required = Required.Always)]
    public /*Secrets*/ object Secrets { get; set; }

    /// <summary>
    /// Whether or not the activity is an instanced game session.
    /// </summary>
    [JsonProperty("instance", Required = Required.Always)]
    public bool Instance { get; set; }

    /// <summary>
    /// Activity flags ord together, describes what the payload includes.
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; set; }

    /// <summary>
    /// The custom buttons shown in the rich presence (max 2).
    /// </summary>
    [JsonProperty("buttons", Required = Required.Always)]
    public /*Button[]*/ object[] Buttons { get; set; }
}