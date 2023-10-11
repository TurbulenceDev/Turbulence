using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;

namespace Turbulence.Desktop;

public partial class MainWindow : Window
{
    SettingsWindow? _settingsWindow;
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void OnSettings(object? _1, RoutedEventArgs _2)
    {
        //TODO: instead show as a independent window?
        _settingsWindow = new();
        await _settingsWindow.ShowDialog(this);
    }

    public void OnExit(object? _1, RoutedEventArgs _2)
    {
        var current = Application.Current;
        if (current == null)
            return;

        var lifetime = current.ApplicationLifetime;
        if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.Shutdown();
    }
}
