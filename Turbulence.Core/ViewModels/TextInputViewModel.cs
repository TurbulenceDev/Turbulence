using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _typingStatus = "";

    [ObservableProperty]
    private string? _input = "";

    [RelayCommand]
    private void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(Input))
            return;

        Messenger.Send(new SendMessageMsg(Input));
        Input = "";
    }
}
