using Avalonia;
using Avalonia.Controls;

namespace Turbulence.Desktop.Views.Main;

public partial class MessageView : UserControl
{
    // The ShowJumpToMessage Property shows/hides the "Jump To Message" button. The value can be read in xaml through {TemplateBinding ShowJumpToMessage}
    public static readonly StyledProperty<bool> ShowJumpToMessageProperty =
        AvaloniaProperty.Register<MessageView, bool>(nameof(ShowJumpToMessage), defaultValue: false);

    public bool ShowJumpToMessage
    {
        get { return GetValue(ShowJumpToMessageProperty); }
        set { SetValue(ShowJumpToMessageProperty, value); }
    }

    public MessageView()
    {
        InitializeComponent();
    }
}
