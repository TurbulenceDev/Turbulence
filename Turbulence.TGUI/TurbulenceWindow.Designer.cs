using System;
using Terminal.Gui;
using static Terminal.Gui.BorderStyle;
using static Terminal.Gui.Color;
using static Terminal.Gui.TextAlignment;

namespace Turbulence.TGUI;

public partial class TurbulenceWindow {
    private ColorScheme tgDefault;
    private FrameView serverView;
    private TreeView serverTree;
    private FrameView messageView;
    private ListView messages;
    private FrameView textInputView;
    private TextField textInput;
    private Button sendButton;
    private MenuBar menuBar;
    private MenuBarItem fileMenu;
    private MenuItem quitMenuItem;
    private MenuBarItem discordMenu;
    private MenuItem setTokenMenuItem;
    private MenuBarItem statusMenu;

    private void InitializeComponent() {
        menuBar = new MenuBar
        {
            Width = Dim.Fill(0),
            Height = 1,
            Data = "menuBar",
        };
        sendButton = new Button
        {
            Width = 9,
            Height = 1,
            X = 81,
            Y = 0,
            Data = "sendButton",
            Text = "Send",
            TextAlignment = Centered,
            IsDefault = true,
        };
        textInput = new TextField
        {
            Width = Dim.Fill(10),
            Height = 1,
            Secret = false,
            Data = "textInput",
            Text = "",
        };
        textInputView = new FrameView
        {
            Width = 93,
            Height = 3,
            X = 25,
            Y = 25,
            Data = "textInputView",
            Border =
            {
                BorderStyle = BorderStyle.Single,
                Effect3D = false,
                Effect3DBrush = null,
                DrawMarginFrame = true,
            },
            Title = "Write Message",
        };
        messages = new ListView
        {
            Width = Dim.Fill(1),
            Height = Dim.Fill(0),
            Data = "messages",
            Source = new ListWrapper(new string[0]),
            AllowsMarking = false,
            AllowsMultipleSelection = false,
        };
        messageView = new FrameView
        {
            Width = 93,
            Height = 24,
            X = 25,
            Y = 1,
            Data = "messageView",
            Border =
            {
                BorderStyle = BorderStyle.Single,
                Effect3D = false,
                Effect3DBrush = null,
                DrawMarginFrame = true,
            },
            Title = "Messages",
        };
        serverTree = new TreeView
        {
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            Data = "serverTree",
            Style =
            { 
                CollapseableSymbol = '-',
                ColorExpandSymbol = false,
                ExpandableSymbol = '+',
                InvertExpandSymbolColors = false,
                LeaveLastRow = false,
                ShowBranchLines = true,
            },
        };
        tgDefault = new ColorScheme
        {
            Normal = new(White, Blue),
            HotNormal = new(BrightCyan, Blue),
            Focus = new(Black, Gray),
            HotFocus = new(BrightBlue, Gray),
            Disabled = new(Black, Blue),
        };

        Width = Dim.Fill(0);
        Height = Dim.Fill(0);
        ColorScheme = tgDefault;
        Modal = false;
        IsMdiContainer = false;
        Border.BorderStyle = BorderStyle.Single;
        Border.BorderBrush = White;
        Border.Background = Blue;
        Border.Effect3D = false;
        Border.Effect3DBrush = null;
        Border.DrawMarginFrame = true;
        Title = "Press Ctrl+Q to quit";
        
        serverView = new FrameView
        {
            Width = 25,
            Height = Dim.Fill(0),
            X = 0,
            Y = 1,
            Data = "serverView",
            Border =
            {
                BorderStyle = BorderStyle.Single,
                Effect3D = false,
                Effect3DBrush = null,
                DrawMarginFrame = true,
            },
            Title = "Servers",
        };
        Add(serverView);
        
        serverView.Add(serverTree);
        Add(messageView);
        messageView.Add(messages);
        Add(textInputView);
        textInputView.Add(textInput);
        textInputView.Add(sendButton);
        
        quitMenuItem = new MenuItem
        {
            Title = "Quit",
            Data = "quitMenuItem",
        };
        
        fileMenu = new MenuBarItem
        {
            Title = "_File",
            Children = new MenuItem[] { quitMenuItem },
        };
        
        setTokenMenuItem = new MenuItem
        {
            Title = "Set Token",
            Data = "setTokenMenuItem",
        };
        
        discordMenu = new MenuBarItem
        {
            Title = "_Discord",
            Children = new MenuItem[] { setTokenMenuItem },
        };
        
        statusMenu = new MenuBarItem
        {
            Title = "Status",
            Children = new MenuItem[0],
        };
        
        menuBar.Menus = new MenuBarItem[]
        {
            fileMenu,
            discordMenu,
            statusMenu,
        };
        
        Add(menuBar);
    }
}