using System.Text.Json.Serialization;
using Turbulence.API.Discord.JsonConverters;

namespace Turbulence.API.Discord.Models.DiscordChannel;

/// <summary>
/// See the <a href="https://discord.com/developers/docs/resources/channel#role-subscription-data-object">Discord API
/// documentation</a> or
/// <a href="https://github.com/discord/discord-api-docs/blob/main/docs/resources/Channel.md#role-subscription-data-object">
/// GitHub</a>.
/// </summary>
public record RoleSubscriptionData {
	/// <summary>
	/// The snowflake ID of the SKU and listing that the user is subscribed to.
	/// </summary>
	[JsonPropertyName("role_subscription_listing_id")]
	public required Snowflake RoleSubscriptionListingId { get; init; }

	/// <summary>
	/// The name of the tier that the user is subscribed to.
	/// </summary>
	[JsonPropertyName("tier_name")]
	public required string TierName { get; init; }

	/// <summary>
	/// The cumulative number of months that the user has been subscribed for.
	/// </summary>
	[JsonPropertyName("total_months_subscribed")]
	public required int TotalMonthsSubscribed { get; init; }

	/// <summary>
	/// Whether this notification is for a renewal rather than a new purchase.
	/// </summary>
	[JsonPropertyName("is_renewal")]
	public required bool IsRenewal { get; init; }
}
