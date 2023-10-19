using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class MessageContextMenuViewModel : ViewModelBase
{
    [RelayCommand]
    public void Reply(Message message)
    {
        Messenger.Send(new ReplyToMessage(message));
    }
}

public record ReplyToMessage(Message message);