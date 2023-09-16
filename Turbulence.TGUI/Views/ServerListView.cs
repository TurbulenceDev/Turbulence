using Terminal.Gui;
using Terminal.Gui.Trees;
using Turbulence.Core;
using Turbulence.Core.ViewModels;

namespace Turbulence.TGUI.Views;

public sealed class ServerListView : FrameView
{
    private readonly TreeView _serverTree;
    private readonly ServerListViewModel _vm;
    
    public ServerListView(ServerListViewModel vm)
    {
        _vm = vm;
        _serverTree = new TreeView
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            TreeBuilder = null, // TODO: Fix
        };
        
        Title = "Servers";
        X = 0;
        Y = 1;
        Width = 25;
        Height = Dim.Fill();
        Border = new Border { BorderStyle = BorderStyle.Rounded };
        
        Add(_serverTree);

        _serverTree.SelectionChanged += SelectionChanged;
        _vm.TreeUpdated += (_, _) => _serverTree.SetNeedsDisplay();
    }

    private void SelectionChanged(object? _, SelectionChangedEventArgs<ITreeNode> e)
    {
        _vm.SelectionChangedCommand.Execute(e.NewValue.Tag);
    }
}
