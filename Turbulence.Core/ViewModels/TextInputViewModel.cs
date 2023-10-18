using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase, IRecipient<ReplyToMessage>
{
    [ObservableProperty]
    private string? _typingStatus = "";

    [ObservableProperty]
    private string? _input = "";

    public void Receive(ReplyToMessage message)
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(Input))
            return;

        Messenger.Send(new SendMessageMsg(Input));
        Input = "";
    }
}
