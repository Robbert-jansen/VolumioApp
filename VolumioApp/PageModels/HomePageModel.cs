using System.Windows.Input;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using VolumioServiceLibrary.Services;

namespace VolumioApp.PageModels;

public class HomePageModel : BasePageModel
{
    private readonly IVolumioService _volumioService;

    //private readonly VolumioSocketIOService _volumioSocketIOService;

    public ICommand MuteCommand { get; set; }
    public ICommand UnmuteCommand { get; set; }
    public ICommand TogglePlaybackCommand { get; set; }
    public ICommand NextTrackCommand { get; set; }
    public ICommand PreviousTrackCommand { get; set; }
    public ICommand EditPlaybackValuesCommand { get; set; }
    public ICommand ShowQueueCommand { get; set; }
    public ICommand VolumeSliderDragCompletedCommand { get; set; }
    public ICommand SeekSliderDragCompletedCommand { get; set; }

    public ICommand SeekSliderDragStartedCommand { get; set; }

    public ICommand DisableOverlayCommand { get; set; }
    public bool EditPlaybackValues { get; set; }

    public bool IsInterpolating { get; set; } = true;

    public bool ShowQueue { get; set; }

    public bool OverlayActive
    {
        get
        {
            if (ShowQueue || EditPlaybackValues) return true;
            return false;
        }
    }

    public string ToggleButtonString
    {
        get
        {
            if (PlayerState is null) return "Play";

            if(PlayerState.IsPlaying)
            {
                return "Pause";
            }
            return "Play";
        }
    }
    private System.Timers.Timer Timer { get; set; }
    public PlayerState PlayerState { get; set; }

    public Queue Queue { get; set; }

    
    

    // Used because of a crash on Android, if that ever gets fixed you can bind straight to the Albumart in PlayerState.
    public ImageSource ImageSource { get; set; }
    public HomePageModel(IVolumioService volumioService)
    { 
        _volumioService = volumioService;
        _volumioService.StatePushed += _volumioService_StatePushed;
        _volumioService.QueuePushed += _volumioService_QueuePushed;

        Init();
        
        TogglePlaybackCommand = new Command(() =>
        {
            TogglePlayback();
        });

        PreviousTrackCommand = new Command(() =>
        {
            _volumioService.PreviousTrack();
        });

        NextTrackCommand = new Command(() =>
        {
            _volumioService.NextTrack();
        });

        MuteCommand = new Command( () =>
        {
            _volumioService.MuteVolume();
        });

        UnmuteCommand = new Command( () =>
        {
            _volumioService.UnmuteVolume();
        });

        EditPlaybackValuesCommand = new Command(async () =>
        {
            EditPlaybackValues = !EditPlaybackValues;
            //await _volumioService.GetQueue();
        });

        ShowQueueCommand = new Command(async () =>
        {
            ShowQueue = !ShowQueue;
        });

        VolumeSliderDragCompletedCommand = new Command( () =>
        {
            _volumioService.ChangeVolume((int)PlayerState.Volume);
        });

        SeekSliderDragCompletedCommand = new Command( () =>
        {
            _volumioService.ChangeSeek(PlayerState.Seek / 1000);
            IsInterpolating = true;
        });

        SeekSliderDragStartedCommand = new Command(() =>
        {
            IsInterpolating = false;
            //_volumioService.ChangeSeek(PlayerState.Seek / 1000);
        });

        DisableOverlayCommand = new Command(() =>
            {
                EditPlaybackValues = false;
                ShowQueue = false;
            });
    }

    private void _volumioService_QueuePushed(object sender, EventArgs e)
    {
        Queue.QueueItems.Clear();
        Queue.QueueItems = (List<QueueItem>)sender;
           
    }


    private void _volumioService_StatePushed(object sender, EventArgs e)
    {
        PlayerState = (PlayerState)sender;

        ImageSource = null;
        ImageSource = ImageSource.FromUri(new Uri(PlayerState.AlbumArt));
    }

    private async void TogglePlayback()
    {
        //PlayerState.IsPlaying = !PlayerState.IsPlaying;
       await _volumioService.TogglePlayback();
       //await LoadDataAsync();
    }

    private async void Init()
    {
        // Double load on init is intentional
        // Progress bar in .NET MAUI overwrites the binding value on first binding.
        // Loading it twice in sucession fixed it for now.
        await LoadDataAsync();
        await LoadDataAsync();
        await Task.Run(StartTimer);

       
    }

    private async Task StartTimer()
    {      
        Timer = new System.Timers.Timer(1000);
        Timer.AutoReset = true;
        Timer.Start();
        Timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        Console.WriteLine("elapsed");

        IncrementSeek();
    }

    private void IncrementSeek()
    {
        if (PlayerState.IsPlaying && IsInterpolating)
        {
            PlayerState.Seek += 1000;
        }

    }




    private async Task LoadDataAsync()
    {
        System.Diagnostics.Debug.WriteLine("Load data");

       PlayerState playerState = await _volumioService.GetPlayerState();
        PlayerState = playerState;

        Queue queue = await _volumioService.GetQueue();
        Queue = queue;
        // Setting ImageSource to null before setting it to the new one prevents a crash on Android when the image is the same and previously.
        ImageSource = null;
        ImageSource = ImageSource.FromUri(new Uri(PlayerState.AlbumArt));
    }
}
