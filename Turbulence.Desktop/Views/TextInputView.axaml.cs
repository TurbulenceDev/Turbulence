using Avalonia.Controls;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;

namespace Turbulence.Desktop.Views;

public partial class TextInputView : UserControl
{
    public TextInputView()
    {
        InitializeComponent();
    }

    private void OnSend(object? _, RoutedEventArgs args)
    {
        if (string.IsNullOrWhiteSpace(TextInput.Text))
            return;

        ((TextInputViewModel)DataContext!).SendMessageCommand.Execute(TextInput.Text);
        TextInput.Text = "";
    }
}
