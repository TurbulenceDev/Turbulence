using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Turbulence.Core.ViewModels;

public partial class SearchViewModel : ViewModelBase, IRecipient<SearchMsg>
{
    public ObservableList<string> SearchResults { get; } = new();

    public void Receive(SearchMsg message)
    {
        SearchResults.ReplaceAll(new List<string>() { "123" });
    }

    [RelayCommand]
    public void CloseSearch(SearchMsg message) 
    {
        Messenger.Send(new SearchClosedMsg());
    }
}

public record SearchClosedMsg();