public class GuildScheduledEventStructure
{
    /// <summary>
    /// The id of the scheduled event
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The guild id which the scheduled event belongs to
    /// </summary>
    [JsonProperty("guild_id", Required = Required.Always)]
    public ulong GuildId { get; internal set; }

    /// <summary>
    /// The channel id in which the scheduled event will be hosted, or null if scheduled entity type is external
    /// </summary>
    [JsonProperty("channel_id **", Required = Required.AllowNull)]
    public ulong? ChannelId ** { get; internal set; }

    /// <summary>
    /// The id of the user that created the scheduled event *
    /// </summary>
    [JsonProperty("creator_id *", Required = Required.AllowNull)]
    public ulong? CreatorId * { get; internal set; }

    /// <summary>
    /// The name of the scheduled event (1-100 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The description of the scheduled event (1-1000 characters)
    /// </summary>
    [JsonProperty("description?", Required = Required.Always)]
    public string Description? { get; internal set; } = null!;

    /// <summary>
    /// The time the scheduled event will start
    /// </summary>
    [JsonProperty("scheduled_start_time", Required = Required.Always)]
    public ISO8601 timestamp ScheduledStartTime { get; internal set; }

    /// <summary>
    /// The time the scheduled event will end, required if entity_type is external
    /// </summary>
    [JsonProperty("scheduled_end_time **", Required = Required.AllowNull)]
    public ISO8601 timestamp? ScheduledEndTime ** { get; internal set; }

    /// <summary>
    /// The privacy level of the scheduled event
    /// </summary>
    [JsonProperty("privacy_level", Required = Required.Always)]
    public privacy level PrivacyLevel { get; internal set; }

    /// <summary>
    /// The status of the scheduled event
    /// </summary>
    [JsonProperty("status", Required = Required.Always)]
    public event status Status { get; internal set; }

    /// <summary>
    /// The type of the scheduled event
    /// </summary>
    [JsonProperty("entity_type", Required = Required.Always)]
    public scheduled entity type EntityType { get; internal set; }

    /// <summary>
    /// The id of an entity associated with a guild scheduled event
    /// </summary>
    [JsonProperty("entity_id", Required = Required.AllowNull)]
    public ulong? EntityId { get; internal set; }

    /// <summary>
    /// Additional metadata for the guild scheduled event
    /// </summary>
    [JsonProperty("entity_metadata **", Required = Required.AllowNull)]
    public entity metadata? EntityMetadata ** { get; internal set; }

    /// <summary>
    /// The user that created the scheduled event
    /// </summary>
    [JsonProperty("creator?", Required = Required.Always)]
    public user object Creator? { get; internal set; }

    /// <summary>
    /// The number of users subscribed to the scheduled event
    /// </summary>
    [JsonProperty("user_count?", Required = Required.Always)]
    public int UserCount? { get; internal set; }

}

enum GuildScheduledEventEntityTypes
{
    STAGE_INSTANCE = 1, // 
    VOICE = 2, // 
    EXTERNAL = 3, // 
}

enum GuildScheduledEventStatus
{
    SCHEDULED = 1, // 
    ACTIVE = 2, // 
    COMPLETED * = 3, // 
    CANCELED  * = 4, // 
}

public class GuildScheduledEventEntityMetadata
{
    /// <summary>
    /// Location of the event (1-100 characters)
    /// </summary>
    [JsonProperty("location? *", Required = Required.Always)]
    public string Location? * { get; internal set; } = null!;

}

public class GuildScheduledEventUserStructure
{
    /// <summary>
    /// The scheduled event id which the user subscribed to
    /// </summary>
    [JsonProperty("guild_scheduled_event_id", Required = Required.Always)]
    public ulong GuildScheduledEventId { get; internal set; }

    /// <summary>
    /// User which subscribed to an event
    /// </summary>
    [JsonProperty("user", Required = Required.Always)]
    public user User { get; internal set; }

    /// <summary>
    /// Guild member data for this user for the guild which this event belongs to, if any
    /// </summary>
    [JsonProperty("member?", Required = Required.Always)]
    public guild member Member? { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Include number of users subscribed to each event
    /// </summary>
    [JsonProperty("with_user_count?", Required = Required.Always)]
    public bool WithUserCount? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The channel id of the scheduled event.
    /// </summary>
    [JsonProperty("channel_id? *", Required = Required.Always)]
    public snowflake * ChannelId? * { get; internal set; }

    /// <summary>
    /// The entity metadata of the scheduled event
    /// </summary>
    [JsonProperty("entity_metadata?", Required = Required.Always)]
    public entity metadata EntityMetadata? { get; internal set; }

    /// <summary>
    /// The name of the scheduled event
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The privacy level of the scheduled event
    /// </summary>
    [JsonProperty("privacy_level", Required = Required.Always)]
    public privacy level PrivacyLevel { get; internal set; }

    /// <summary>
    /// The time to schedule the scheduled event
    /// </summary>
    [JsonProperty("scheduled_start_time", Required = Required.Always)]
    public ISO8601 timestamp ScheduledStartTime { get; internal set; }

    /// <summary>
    /// The time when the scheduled event is scheduled to end
    /// </summary>
    [JsonProperty("scheduled_end_time?", Required = Required.Always)]
    public ISO8601 timestamp ScheduledEndTime? { get; internal set; }

    /// <summary>
    /// The description of the scheduled event
    /// </summary>
    [JsonProperty("description?", Required = Required.Always)]
    public string Description? { get; internal set; } = null!;

    /// <summary>
    /// The entity type of the scheduled event
    /// </summary>
    [JsonProperty("entity_type", Required = Required.Always)]
    public entity type EntityType { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Include number of users subscribed to this event
    /// </summary>
    [JsonProperty("with_user_count?", Required = Required.Always)]
    public bool WithUserCount? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The channel id of the scheduled event, set to null if changing entity type to external
    /// </summary>
    [JsonProperty("channel_id? *", Required = Required.AllowNull)]
    public ulong? ChannelId? * { get; internal set; }

    /// <summary>
    /// The entity metadata of the scheduled event
    /// </summary>
    [JsonProperty("entity_metadata?", Required = Required.Always)]
    public entity metadata EntityMetadata? { get; internal set; }

    /// <summary>
    /// The name of the scheduled event
    /// </summary>
    [JsonProperty("name?", Required = Required.Always)]
    public string Name? { get; internal set; } = null!;

    /// <summary>
    /// The privacy level of the scheduled event
    /// </summary>
    [JsonProperty("privacy_level?", Required = Required.Always)]
    public privacy level PrivacyLevel? { get; internal set; }

    /// <summary>
    /// The time to schedule the scheduled event
    /// </summary>
    [JsonProperty("scheduled_start_time?", Required = Required.Always)]
    public ISO8601 timestamp ScheduledStartTime? { get; internal set; }

    /// <summary>
    /// The time when the scheduled event is scheduled to end
    /// </summary>
    [JsonProperty("scheduled_end_time? *", Required = Required.Always)]
    public ISO8601 timestamp ScheduledEndTime? * { get; internal set; }

    /// <summary>
    /// The description of the scheduled event
    /// </summary>
    [JsonProperty("description?", Required = Required.Always)]
    public string Description? { get; internal set; } = null!;

    /// <summary>
    /// The entity type of the scheduled event
    /// </summary>
    [JsonProperty("entity_type? *", Required = Required.Always)]
    public event entity type EntityType? * { get; internal set; }

    /// <summary>
    /// The status of the scheduled event
    /// </summary>
    [JsonProperty("status?", Required = Required.Always)]
    public event status Status? { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Number of users to return (up to maximum 100)
    /// </summary>
    [JsonProperty("limit?", Required = Required.Always)]
    public number Limit? { get; internal set; }

    /// <summary>
    /// Include guild member data if it exists
    /// </summary>
    [JsonProperty("with_member?", Required = Required.Always)]
    public bool WithMember? { get; internal set; }

    /// <summary>
    /// Consider only users before given user id
    /// </summary>
    [JsonProperty("before? *", Required = Required.Always)]
    public ulong Before? * { get; internal set; }

    /// <summary>
    /// Consider only users after given user id
    /// </summary>
    [JsonProperty("after? *", Required = Required.Always)]
    public ulong After? * { get; internal set; }

}

