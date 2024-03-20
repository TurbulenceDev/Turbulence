using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main;

public partial class TextInputView : UserControl
{
    public TextInputView()
    {
        InitializeComponent();
        TextInput.AddHandler(KeyDownEvent, OnPreviewKeyDown, RoutingStrategies.Tunnel);
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignTextInputViewModel();
        }
    }
    
    private void OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            var command = SendButton.GetValue(Button.CommandProperty);
            if (command != null && command.CanExecute(null))
                command.Execute(null);

            e.Handled = true;
        }
    }
}
