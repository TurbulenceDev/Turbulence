using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Turbulence.Discord.Services;

namespace Turbulence.Core.ViewModels;

public partial class LogViewModel : ViewModelBase
{
    private readonly ILogger _logger = Ioc.Default.GetService<ILogger>()!;
    public ObservableList<LogEntry> Logs { get; } = new();

    public LogViewModel()
    {
        Refresh();
    }

    [RelayCommand]
    public virtual void Refresh()
    {
        Logs.ReplaceAll(_logger.GetLogs());
    }
}
