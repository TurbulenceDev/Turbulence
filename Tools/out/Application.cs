public class ApplicationStructure
{
    /// <summary>
    /// The id of the app
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The name of the app
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The icon hash of the app
    /// </summary>
    [JsonProperty("icon", Required = Required.AllowNull)]
    public string? Icon { get; internal set; }

    /// <summary>
    /// The description of the app
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// An array of rpc origin urls, if rpc is enabled
    /// </summary>
    [JsonProperty("rpc_origins?", Required = Required.Always)]
    public strings[] RpcOrigins? { get; internal set; }

    /// <summary>
    /// When false only app owner can join the app's bot to guilds
    /// </summary>
    [JsonProperty("bot_public", Required = Required.Always)]
    public bool BotPublic { get; internal set; }

    /// <summary>
    /// When true the app's bot will only join upon completion of the full oauth2 code grant flow
    /// </summary>
    [JsonProperty("bot_require_code_grant", Required = Required.Always)]
    public bool BotRequireCodeGrant { get; internal set; }

    /// <summary>
    /// The url of the app's terms of service
    /// </summary>
    [JsonProperty("terms_of_service_url?", Required = Required.Always)]
    public string TermsOfServiceUrl? { get; internal set; } = null!;

    /// <summary>
    /// The url of the app's privacy policy
    /// </summary>
    [JsonProperty("privacy_policy_url?", Required = Required.Always)]
    public string PrivacyPolicyUrl? { get; internal set; } = null!;

    /// <summary>
    /// Partial user object containing info on the owner of the application
    /// </summary>
    [JsonProperty("owner?", Required = Required.Always)]
    public partial user object Owner? { get; internal set; }

    /// <summary>
    /// If this application is a game sold on discord, this field will be the summary field for the store page of its primary sku
    /// </summary>
    [JsonProperty("summary", Required = Required.Always)]
    public string Summary { get; internal set; } = null!;

    /// <summary>
    /// The hex encoded key for verification in interactions and the gamesdk's getticket
    /// </summary>
    [JsonProperty("verify_key", Required = Required.Always)]
    public string VerifyKey { get; internal set; } = null!;

    /// <summary>
    /// If the application belongs to a team, this will be a list of the members of that team
    /// </summary>
    [JsonProperty("team", Required = Required.AllowNull)]
    public team object? Team { get; internal set; }

    /// <summary>
    /// If this application is a game sold on discord, this field will be the guild to which it has been linked
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// If this application is a game sold on discord, this field will be the id of the "game sku" that is created, if exists
    /// </summary>
    [JsonProperty("primary_sku_id?", Required = Required.Always)]
    public ulong PrimarySkuId? { get; internal set; }

    /// <summary>
    /// If this application is a game sold on discord, this field will be the url slug that links to the store page
    /// </summary>
    [JsonProperty("slug?", Required = Required.Always)]
    public string Slug? { get; internal set; } = null!;

    /// <summary>
    /// The application's default rich presence invite cover image hash
    /// </summary>
    [JsonProperty("cover_image?", Required = Required.Always)]
    public string CoverImage? { get; internal set; } = null!;

    /// <summary>
    /// The application's public flags
    /// </summary>
    [JsonProperty("flags?", Required = Required.Always)]
    public int Flags? { get; internal set; }

}

