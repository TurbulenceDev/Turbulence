using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase, IRecipient<ReplyToMessage>
{
    [ObservableProperty]
    private string? _typingStatus = "";

    [ObservableProperty]
    private string? _input = "";

    [ObservableProperty]
    private Message? _replyingMessage = null;

    [ObservableProperty]
    private bool _replyPing = false;

    public void Receive(ReplyToMessage msg)
    {
        ReplyingMessage = msg.Message;
        ReplyPing = true; // TODO: save and get default ping from settings?
        // TODO: notify messagesview to refresh scroll?
    }

    [RelayCommand]
    public void ReplyCancel()
    {
        // unset command
        ReplyingMessage = null;
        //TODO: send message to other vms
    }

    [RelayCommand]
    private void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(Input))
            return;

        Messenger.Send(new SendMessageMsg(Input, ReplyingMessage, ReplyPing));
        Input = "";
        // unset reply
        ReplyingMessage = null;
    }
}
