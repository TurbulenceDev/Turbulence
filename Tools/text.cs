public class StickerStructure
{
    /// <summary>
    /// Id of the sticker
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// For standard stickers, id of the pack the sticker is from
    /// </summary>
    [JsonProperty("pack_id?", Required = Required.Always)]
    public ulong PackId? { get; internal set; }

    /// <summary>
    /// Name of the sticker
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Description of the sticker
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Autocomplete/suggestion tags for the sticker (max 200 characters)
    /// </summary>
    [JsonProperty("tags*", Required = Required.Always)]
    public string Tags* { get; internal set; } = null!;

    /// <summary>
    /// Deprecated previously the sticker asset hash, now an empty string
    /// </summary>
    [JsonProperty("asset", Required = Required.Always)]
    public string Asset { get; internal set; } = null!;

    /// <summary>
    /// Type of sticker
    /// </summary>
    [JsonProperty("type", Required = Required.Always)]
    public int Type { get; internal set; }

    /// <summary>
    /// Type of sticker format
    /// </summary>
    [JsonProperty("format_type", Required = Required.Always)]
    public int FormatType { get; internal set; }

    /// <summary>
    /// Whether this guild sticker can be used, may be false due to loss of server boosts
    /// </summary>
    [JsonProperty("available?", Required = Required.Always)]
    public bool Available? { get; internal set; }

    /// <summary>
    /// Id of the guild that owns this sticker
    /// </summary>
    [JsonProperty("guild_id?", Required = Required.Always)]
    public ulong GuildId? { get; internal set; }

    /// <summary>
    /// The user that uploaded the guild sticker
    /// </summary>
    [JsonProperty("user?", Required = Required.Always)]
    public user object User? { get; internal set; }

    /// <summary>
    /// The standard sticker's sort order within its pack
    /// </summary>
    [JsonProperty("sort_value?", Required = Required.Always)]
    public int SortValue? { get; internal set; }

}

enum StickerTypes
{
    STANDARD = 1, // an official sticker in a pack, part of Nitro or in a removed purchasable pack
    GUILD = 2, // a sticker uploaded to a Boosted guild for the guild's members
}

enum StickerFormatTypes
{
    PNG = 1, // 
    APNG = 2, // 
    LOTTIE = 3, // 
}

public class StickerItemStructure
{
    /// <summary>
    /// Id of the sticker
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// Name of the sticker
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Type of sticker format
    /// </summary>
    [JsonProperty("format_type", Required = Required.Always)]
    public int FormatType { get; internal set; }

}

public class StickerPackStructure
{
    /// <summary>
    /// Id of the sticker pack
    /// </summary>
    [JsonProperty("id", Required = Required.Always)]
    public ulong Id { get; internal set; }

    /// <summary>
    /// The stickers in the pack
    /// </summary>
    [JsonProperty("stickers", Required = Required.Always)]
    public array of sticker objects Stickers { get; internal set; }

    /// <summary>
    /// Name of the sticker pack
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Id of the pack's sku
    /// </summary>
    [JsonProperty("sku_id", Required = Required.Always)]
    public ulong SkuId { get; internal set; }

    /// <summary>
    /// Id of a sticker in the pack which is shown as the pack's icon
    /// </summary>
    [JsonProperty("cover_sticker_id?", Required = Required.Always)]
    public ulong CoverStickerId? { get; internal set; }

    /// <summary>
    /// Description of the sticker pack
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// Id of the sticker pack's banner image
    /// </summary>
    [JsonProperty("banner_asset_id?", Required = Required.Always)]
    public ulong BannerAssetId? { get; internal set; }

}

public class FormParams
{
    /// <summary>
    /// Name of the sticker (2-30 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Description of the sticker (empty or 2-100 characters)
    /// </summary>
    [JsonProperty("description", Required = Required.Always)]
    public string Description { get; internal set; } = null!;

    /// <summary>
    /// Autocomplete/suggestion tags for the sticker (max 200 characters)
    /// </summary>
    [JsonProperty("tags", Required = Required.Always)]
    public string Tags { get; internal set; } = null!;

    /// <summary>
    /// The sticker file to upload, must be a png, apng, or lottie json file, max 500 kb
    /// </summary>
    [JsonProperty("file", Required = Required.Always)]
    public file contents File { get; internal set; }

}

public class JSONParams
{
    /// <summary>
    /// Name of the sticker (2-30 characters)
    /// </summary>
    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Description of the sticker (2-100 characters)
    /// </summary>
    [JsonProperty("description", Required = Required.AllowNull)]
    public string? Description { get; internal set; }

    /// <summary>
    /// Autocomplete/suggestion tags for the sticker (max 200 characters)
    /// </summary>
    [JsonProperty("tags", Required = Required.Always)]
    public string Tags { get; internal set; } = null!;

}

