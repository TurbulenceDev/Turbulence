using Avalonia.Controls;
using Turbulence.Core.ViewModels.Design;

namespace Turbulence.Desktop.Views.Main
{
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            if (Design.IsDesignMode)
            {
                DataContext = new DesignSearchViewModel();
            }
        }
    }
}
