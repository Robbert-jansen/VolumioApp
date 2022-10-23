
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using VolumioServiceLibrary.Services;

namespace VolumioApp.PageModels;

public class HomePageModel : BasePageModel
{
    private readonly IVolumioRestService _volumioRestService;
    private readonly IVolumioService _volumioService;

    //private readonly VolumioSocketIOService _volumioSocketIOService;

    public ICommand MuteCommand { get; set; }
    public ICommand UnmuteCommand { get; set; }
    public ICommand ReloadCommand { get; set; }
    public ICommand TogglePlaybackCommand { get; set; }
    public ICommand NextTrackCommand { get; set; }
    public ICommand PreviousTrackCommand { get; set; }
    public ICommand EditPlaybackValuesCommand { get; set; }
    public ICommand VolumeSliderDragCompletedCommand { get; set; }
    public ICommand SeekSliderDragCompletedCommand { get; set; }

    public ImageSource PlayPauseImage { get; set; }

    public bool EditPlaybackValues { get; set; }

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

    
    

    // Used because of a crash on Android, if that ever gets fixed you can bind straight to the Albumart in PlayerState.
    public ImageSource ImageSource { get; set; }
    public HomePageModel(IVolumioRestService volumioRestService)
    {
        _volumioRestService = volumioRestService;
        //_volumioSocketIOService = new VolumioSocketIOService();
        _volumioService = new VolumioService();
        _volumioService.StatePushed += _volumioService_StatePushed;

        Init();

        ReloadCommand = new Command(async () =>
        {
            //await LoadDataAsync();
        });

        TogglePlaybackCommand = new Command(() =>
        {
            TogglePlayback();
        });

        PreviousTrackCommand = new Command(async() =>
        {
            await _volumioService.PreviousTrack();
            //await LoadDataAsync();
        });

        NextTrackCommand = new Command(async() =>
        {
            await _volumioService.NextTrack();
            //await LoadDataAsync();
        });

        MuteCommand = new Command(async () =>
        {
            await _volumioService.MuteVolume();
            //EditPlaybackValues = !EditPlaybackValues;
        });

        UnmuteCommand = new Command(async () =>
        {
            await _volumioService.UnmuteVolume();
            //await _volumioSocketIOService.VolumeToMax();
            //await _volumioRestService.UnmuteVolume();
            //EditPlaybackValues = !EditPlaybackValues;
        });

        EditPlaybackValuesCommand = new Command(async () =>
        {
            EditPlaybackValues = !EditPlaybackValues;
        });

        VolumeSliderDragCompletedCommand = new Command(async () =>
        {
            await _volumioService.ChangeVolume(PlayerState.Volume);
            //EditPlaybackValues = !EditPlaybackValues;
        });

        SeekSliderDragCompletedCommand = new Command(async () =>
        {
            await _volumioService.ChangeSeek(PlayerState.Seek / 1000);
        });



        PlayPauseImage = ImageSource.FromFile("play.png");
    }

    private void _volumioService_StatePushed(object sender, EventArgs e)
    {
        PlayerState = (PlayerState)sender;
    }

    private async void TogglePlayback()
    {
        PlayerState.IsPlaying = !PlayerState.IsPlaying;
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

        if((DateTimeOffset.UtcNow.ToUnixTimeSeconds() - PlayerState.LastUpdated.ToUnixTimeSeconds()) >= 1)
        {
            System.Diagnostics.Debug.WriteLine("Refresh");
            LoadDataAsync();
        }

    }

    private void IncrementSeek()
    {
        if (PlayerState.IsPlaying)
        {
            PlayerState.Seek += 1000;
        }

    }




    private async Task LoadDataAsync()
    {
        System.Diagnostics.Debug.WriteLine("Load data");

       PlayerState playerState = await _volumioRestService.GetPlayerState();
        PlayerState = playerState;
        
        // Setting ImageSource to null before setting it to the new one prevents a crash on Android when the image is the same and previously.
        ImageSource = null;
        ImageSource = ImageSource.FromUri(new Uri(PlayerState.Albumart));

        if(playerState.IsPlaying)
        {
            PlayPauseImage = ImageSource.FromFile("pause.png");
        }
        else
        {
            PlayPauseImage = ImageSource.FromFile("play.png");
        }
    }
}
