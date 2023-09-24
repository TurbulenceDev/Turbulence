using Avalonia.Controls;
using Avalonia.Interactivity;
using JetBrains.Annotations;
using Turbulence.Core.ViewModels;

namespace Turbulence.Desktop.Views;

public partial class TextInputView : UserControl
{
    public TextInputView()
    {
        InitializeComponent();
    }

    [UsedImplicitly]
    private void OnSend(object? _1, RoutedEventArgs _2)
    {
        if (string.IsNullOrWhiteSpace(TextInput.Text))
            return;

        ((TextInputViewModel)DataContext!).SendMessageCommand.Execute(TextInput.Text);
        TextInput.Text = "";
    }
}
