using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;
using Turbulence.Desktop.Dialogs;

namespace Turbulence.Desktop;

public partial class MainWindow : Window
{
    private SettingsWindow? _settingsWindow;
    private LogWindow? _logWindow;
    private MainWindowViewModel _vm;
    public MainWindow()
    {
        InitializeComponent();
        _vm = (MainWindowViewModel)DataContext!;
        _vm.ErrorEvent += OnErrorEvent;
    }

    private async void OnErrorEvent(object? sender, string e)
    {
        OkWindow errorDialog = new()
        {
            Title = "Error",
            Prompt = e
        };
        await errorDialog.ShowDialog(this);
    }

    // Menu Item Handlers //TODO: move them into the menu bar view + use it??
    public void OnConnect(object? _1, RoutedEventArgs? _2)
    {
        _vm.Connect();
    }

    public async void OnSettings(object? _1, RoutedEventArgs _2)
    {
        // TODO: instead show as a independent window?
        _settingsWindow = new SettingsWindow();
        await _settingsWindow.ShowDialog(this);
    }
    
    public async void OnLogs(object? _1, RoutedEventArgs _2)
    {
        // TODO: instead show as a independent window?
        _logWindow = new LogWindow();
        await _logWindow.ShowDialog(this);
    }

    public void OnDevTools(object? _1, RoutedEventArgs _2)
    {
        // Cause a f12 gesture to workaround the fact that we cant call the devtools manually
        var ev = new KeyEventArgs
        {
            RoutedEvent = KeyDownEvent,
            Key = Key.F12,
            Source = this
        };
        RaiseEvent(ev);
    }

    public void OnExit(object? _1, RoutedEventArgs _2)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }

    // Prevent the search pane from closing automatically
    public void OnPaneClosing(object? sender, CancelRoutedEventArgs? args)
    {
        if (_vm == null) // why is this null on start??
            return;

        if (sender is not SplitView)
            return;

        if (_vm.SearchOpen)
            args!.Cancel = true;
    }
}
