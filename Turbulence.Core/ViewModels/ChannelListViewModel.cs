using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class ChannelListViewModel : ViewModelBase, IRecipient<SetChannelsMsg>, IRecipient<ServerSelectedMsg>
{
    public ObservableList<Channel> Channels { get; } = new();
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    [ObservableProperty]
    private Channel? _selectedChannel;

    public ChannelListViewModel()
    {
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedChannel) && SelectedChannel is { } channel)
                Messenger.Send(new ChannelSelectedMsg(channel));
        };
    }
    
    public void Receive(SetChannelsMsg m) => Channels.ReplaceAll(m.Channels);
    
    public async void Receive(ServerSelectedMsg m)
    {
        Channels.Clear();
        
        // TODO: Move this to its own method
        if (m.Server.OwnerId == _client.CurrentUser?.Id)
        {
            Channels.ReplaceAll(m.Server.Channels);
        }
        else
        {
            var everyoneRole = m.Server.Roles.First(r => r.Id == m.Server.Id);
            // TODO: Get rid of API call
            var myGuildMember = await Api.GetCurrentUserGuildMember(_client.HttpClient, m.Server.Id);
            var myRoles = m.Server.Roles.Where(r => myGuildMember.Roles.Contains(r.Id)).ToList();

            var perms = BigInteger.Parse(everyoneRole.Permissions);

            foreach (var role in myRoles)
            {
                perms |= BigInteger.Parse(role.Permissions);
            }

            foreach (var channel in m.Server.Channels.Order())
            {
                var channelPerms = perms;
                if (channel.PermissionOverwrites is { } overwrites)
                {
                    List<Overwrite> toApply = new();
                    if (overwrites.FirstOrDefault(o => o.Id == m.Server.Id) is { } everyoneOverwrite)
                        toApply.Add(everyoneOverwrite);

                    // Add overwrites for my roles
                    toApply.AddRange(overwrites.Where(o => o.Type == 0 && myRoles.Exists(r => r.Id == o.Id))); 
                    // Add overwrites for me specifically
                    toApply.AddRange(overwrites.Where(o => o.Type == 1 && o.Id == _client.CurrentUser?.Id));

                    // Then apply
                    foreach (var overwrite in toApply)
                    {
                        channelPerms |= BigInteger.Parse(overwrite.Allow);
                        channelPerms &= ~BigInteger.Parse(overwrite.Deny);
                    }
                }

                // Add to channel list if we have permission to view it
                // TODO: Use enum flag here
                if ((channelPerms & (1 << 10)) != 0)
                    Channels.Add(channel);
            }
        }
    }
}

public record SetChannelsMsg(List<Channel> Channels);
public record ChannelSelectedMsg(Channel Channel);