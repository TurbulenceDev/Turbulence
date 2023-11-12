using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Turbulence.Discord.Services;

namespace Turbulence.Core.ViewModels;

public partial class LogViewModel : ViewModelBase
{
    private readonly ILogger _logger = Ioc.Default.GetService<ILogger>()!;
    public ObservableList<LogEntry> _logs { get; } = new();
    public IEnumerable<LogEntry>? Logs { get; set; }

    [ObservableProperty]
    public int _selectedLevel = (int)LogLevel.Info;

    private LogType _selectedType = LogType.Any;

    public LogViewModel()
    {
        _logs.CollectionChanged += (_, _) => UpdateFilters();
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(SelectedLevel))
                UpdateFilters();
        };
        Refresh();
    }

    public void SelectType(LogType t)
    {
        _selectedType = t;
        UpdateFilters();
    }

    private void UpdateFilters()
    {
        Logs = _logs.Where(LogFilter);
        OnPropertyChanged(nameof(Logs));
    }

    private bool LogFilter(LogEntry entry)
    {
        return entry.Level >= (LogLevel)SelectedLevel &&
            (_selectedType == LogType.Any || entry.Type == _selectedType);
    }

    [RelayCommand]
    public virtual void Refresh()
    {
        _logs.ReplaceAll(_logger.GetLogs());
    }
}
