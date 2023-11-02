using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Turbulence.Discord;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Core.ViewModels;

public partial class SearchViewModel : ViewModelBase, IRecipient<SearchMsg>
{
    [ObservableProperty]
    private int _totalSearchResult = 0;
    private SearchRequest? _request;
    public ObservableList<Message> SearchResults { get; } = new();
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public async void Receive(SearchMsg message)
    {
        _request = message.Request;
        var result = await _client.SearchMessages(_request);
        TotalSearchResult = result.TotalResults;
        //FIXME: fix this somehow not using linq?
        SearchResults.ReplaceAll(result.Messages.Select(m => m[0]));
    }

    [RelayCommand]
    public void CloseSearch(SearchMsg message) 
    {
        Messenger.Send(new SearchClosedMsg());
    }
}

public record SearchClosedMsg;
