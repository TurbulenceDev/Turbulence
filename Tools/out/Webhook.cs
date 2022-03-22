public class WebhookStructure
{
    /// <summary>
    /// The id of the webhook
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The type of the webhook
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// The guild id this webhook is for, if any
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.AllowNull)]
    public ulong? GuildId? { get; internal set; }

    /// <summary>
    /// The channel id this webhook is for, if any
    /// </summary>
    [JsonProperty("channel_id", Required = Required.AllowNull)]
    public ulong? ChannelId { get; internal set; }

    /// <summary>
    /// The user this webhook was created by (not returned when getting a webhook with its token)
    /// </summary>
    [JsonProperty("user?", Required = Required.Always)]
    public user object User? { get; internal set; }

    /// <summary>
    /// The default name of the webhook
    /// </summary>
    [JsonProperty("name", Required = Required.AllowNull)]
    public string? Name { get; internal set; }

    /// <summary>
    /// The default user avatar hash of the webhook
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public string? Avatar { get; internal set; }

    /// <summary>
    /// The secure token of the webhook (returned for incoming webhooks)
    /// </summary>
    [JsonProperty("token?", Required = Required.Always)]
    public string Token? { get; internal set; } = null!;

    /// <summary>
    /// The bot/oauth2 application that created this webhook
    /// </summary>
    [JsonProperty("application_id", Required = Required.AllowNull)]
    public ulong? ApplicationId { get; internal set; }

    /// <summary>
    /// The guild of the channel that this webhook is following (returned for channel follower webhooks)
    /// </summary>
    [JsonProperty("source_guild?", Required = Required.Always)]
    public partial guild object SourceGuild? { get; internal set; }

    /// <summary>
    /// The channel that this webhook is following (returned for channel follower webhooks)
    /// </summary>
    [JsonProperty("source_channel?", Required = Required.Always)]
    public partial channel object SourceChannel? { get; internal set; }

    /// <summary>
    /// The url used for executing the webhook (returned by the webhooks oauth2 flow)
    /// </summary>
    [JsonProperty("url?", Required = Required.Always)]
    public string Url? { get; internal set; } = null!;

}

public class JsonParams
{
    /// <summary>
    /// Name of the webhook (1-80 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Image for the default webhook avatar
    /// </summary>
    [JsonProperty("avatar?", Required = Required.AllowNull)]
    public image data? Avatar? { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The default name of the webhook
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Image for the default webhook avatar
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public image data? Avatar { get; internal set; }

    /// <summary>
    /// The new channel id this webhook should be moved to
    /// </summary>
    [JsonProperty("channel_id", Required = Required.Always)]
    public ulong ChannelId { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Waits for server confirmation of message send before response, and returns the created message body (defaults to false; when false a message that is not saved does not return an error)
    /// </summary>
    [JsonProperty("wait", Required = Required.Always)]
    public bool Wait { get; internal set; }

    /// <summary>
    /// Send a message to the specified thread within a webhook's channel. the thread will automatically be unarchived.
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

}

public class Json/formParams
{
    /// <summary>
    /// The message contents (up to 2000 characters)
    /// </summary>
    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; internal set; } = null!;

    /// <summary>
    /// Override the default username of the webhook
    /// </summary>
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; internal set; } = null!;

    /// <summary>
    /// Override the default avatar of the webhook
    /// </summary>
    [JsonProperty("avatar_url", Required = Required.Always)]
    public string AvatarUrl { get; internal set; } = null!;

    /// <summary>
    /// True if this is a tts message
    /// </summary>
    [JsonProperty("tts", Required = Required.Always)]
    public bool Tts { get; internal set; }

    /// <summary>
    /// Embedded rich content
    /// </summary>
    [JsonProperty("embeds", Required = Required.Always)]
    public up to 10 embed objects[] Embeds { get; internal set; }

    /// <summary>
    /// Allowed mentions for the message
    /// </summary>
    [JsonProperty("allowed_mentions", Required = Required.Always)]
    public allowed mention object AllowedMentions { get; internal set; }

    /// <summary>
    /// The components to include with the message
    /// </summary>
    [JsonProperty("components *", Required = Required.Always)]
    public message component[] Components * { get; internal set; }

    /// <summary>
    /// The contents of the file being sent
    /// </summary>
    [JsonProperty("files[n] **", Required = Required.Always)]
    public file contents Files[n] ** { get; internal set; }

    /// <summary>
    /// Json encoded body of non-file params
    /// </summary>
    [JsonProperty("payload_json **", Required = Required.Always)]
    public string PayloadJson ** { get; internal set; } = null!;

    /// <summary>
    /// Attachment objects with filename and description
    /// </summary>
    [JsonProperty("attachments **", Required = Required.Always)]
    public partial attachment objects[] Attachments ** { get; internal set; }

    /// <summary>
    /// Message flags combined as a bitfield
    /// </summary>
    [JsonProperty("flags", Required = Required.Always)]
    public int Flags { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Id of the thread to send the message in
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

    /// <summary>
    /// Waits for server confirmation of message send before response (defaults to true; when false a message that is not saved does not return an error)
    /// </summary>
    [JsonProperty("wait", Required = Required.Always)]
    public bool Wait { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Id of the thread to send the message in
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

    /// <summary>
    /// Waits for server confirmation of message send before response (defaults to true; when false a message that is not saved does not return an error)
    /// </summary>
    [JsonProperty("wait", Required = Required.Always)]
    public bool Wait { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Id of the thread the message is in
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Id of the thread the message is in
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

}

public class Json/formParams
{
    /// <summary>
    /// The message contents (up to 2000 characters)
    /// </summary>
    [JsonProperty("content", Required = Required.Always)]
    public string Content { get; internal set; } = null!;

    /// <summary>
    /// Embedded rich content
    /// </summary>
    [JsonProperty("embeds", Required = Required.Always)]
    public up to 10 embed objects[] Embeds { get; internal set; }

    /// <summary>
    /// Allowed mentions for the message
    /// </summary>
    [JsonProperty("allowed_mentions", Required = Required.Always)]
    public allowed mention object AllowedMentions { get; internal set; }

    /// <summary>
    /// The components to include with the message
    /// </summary>
    [JsonProperty("components *", Required = Required.Always)]
    public message component[] Components * { get; internal set; }

    /// <summary>
    /// The contents of the file being sent/edited
    /// </summary>
    [JsonProperty("files[n] **", Required = Required.Always)]
    public file contents Files[n] ** { get; internal set; }

    /// <summary>
    /// Json encoded body of non-file params (multipart/form-data only)
    /// </summary>
    [JsonProperty("payload_json **", Required = Required.Always)]
    public string PayloadJson ** { get; internal set; } = null!;

    /// <summary>
    /// Attached files to keep and possible descriptions for new files
    /// </summary>
    [JsonProperty("attachments **", Required = Required.Always)]
    public partial attachment objects[] Attachments ** { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Id of the thread the message is in
    /// </summary>
    [JsonProperty("thread_id", Required = Required.Always)]
    public ulong ThreadId { get; internal set; }

}

