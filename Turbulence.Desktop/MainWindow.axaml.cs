using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Turbulence.Desktop;

public partial class MainWindow : Window
{
    private SettingsWindow? _settingsWindow;
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void OnSettings(object? _1, RoutedEventArgs _2)
    {
        // TODO: instead show as a independent window?
        _settingsWindow = new SettingsWindow();
        await _settingsWindow.ShowDialog(this);
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
        this.RaiseEvent(ev);
    }

    public void OnExit(object? _1, RoutedEventArgs _2)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }
}
