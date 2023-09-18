using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel : ViewModelBase, IRecipient<SetServersMessage>
{
    public List<Channel> PrivateChannels = new();
    public List<User> Users = new();
    public List<Guild> Guilds = new();

    public event EventHandler? TreeUpdated;

    [RelayCommand]
    private void SelectionChanged((Snowflake Id, string Name) channel)
    {
        // if (data is ServerNode)
        //     return;
        //
        // if (data is not ChannelNode node)
        //     throw new Exception("This shouldn't happen");
        //
        // if (node.Type is not (GUILD_TEXT or DM or GROUP_DM))
        //     return;

        Messenger.Send(new SetCurrentChannel(channel.Id));
        Messenger.Send(new ShowChannelMessage(channel.Id, channel.Name));
    }
    public void Receive(SetServersMessage m)
    {
        PrivateChannels = m.PrivateChannels;
        Users = m.Users;
        Guilds = m.Guilds;
        
        TreeUpdated?.Invoke(null, EventArgs.Empty);
    }
}

public record SetServersMessage(List<Channel> PrivateChannels, List<User> Users, List<Guild> Guilds);
