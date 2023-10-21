using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Turbulence.Core.ViewModels;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Views.Main
{
    public partial class MessageContextMenu : MenuFlyout
    {
        private MessageContextMenuViewModel viewModel = new();
        public MessageContextMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void Copy(object? sender, RoutedEventArgs? args)
        {
            if (sender is not Control control)
                return;

            if (control.DataContext is not Message message)
                return;

            if (string.IsNullOrEmpty(message.Content))
                return;

            var toplevel = TopLevel.GetTopLevel(control);
            if (toplevel == null || toplevel.Clipboard is not IClipboard clipboard)
                return;

            clipboard.SetTextAsync(message.Content);
        }

        public void Reply(object? sender, RoutedEventArgs? args)
        {
            if (sender is not Control control)
                return;

            if (control.DataContext is not Message message)
                return;

            viewModel.ReplyCommand.Execute(message);
        }

        public void Edit(object? sender, RoutedEventArgs? args)
        {
            //TODO: permission checks
            if (sender is not Control control)
                return;

            if (control.DataContext is not Message message)
                return;

            viewModel.EditCommand.Execute(message);
        }
        
        public void Delete(object? sender, RoutedEventArgs? args)
        {
            //TODO: permission checks
            if (sender is not Control control)
                return;

            if (control.DataContext is not Message message)
                return;

            viewModel.DeleteCommand.Execute(message);
        }
    }
}
