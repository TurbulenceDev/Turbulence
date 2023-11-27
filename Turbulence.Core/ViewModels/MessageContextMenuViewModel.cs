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

/// <summary>
/// Notifies that the user wants to reply to the message <paramref name="Message"/>
/// </summary>
/// <param name="Message"></param>
public record ReplyToMessage(Message Message);
/// <summary>
/// Notifies that the user wants to edit the message <paramref name="Message"/>
/// </summary>
/// <param name="Message">The message to be edited</param>
public record EditMessage(Message Message);