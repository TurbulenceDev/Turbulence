using Avalonia.Controls;
using Avalonia.Interactivity;
using Turbulence.Core.ViewModels;
using Turbulence.Core.ViewModels.Design;
using Turbulence.Discord.Services;

namespace Turbulence.Desktop;

public partial class LogWindow : Window
{
    private LogViewModel _vm;
    public LogWindow()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignLogViewModel();
        }
        _vm = (LogViewModel)DataContext!;
    }

    public void RadioChecked(object sender, RoutedEventArgs args)
    {
        if (_vm == null) //FIXME: why is the vm null :weary:
            return;

        try
        {
            var button = (RadioButton)args.Source!;
            var val = button.Content as string;
            var type = Enum.Parse<LogType>(val!);
            _vm.SelectType(type);
        }
        catch (Exception e)
        {
            throw new Exception($"Sender is invalid or has invalid content: {e}");
        }
    }
}
