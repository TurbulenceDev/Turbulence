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

    [RelayCommand]
    public void Edit(Message message)
    {
        Messenger.Send(new EditMessage(message));
    }

    [RelayCommand]
    public void Delete(Message message)
    {
        Messenger.Send(new DeleteMessageMsg(message));
    }
}

public record ReplyToMessage(Message Message);
public record EditMessage(Message Message);