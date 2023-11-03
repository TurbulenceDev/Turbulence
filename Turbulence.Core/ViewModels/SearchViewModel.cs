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
    private const int ResultsPerPage = 25;
    [ObservableProperty]
    private int _totalSearchResult = 0;
    // The cached request
    private SearchRequest? _request;

    [ObservableProperty]
    private int _maximumPage = 1;
    [ObservableProperty]
    private bool _isPaginationVisible = false;

    // The current search page, note that this isn't an index
    [ObservableProperty]
    private int _currentPage = 1;

    public ObservableList<Message> SearchResults { get; } = new();
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public void OnPageChanged(int page)
    {
        if (_request == null)
            return;

        _request.Offset = (page - 1) * ResultsPerPage;
        Task.Run(Search);
    }
    private void UpdateFields()
    {
        //TODO: can we do this better than manually calling this?
        MaximumPage = (int)Math.Ceiling((decimal)TotalSearchResult / ResultsPerPage);
        IsPaginationVisible = TotalSearchResult > ResultsPerPage;
    }

    public async void Search()
    {
        if (_request == null)
            return;

        var result = await _client.SearchMessages(_request);
        TotalSearchResult = result.TotalResults;
        // update fields
        UpdateFields();
        //FIXME: fix this somehow not using linq?
        SearchResults.ReplaceAll(result.Messages.Select(m => m[0]));
    }

    public async void Receive(SearchMsg message)
    {
        // calculate the current page
        CurrentPage = (message.Request.Offset / ResultsPerPage) + 1;
        _request = message.Request;
        _ = Task.Run(Search);
    }

    [RelayCommand]
    public void CloseSearch(SearchMsg message) 
    {
        Messenger.Send(new SearchClosedMsg());
    }
}

public record SearchClosedMsg;
