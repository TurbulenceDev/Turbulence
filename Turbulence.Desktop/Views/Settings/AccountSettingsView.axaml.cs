using Avalonia.Controls;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;

namespace Turbulence.Desktop.Views.Settings;

public partial class AccountSettingsView : UserControl
{
    public AccountSettingsView()
    {
        InitializeComponent();
    }

    public void OnSetToken(object? sender, RoutedEventArgs? e)
    {
        ((SettingsViewModel)DataContext!).SetTokenCommand.Execute(Token.Text);
        Token.Text = "";
    }

    public void OnTokenShow(object? sender, RoutedEventArgs? e)
    {
        if (sender is CheckBox check)
        {
            Token.RevealPassword = check.IsChecked ?? false;
        }
    }
}
