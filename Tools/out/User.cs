public class UserStructure
{
    /// <summary>
    /// The user's id
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The user's username, not unique across the platform
    /// </summary>
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; internal set; } = null!;

    /// <summary>
    /// The user's 4-digit discord-tag
    /// </summary>
    [JsonProperty("discriminator", Required = Required.Always)]
    public string Discriminator { get; internal set; } = null!;

    /// <summary>
    /// The user's avatar hash
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public string? Avatar { get; internal set; }

    /// <summary>
    /// Whether the user belongs to an oauth2 application
    /// </summary>
    [JsonProperty("bot?", Required = Required.Always)]
    public bool Bot? { get; internal set; }

    /// <summary>
    /// Whether the user is an official discord system user (part of the urgent message system)
    /// </summary>
    [JsonProperty("system?", Required = Required.Always)]
    public bool System? { get; internal set; }

    /// <summary>
    /// Whether the user has two factor enabled on their account
    /// </summary>
    [JsonProperty("mfa_enabled?", Required = Required.Always)]
    public bool MfaEnabled? { get; internal set; }

    /// <summary>
    /// The user's banner hash
    /// </summary>
    [JsonProperty("banner?", Required = Required.AllowNull)]
    public string? Banner? { get; internal set; }

    /// <summary>
    /// The user's banner color encoded as an integer representation of hexadecimal color code
    /// </summary>
    [JsonProperty("accent_color?", Required = Required.AllowNull)]
    public int? AccentColor? { get; internal set; }

    /// <summary>
    /// The user's chosen language option
    /// </summary>
    [JsonProperty("locale?", Required = Required.Always)]
    public string Locale? { get; internal set; } = null!;

    /// <summary>
    /// Whether the email on this account has been verified
    /// </summary>
    [JsonProperty("verified?", Required = Required.Always)]
    public bool Verified? { get; internal set; }

    /// <summary>
    /// The user's email
    /// </summary>
    [JsonProperty("email?", Required = Required.AllowNull)]
    public string? Email? { get; internal set; }

    /// <summary>
    /// The flags on a user's account
    /// </summary>
    [JsonProperty("flags?", Required = Required.Always)]
    public int Flags? { get; internal set; }

    /// <summary>
    /// The type of nitro subscription on a user's account
    /// </summary>
    [JsonProperty("premium_type?", Required = Required.Always)]
    public int PremiumType? { get; internal set; }

    /// <summary>
    /// The public flags on a user's account
    /// </summary>
    [JsonProperty("public_flags?", Required = Required.Always)]
    public int PublicFlags? { get; internal set; }

}

public class ConnectionStructure
{
    /// <summary>
    /// Id of the connection account
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; internal set; } = null!;

    /// <summary>
    /// The username of the connection account
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The service of the connection (twitch, youtube)
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; internal set; } = null!;

    /// <summary>
    /// Whether the connection is revoked
    /// </summary>
    [JsonProperty("revoked?", Required = Required.Always)]
    public bool Revoked? { get; internal set; }

    /// <summary>
    /// An array of partial server integrations
    /// </summary>
    [JsonProperty("integrations?", Required = Required.Always)]
    public array Integrations? { get; internal set; }

    /// <summary>
    /// Whether the connection is verified
    /// </summary>
    [JsonProperty("verified", Required = Required.Always)]
    public bool Verified { get; internal set; }

    /// <summary>
    /// Whether friend sync is enabled for this connection
    /// </summary>
    [JsonProperty("friend_sync", Required = Required.Always)]
    public bool FriendSync { get; internal set; }

    /// <summary>
    /// Whether activities related to this connection will be shown in presence updates
    /// </summary>
    [JsonProperty("show_activity", Required = Required.Always)]
    public bool ShowActivity { get; internal set; }

    /// <summary>
    /// Visibility of this connection
    /// </summary>
    [JsonProperty("visibility", Required = Required.Always)]
    public int Visibility { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// User's username, if changed may cause the user's discriminator to be randomized.
    /// </summary>
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; internal set; } = null!;

    /// <summary>
    /// If passed, modifies the user's avatar
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public image data? Avatar { get; internal set; }

}

public class QueryStringParams
{
    /// <summary>
    /// Get guilds before this guild id
    /// </summary>
    [JsonProperty("before", Required = Required.Always)]
    public ulong Before { get; internal set; }

    /// <summary>
    /// Get guilds after this guild id
    /// </summary>
    [JsonProperty("after", Required = Required.Always)]
    public ulong After { get; internal set; }

    /// <summary>
    /// Max number of guilds to return (1-200)
    /// </summary>
    [JsonProperty("limit", Required = Required.Always)]
    public int Limit { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// The recipient to open a dm channel with
    /// </summary>
    [JsonProperty("recipient_id", Required = Required.Always)]
    public ulong RecipientId { get; internal set; }

}

public class JsonParams
{
    /// <summary>
    /// Access tokens of users that have granted your app the gdm.join scope
    /// </summary>
    [JsonProperty("access_tokens", Required = Required.Always)]
    public strings[] AccessTokens { get; internal set; }

    /// <summary>
    /// A dictionary of user ids to their respective nicknames
    /// </summary>
    [JsonProperty("nicks", Required = Required.Always)]
    public dict Nicks { get; internal set; }

}

