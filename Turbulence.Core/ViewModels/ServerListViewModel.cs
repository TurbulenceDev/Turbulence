using CommunityToolkit.Mvvm.Input;
using static Turbulence.Discord.Models.DiscordChannel.ChannelType;

namespace Turbulence.Core.ViewModels;

public partial class ServerListViewModel
{
    private readonly TurbulenceWindowViewModel _parentVm;
    private readonly MessagesViewModel _messagesVm;

    public ServerListViewModel(TurbulenceWindowViewModel parentVm, MessagesViewModel messagesVm)
    {
        _parentVm = parentVm;
        _messagesVm = messagesVm;
    }
    
    [RelayCommand]
    private async Task SelectionChanged(TreeNodeData data)
    {
        if (data is ServerNode)
            return; // server => do nothing

        if (data is not ChannelNode node)
            throw new Exception("we shouldnt be here"); // this shouldnt happen

        // channel or dm
        if (node.Type is not (GUILD_TEXT or DM or GROUP_DM))
            return;

        _parentVm.CurrentChannel = node.Id;

        await _messagesVm.ShowChannel(node);
    }
}
