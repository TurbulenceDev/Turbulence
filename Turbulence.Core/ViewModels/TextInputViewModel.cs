using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Services;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase, IRecipient<ReplyToMessage>, IRecipient<ChannelSelectedMsg>, IRecipient<EditMessage>
{
    [ObservableProperty]
    private string? _typingStatus = "";

    [ObservableProperty]
    private string? _input = "";

    [ObservableProperty]
    private Message? _replyingMessage = null;

    [ObservableProperty]
    private bool _replyPing = false;

    [ObservableProperty]
    private Message? _editingMessage = null;

    private readonly ITypingStorage _typing = Ioc.Default.GetService<ITypingStorage>()!;
    private Snowflake? _currentChannel;
    private const int MaxTypingUsers = 3;

    public TextInputViewModel()
    {
        _typing.TypingStatusChanged += _typing_TypingStatusChanged;
    }

    private void _typing_TypingStatusChanged(object? sender, Event<Snowflake> e)
    {
        var channel = e.Data;
        if (_currentChannel != channel)
            return;

        // fetch the users
        var users = _typing.GetTypingUsers(channel);
        if (users == null)
            return;
        var count = users.Count();

        //TODO: get user name from snowflake
        switch (count)
        {
            case 0:
                TypingStatus = "";
                break;
            case 1:
                TypingStatus = $"{users.ElementAt(0)} is typing...";
                break;
            case 2:
                TypingStatus = $"{users.ElementAt(0)} and {users.ElementAt(1)} are typing...";
                break;
            default:
                var str = "";
                for (var i = 0; i < MaxTypingUsers - 1; i++)
                    str += $"{users.ElementAt(i)}, ";
                TypingStatus = $"{str} and {users.ElementAt(MaxTypingUsers - 1)} are typing...";
                break;
        }
    }

    public void Receive(ReplyToMessage msg)
    {
        ReplyingMessage = msg.Message;
        ReplyPing = true; // TODO: get default ping from settings?
        // TODO: notify messagesview to refresh scroll?
        // unset other messages so we only have one clip open
        EditingMessage = null;
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

        if (EditingMessage != null)
            Messenger.Send(new EditMessageMsg(Input, EditingMessage));
        else
            Messenger.Send(new SendMessageMsg(Input, ReplyingMessage, ReplyPing));
        Input = "";
        // unset clip messages
        ReplyingMessage = null;
        EditingMessage = null;
    }

    public void Receive(ChannelSelectedMsg message)
    {
        _currentChannel = message.Channel.Id;
    }

    public void Receive(EditMessage message)
    {
        EditingMessage = message.Message;
        Input = message.Message.Content;
        // unset other messages so we only have one clip open
        ReplyingMessage = null;
    }

    [RelayCommand]
    public void EditCancel()
    {
        // unset message
        EditingMessage = null;
        Input = "";
        //TODO: send message to other vms
    }
}
