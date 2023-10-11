using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Turbulence.Discord;

namespace Turbulence.Core.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    //TODO: save current user?

    [RelayCommand]
    private void SetToken(string token)
    {
        //TODO: run in service?
        //Messenger.Send(new SetTokenMessage(token));
        var config = new ConfigurationManager().AddUserSecrets<Client>().Build();
        config["token"] = token;
        //TODO: actually save it lmao
    }
}
