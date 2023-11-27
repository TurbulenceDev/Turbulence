using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordEmoji;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordPermissions;

namespace Turbulence.Core.ViewModels.Design;

public class DesignServerListViewModel : ServerListViewModel
{
    public DesignServerListViewModel()
    {
        Connected = true;
        Servers.AddRange(new List<Guild>()
        {
            CreateServer(1, "Server 2"),
            CreateServer(0, "Server"),
        });
    }

    private static Guild CreateServer(ulong id, string name)
    {
        return new Guild()
        {
            Id = new(id),
            Name = name,
            Icon = null,
            Splash = null,
            DiscoverySplash = null,
            OwnerId = new(0),
            AfkChannelId = null,
            AfkTimeout = 0,
            VerificationLevel = 0,
            DefaultMessageNotifications = 0,
            ExplicitContentFilter = 0,
            Roles = Array.Empty<Role>(),
            Emojis = Array.Empty<Emoji>(),
            Features = Array.Empty<string>(),
            MfaLevel = 0,
            ApplicationId = null,
            SystemChannelFlags = 0,
            SystemChannelId = null,
            RulesChannelId = null,
            VanityUrlCode = null,
            Description = null,
            Banner = null,
            PremiumTier = 0,
            PreferredLocale = string.Empty,
            PublicUpdatesChannelId = null,
            NsfwLevel = 0,
            PremiumProgressBarEnabled = false,
            SafetyAlertsChannelId = null,
            Channels = Array.Empty<Channel>(),
        };
    }
}
