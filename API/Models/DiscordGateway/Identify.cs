using System.Runtime.Serialization;

namespace Turbulence.API.Models.DiscordGateway;

/// <summary>
/// Taken from https://discord.com/developers/docs/topics/gateway#identify-identify-structure
/// </summary>
public class Identify
{
    /// <summary>
    /// Authentication token
    /// </summary>
    [DataMember(Name = "token", IsRequired = true)]
    public string Token { get; set; }

    /// <summary>
    /// Connection properties
    /// </summary>
    [DataMember(Name = "properties", IsRequired = true)]
    public object Properties { get; set; }

    /// <summary>
    /// Whether this connection supports compression of packets
    /// </summary>
    [DataMember(Name = "compress", IsRequired = true)]
    public bool Compress { get; set; } = false;

    /// <summary>
    /// Value between 50 and 250, total number of members where the gateway will stop sending offline members in the guild member list
    /// </summary>
    [DataMember(Name = "large_threshold", IsRequired = true)]
    public int LargeThreshold { get; set; } = 50;

    /// <summary>
    /// Used for guild sharding
    /// </summary>
    [DataMember(Name = "shard", IsRequired = true)]
    public /*two integers (shard_id, num_shards)[]*/ object Shard { get; set; }

    /// <summary>
    /// Presence structure for initial presence information
    /// </summary>
    [DataMember(Name = "presence", IsRequired = true)]
    public GatewayPresenceUpdate Presence { get; set; }

    /// <summary>
    /// The gateway intents you wish to receive
    /// </summary>
    [DataMember(Name = "intents", IsRequired = true)]
    public int Intents { get; set; }
}