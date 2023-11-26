using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGuild;
using Turbulence.Discord.Models.DiscordPermissions;

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

    [RelayCommand]
    public void SelectChannel(Channel channel)
    {
        SelectedChannel = channel;
    }

    public void Receive(SetChannelsMsg m) => Channels.ReplaceAll(m.Channels);
    
    //TODO: move to client
    private bool CanSeeChannel(Guild Server, Channel channel, BigInteger perms, List<Role> myRoles)
    {
        if (Server.OwnerId == _client.CurrentUser?.Id)
        {
            return true;
        }

        var channelPerms = perms;
        if (channel.PermissionOverwrites is { } overwrites)
        {
            List<Overwrite> toApply = new();
            if (overwrites.FirstOrDefault(o => o.Id == Server.Id) is { } everyoneOverwrite)
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
        return (channelPerms & (1 << 10)) != 0;
    }

    public async void Receive(ServerSelectedMsg m)
    {
        Channels.Clear();

        // Calculate permissions //TODO: move into client
        var everyoneRole = m.Server.Roles.First(r => r.Id == m.Server.Id);
        // TODO: Get rid of API call
        var myGuildMember = await Api.GetCurrentUserGuildMember(_client.HttpClient, m.Server.Id);
        var myRoles = m.Server.Roles.Where(r => myGuildMember.Roles.Contains(r.Id)).ToList();
        // Parse base permission for everyone
        var perms = BigInteger.Parse(everyoneRole.Permissions);
        // Add our permission onto it
        foreach (var role in myRoles)
        {
            perms |= BigInteger.Parse(role.Permissions);
        }

        var topLevel = new List<Channel>();
        var subChannels = new Dictionary<Snowflake, List<Channel>>();
        // iterate over list
        foreach (var channel in m.Server.Channels)
        {
            //TODO: permission filtering 
            if (!CanSeeChannel(m.Server, channel, perms, myRoles))
                continue;

            // save top level channels/categories into one list
            if (channel.ParentId == null)
            {
                topLevel.Add(channel);
            }
            else // save every other channel into a dict<parentid, list>
            {
                if (!subChannels.TryGetValue(channel.ParentId, out var list))
                {
                    list = new();
                    subChannels.Add(channel.ParentId, list);
                }
                list.Add(channel);
            }
        }
        // Sort 
        topLevel.Sort();
        foreach (var l in subChannels.Values)
            l.Sort();
        // Then add each top level channel
        foreach (var cat in topLevel)
        {
            Channels.Add(cat);
            // Add child channels if they exist
            if (subChannels.TryGetValue(cat.Id, out var list))
            {
                foreach (var c in list)
                    Channels.Add(c);
            }
        }
    }
}

public record SetChannelsMsg(List<Channel> Channels);
public record ChannelSelectedMsg(Channel Channel);