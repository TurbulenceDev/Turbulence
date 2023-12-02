using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Reactive;
using CommunityToolkit.Mvvm.DependencyInjection;
using LibVLCSharp.Avalonia.Unofficial;
using LibVLCSharp.Shared;

namespace Turbulence.Desktop.Controls;

public partial class VideoPlayerControl : UserControl
{
    public static readonly DirectProperty<VideoPlayerControl, Uri?> MediaProperty =
        AvaloniaProperty.RegisterDirect<VideoPlayerControl, Uri?>(nameof(Media),
            o => o.Media,
            (o, v) => o.Media = v,
            null!,
            BindingMode.TwoWay,
            false);

    private VideoView? _videoViewer;
    
    private readonly LibVLC _libVLC = Ioc.Default.GetService<LibVLC>()!;
    public MediaPlayer? MediaPlayer;
    private Uri? _media;

    public Uri? Media
    {
        get => _media;
        set
        {
            _media = value;
            if (_media == null)
            {
                MediaPlayer!.Media = null;
            }
            else
            {
                using var media = new Media(_libVLC, _media);
                MediaPlayer!.Media = media;
                media.Dispose();
            }
        }
    }

    public VideoPlayerControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
        {
            MediaPlayer = new MediaPlayer(_libVLC);
        }

        _videoViewer = this.Get<VideoView>("VideoViewer");
        
        _videoViewer.Loaded += VideoViewerLoaded;
        _videoViewer.GetObservable(IsVisibleProperty).Subscribe(new AnonymousObserver<bool>(b =>
            {
                if (b) MediaPlayer?.Play();
                else MediaPlayer?.Pause();
            }
        ));
    }

    private void VideoViewerLoaded(object? sender, RoutedEventArgs e)
    {
        if (_videoViewer != null && MediaPlayer != null)
        {
            _videoViewer.MediaPlayer = MediaPlayer;
            _videoViewer.MediaPlayer.Hwnd = _videoViewer.hndl.Handle;
            MediaPlayer?.Play();
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}