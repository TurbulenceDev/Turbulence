using CommunityToolkit.Mvvm.Input;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase
{
    [RelayCommand]
    private static async Task SendMessage(Message message)
    {
        await new Client().SendMessage(message.Content, message.Channel);
    }
}

public record Message(string Content, Channel Channel);
