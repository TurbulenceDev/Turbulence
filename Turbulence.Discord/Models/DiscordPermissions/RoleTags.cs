using System.Text.Json.Serialization;

namespace Turbulence.Discord.Models.DiscordPermissions;

// TODO: Completely mangled by Discord, need to deserialize it properly to make it actually usable
/// <summary>
/// See the <a href="https://discord.com/developers/docs/topics/permissions#role-object-role-tags-structure">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/topics/Permissions.md#role-tags-structure">
/// GitHub</a>.
/// </summary>
public record RoleTags {
	/// <summary>
	/// The snowflake ID of the bot this role belongs to.
	/// </summary>
	[JsonPropertyName("bot_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? BotId { get; init; }

	/// <summary>
	/// The snowflake ID of the integration this role belongs to.
	/// </summary>
	[JsonPropertyName("integration_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? IntegrationId { get; init; }

	/// <summary>
	/// Whether this is the guild's Booster role.
	/// </summary>
	[JsonPropertyName("premium_subscriber")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? PremiumSubscriber => throw new NotImplementedException();

	/// <summary>
	/// The snowflake ID of this role's subscription sku and listing.
	/// </summary>
	[JsonPropertyName("subscription_listing_id")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Snowflake? SubscriptionListingId { get; init; }

	/// <summary>
	/// Whether this role is available for purchase.
	/// </summary>
	[JsonPropertyName("available_for_purchase")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? AvailableForPurchase => throw new NotImplementedException();

	/// <summary>
	/// Whether this role is a guild's linked role.
	/// </summary>
	[JsonPropertyName("guild_connections")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? GuildConnections => throw new NotImplementedException();
}
