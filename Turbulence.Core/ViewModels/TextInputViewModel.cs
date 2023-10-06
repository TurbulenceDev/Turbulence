using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _typingStatus;

    [RelayCommand]
    private void SendMessage(string message)
    {
        Messenger.Send(new SendMessageMsg(message));
    }
}
