using Avalonia.Controls;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Settings;

public partial class GeneralSettingsView : UserControl
{
    public GeneralSettingsView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            DataContext = new DesignSettingsViewModel();
        }
    }
}
