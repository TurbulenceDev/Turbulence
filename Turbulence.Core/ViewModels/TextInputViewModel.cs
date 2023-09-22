using CommunityToolkit.Mvvm.Input;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class TextInputViewModel : ViewModelBase
{
    [RelayCommand]
    private static async Task SendMessage(ChatMessage chatMessage)
    {
        await Api.CreateAndSendMessage(Client.HttpClient, chatMessage.Channel, chatMessage.Content);
    }
}

public record ChatMessage(string Content, Channel Channel);
