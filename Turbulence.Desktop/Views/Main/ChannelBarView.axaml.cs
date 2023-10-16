using Avalonia.Controls;

namespace Turbulence.Desktop.Views.Main
{
    public partial class ChannelBarView : UserControl
    {
        public ChannelBarView()
        {
            InitializeComponent();
            if (Design.IsDesignMode)
            {
                // Workaround to fix design data context getting overwritten
                DataContext = Design.GetDataContext(this);
            }
        }
    }
}
