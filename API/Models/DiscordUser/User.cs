using System.Runtime.Serialization;

namespace Turbulence.API.Models.DiscordUser;

/// <summary>
/// Taken from https://discord.com/developers/docs/resources/user#user-object-user-structure
/// </summary>
public class User : Snowflake
{
    /// <summary>
    /// The user's id.
    /// </summary>
    [DataMember(Name = "id", IsRequired = true)]
    public int Id { get; set; }
    
    /// <summary>
    /// The user's username.
    /// </summary>
    [DataMember(Name = "username", IsRequired = true)]
    public string Username { get; set; }

    /// <summary>
    /// The user's 4-digit discriminator.
    /// </summary>
    [DataMember(Name = "discriminator", IsRequired = true)]
    public string Discriminator { get; set; }

    /// <summary>
    /// The user's avatar hash.
    /// </summary>
    [DataMember(Name = "avatar", IsRequired = true)]
    public string? AvatarHash { get; set; }
    
    /// <summary>
    /// Whether or not the user is a bot.
    /// </summary>
    [DataMember(Name = "bot", IsRequired = true)]
    public bool IsBot { get; set; }
    
    /// <summary>
    /// Whether the user is an official Discord system user.
    /// </summary>
    [DataMember(Name = "system", IsRequired = true)]
    public bool IsSystem { get; set; }
    
    /// <summary>
    /// Whether the user has two factor enabled on their account.
    /// </summary>
    [DataMember(Name = "mfa_enabled", IsRequired = true)]
    public bool MfaEnabled { get; set; }
        
    /// <summary>
    /// The user's banner hash.
    /// </summary>
    [DataMember(Name = "banner")]
    public string? BannerHash { get; set; }
        
    /// <summary>
    /// The user's banner color encoded as an integer representation of hexadecimal color code.
    /// </summary>
    [DataMember(Name = "accent_color")]
    public int? AccentColor { get; set; }

    /// <summary>
    /// The user's chosen language option.
    /// </summary>
    [DataMember(Name = "locale", IsRequired = true)]
    public string Locale { get; set; }

    /// <summary>
    /// Whether the email on this account has been verified.
    /// </summary>
    [DataMember(Name = "verified", IsRequired = true)]
    public bool Verified { get; set; }

    /// <summary>
    /// The user's email.
    /// </summary>
    [DataMember(Name = "email")]
    public string? Email { get; set; }

    /// <summary>
    /// The flags on a user's account.
    /// </summary>
    [DataMember(Name = "flags", IsRequired = true)]
    public UserFlag Flags { get; set; }

    /// <summary>
    /// The type of Nitro subscription on a user's account.
    /// </summary>
    [DataMember(Name = "premium_type", IsRequired = true)]
    public PremiumType PremiumType { get; set; }

    /// <summary>
    /// The public flags on a user's account.
    /// </summary>
    [DataMember(Name = "public_flags", IsRequired = true)]
    public UserFlag PublicFlags { get; set; }
}