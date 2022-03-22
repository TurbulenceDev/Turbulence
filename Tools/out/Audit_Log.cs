public class AuditLogStructure
{
    /// <summary>
    /// List of audit log entries
    /// </summary>
    [JsonProperty("audit_log_entries", Required = Required.Always)]
    public audit log entry objects[] AuditLogEntries { get; internal set; }

    /// <summary>
    /// List of guild scheduled events found in the audit log
    /// </summary>
    [JsonProperty("guild_scheduled_events", Required = Required.Always)]
    public guild scheduled event objects[] GuildScheduledEvents { get; internal set; }

    /// <summary>
    /// List of partial integration objects
    /// </summary>
    [JsonProperty("integrations", Required = Required.Always)]
    public partial integration objects[] Integrations { get; internal set; }

    /// <summary>
    /// List of threads found in the audit log*
    /// </summary>
    [JsonProperty("threads", Required = Required.Always)]
    public channel objects[] Threads { get; internal set; }

    /// <summary>
    /// List of users found in the audit log
    /// </summary>
    [JsonProperty("users", Required = Required.Always)]
    public user objects[] Users { get; internal set; }

    /// <summary>
    /// List of webhooks found in the audit log
    /// </summary>
    [JsonProperty("webhooks", Required = Required.Always)]
    public webhook objects[] Webhooks { get; internal set; }

}

public class AuditLogEntryStructure
{
    /// <summary>
    /// Id of the affected entity (webhook, user, role, etc.)
    /// </summary>
    [JsonProperty("target_id", Required = Required.AllowNull)]
    public string? TargetId { get; internal set; }

    /// <summary>
    /// Changes made to the target_id
    /// </summary>
    [JsonProperty("changes?", Required = Required.Always)]
    public audit log change objects[] Changes? { get; internal set; }

    /// <summary>
    /// The user who made the changes
    /// </summary>
    [JsonProperty("user_id", Required = Required.AllowNull)]
    public ulong? UserId { get; internal set; }

    /// <summary>
    /// Id of the entry
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Type of action that occurred
    /// </summary>
    [JsonProperty("action_type", Required = Required.Always)]
    public audit log event ActionType { get; internal set; }

    /// <summary>
    /// Additional info for certain action types
    /// </summary>
    [JsonProperty("options?", Required = Required.Always)]
    public optional audit entry info Options? { get; internal set; }

    /// <summary>
    /// The reason for the change (0-512 characters)
    /// </summary>
    [JsonProperty("reason?", Required = Required.Always)]
    public string Reason? { get; internal set; } = null!;

}

public class OptionalAuditEntryInfo
{
    /// <summary>
    /// Channel in which the entities were targeted
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

    /// <summary>
    /// Number of entities that were targeted
    /// </summary>
    [JsonProperty("count", Required = Required.Always)]
    public string Count { get; internal set; } = null!;

    /// <summary>
    /// Number of days after which inactive members were kicked
    /// </summary>
    [JsonProperty("delete_member_days", Required = Required.Always)]
    public string DeleteMemberDays { get; internal set; } = null!;

    /// <summary>
    /// Id of the overwritten entity
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Number of members removed by the prune
    /// </summary>
    [JsonProperty("members_removed", Required = Required.Always)]
    public string MembersRemoved { get; internal set; } = null!;

    /// <summary>
    /// Id of the message that was targeted
    /// </summary>
    [JsonProperty("message_id", Required = Required.Always)]
    public ulong MessageId { get; internal set; }

    /// <summary>
    /// Name of the role if type is "0" (not present if type is "1")
    /// </summary>
    [JsonProperty("role_name", Required = Required.Always)]
    public string RoleName { get; internal set; } = null!;

    /// <summary>
    /// Type of overwritten entity - "0" for "role" or "1" for "member"
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; internal set; } = null!;

}

public class 

{
    /// <summary>
    /// New value of the key
    /// </summary>
    [JsonProperty("new_value?", Required = Required.Always)]
    public mixed NewValue? { get; internal set; }

    /// <summary>
    /// Old value of the key
    /// </summary>
    [JsonProperty("old_value?", Required = Required.Always)]
    public mixed OldValue? { get; internal set; }

    /// <summary>
    /// Name of audit log change key
    /// </summary>
    [JsonProperty("key", Required = Required.Always)]
    public string Key { get; internal set; } = null!;

}

public class QueryStringParams
{
    /// <summary>
    /// Filter the log for actions made by a user
    /// </summary>
    [JsonProperty("user_id", Required = Required.Always)]
    public ulong UserId { get; internal set; }

    /// <summary>
    /// The type of audit log event
    /// </summary>
    [JsonProperty("action_type", Required = Required.Always)]
    public int ActionType { get; internal set; }

    /// <summary>
    /// Filter the log before a certain entry id
    /// </summary>
    [JsonProperty("before", Required = Required.Always)]
    public ulong Before { get; internal set; }

    /// <summary>
    /// How many entries are returned (default 50, minimum 1, maximum 100)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

}

