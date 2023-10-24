using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class MenuBarViewModel : ViewModelBase, IRecipient<SetStatusMsg>
{
    [ObservableProperty]
    private string? _status = "Status";
    
    public void Receive(SetStatusMsg message)
    {
        Status = message.Status;
    }

    [RelayCommand]
    public void Connect()
    {
        Messenger.Send(new ConnectMsg());
    }
}

public record SetStatusMsg(string Status);
public record ConnectMsg();