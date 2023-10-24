using Avalonia.Controls;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main;

public partial class TextInputView : UserControl
{
    public TextInputView()
    {
        InitializeComponent();
        if (Design.IsDesignMode)
        {
            // Workaround to fix design data context getting overwritten
            DataContext = new DesignTextInputViewModel();
        }
    }
}
