using Newtonsoft.Json;

namespace Accord.API.Models.User;

/// <summary>
/// Taken from https://discord.com/developers/docs/resources/user#user-object-user-structure
/// </summary>
public class User : Snowflake
{
    /// <summary>
    /// The user's username.
    /// </summary>
    [JsonProperty("username", Required = Required.Always)]
    public string Username { get; internal set; } = null!;

    /// <summary>
    /// The user's 4-digit discriminator.
    /// </summary>
    [JsonProperty("discriminator", Required = Required.Always)]
    public string Discriminator { get; internal set; } = null!;

    /// <summary>
    /// The user's avatar hash.
    /// </summary>
    [JsonProperty("avatar", Required = Required.AllowNull)]
    public string? AvatarHash { get; internal set; }
    
    /// <summary>
    /// Whether or not the user is a bot.
    /// </summary>
    [JsonProperty("bot", Required = Required.DisallowNull)]
    public bool IsBot { get; internal set; }
    
    /// <summary>
    /// Whether the user is an official Discord system user.
    /// </summary>
    [JsonProperty("system", Required = Required.DisallowNull)]
    public bool IsSystem { get; internal set; }
    
    /// <summary>
    /// Whether the user has two factor enabled on their account.
    /// </summary>
    [JsonProperty("mfa_enabled", Required = Required.DisallowNull)]
    public bool MfaEnabled { get; internal set; }
        
    /// <summary>
    /// The user's banner hash.
    /// </summary>
    [JsonProperty("banner")]
    public string? BannerHash { get; internal set; }
        
    /// <summary>
    /// The user's banner color encoded as an integer representation of hexadecimal color code.
    /// </summary>
    [JsonProperty("accent_color")]
    public int? AccentColor { get; internal set; }

    /// <summary>
    /// The user's chosen language option.
    /// </summary>
    [JsonProperty("locale", Required = Required.DisallowNull)]
    public string Locale { get; internal set; } = null!;

    /// <summary>
    /// Whether the email on this account has been verified.
    /// </summary>
    [JsonProperty("verified", Required = Required.DisallowNull)]
    public bool Verified { get; internal set; }

    /// <summary>
    /// The user's email.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; internal set; }

    /// <summary>
    /// The flags on a user's account.
    /// </summary>
    [JsonProperty("flags", Required = Required.DisallowNull)]
    public UserFlag Flags { get; internal set; }

    /// <summary>
    /// The type of Nitro subscription on a user's account.
    /// </summary>
    [JsonProperty("premium_type", Required = Required.DisallowNull)]
    public PremiumType PremiumType { get; internal set; }

    /// <summary>
    /// The public flags on a user's account.
    /// </summary>
    [JsonProperty("public_flags", Required = Required.DisallowNull)]
    public UserFlag PublicFlags { get; internal set; }
}