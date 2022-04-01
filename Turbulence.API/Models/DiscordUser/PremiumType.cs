namespace Turbulence.API.Models.DiscordUser;

/// <summary>
/// Taken from https://discord.com/developers/docs/resources/user#user-object-premium-types
/// </summary>
public enum PremiumType : byte
{
    /// <summary>
    /// None
    /// </summary>
    NONE = 0,
    
    /// <summary>
    /// Nitro Classic
    /// </summary>
    CLASSIC = 1,
    
    /// <summary>
    /// Nitro
    /// </summary>
    NITRO = 2,
}