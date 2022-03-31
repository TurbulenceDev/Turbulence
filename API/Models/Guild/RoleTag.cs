using Newtonsoft.Json;

namespace Turbulence.API.Models.Guild;

public class RoleTag
{
    /// <summary>
    /// The id of the bot this role belongs to
    /// </summary>
    [JsonProperty("bot_id", Required = Required.DisallowNull)]
    public ulong BotId { get; set; }

    /// <summary>
    /// The id of the integration this role belongs to
    /// </summary>
    [JsonProperty("integration_id", Required = Required.DisallowNull)]
    public ulong IntegrationId { get; set; }

    /// <summary>
    /// Whether this is the guild's premium subscriber role
    /// </summary>
    [JsonProperty("premium_subscriber", Required = Required.DisallowNull)]
    public bool PremiumSubscriber { get; set; }
}