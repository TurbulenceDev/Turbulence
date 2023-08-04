using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordGateway;

/// <summary>
///	See the <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Gateway_Events.md#activity-object">GitHub
/// </a>.
/// /// </summary>
public record Activity {
	/// <summary>
	/// Activity's name.
	/// </summary>
	[JsonPropertyName("name")]
	public required string Name { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-types">Activity type
	/// </a>.
	/// </summary>
	[JsonPropertyName("type")]
	public required int Type { get; init; } // TODO: Enum

	// TODO: Cref enum value
	/// <summary>
	/// Stream URL, is validated when type is 1.
	/// </summary>
	[JsonPropertyName("url")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Url { get; init; }

	/// <summary>
	/// Unix timestamp (in milliseconds) of when the activity was added to the user's session.
	/// </summary>
	[JsonPropertyName("created_at")]
	public required long CreatedAt { get; init; }

	/// <summary>
	/// Unix timestamps for start and/or end of the game.
	/// </summary>
	[JsonPropertyName("timestamps")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivityTimestamps? Timestamps { get; init; }

	/// <summary>
	/// Application snowflake ID for the game.
	/// </summary>
	[JsonPropertyName("application_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonConverter(typeof(SnowflakeConverter))]
	public Snowflake? ApplicationId { get; init; }

	/// <summary>
	/// What the player is currently doing.
	/// </summary>
	[JsonPropertyName("details")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Details { get; init; }

	/// <summary>
	/// User's current party status.
	/// </summary>
	[JsonPropertyName("state")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? State { get; init; }

	/// <summary>
	/// Emoji used for a custom status.
	/// </summary>
	[JsonPropertyName("emoji")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivityEmoji? Emoji { get; init; }

	/// <summary>
	/// Information for the current party of the player.
	/// </summary>
	[JsonPropertyName("party")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivityParty? Party { get; init; }

	/// <summary>
	/// Images for the presence and their hover texts.
	/// </summary>
	[JsonPropertyName("assets")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivityAsset? Assets { get; init; }

	/// <summary>
	/// Secrets for Rich Presence joining and spectating.
	/// </summary>
	[JsonPropertyName("secrets")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivitySecrets? Secrets { get; init; }

	/// <summary>
	/// Whether or not the activity is an instanced game session.
	/// </summary>
	[JsonPropertyName("instance")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? Instance { get; init; }

	/// <summary>
	/// <a href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-flags">Activity
	/// flags</a> <c>OR</c>d together, describes what the payload includes.
	/// </summary>
	[JsonPropertyName("flags")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public int? Flags { get; init; } // TODO: Enum

	/// <summary>
	/// Custom buttons shown in the Rich Presence (max 2).
	/// </summary>
	[JsonPropertyName("buttons")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ActivityButton[]? Buttons { get; init; }
}
