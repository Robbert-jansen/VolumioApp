using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using VolumioModelLibrary.Enums;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioApp.PageModels;

public class HomePageModel : BasePageModel
{
	private readonly IVolumioService _volumioService;

    public PlayerState PlayerState { get; set; }

    private System.Timers.Timer Timer { get; set; }

    public bool IsInterpolating { get; set; } = true;

    public NavigationRoot NavigationRoot { get; set; }
	
	public ObservableCollection<List> NavigationLists { get; set; }

	public ItemsLayout ItemsLayout { get; set; }

    public ICommand ListItemSelectedCommand => new Command<List>(ListItemSelected);

    public ICommand GoToPreviousCommand => new Command(GoToPrevious);

    public ICommand ItemSelectedCommand => new Command<Item>(ItemSelected);

    public ICommand TogglePlaybackCommand { get; set; }

    public ICommand SeekSliderDragCompletedCommand { get; set; }
    public ICommand SeekSliderDragStartedCommand { get; set; }

    private async void ListItemSelected(List obj)
    {
        System.Diagnostics.Debug.WriteLine(" the selected item's name  is:  " + obj.Name);

        NavigationRoot = await _volumioService.GetNavigationState(obj.Uri);

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);

		if(NavigationRoot.Navigation.Lists.Count == 1)
		{
            var linearLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);

            ItemsLayout = linearLayout;
        }
		else
		{
            var GridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);
            GridLayout.Span = 2;

            ItemsLayout = GridLayout;
        }
    }

    private async void ItemSelected(Item obj)
    {
        System.Diagnostics.Debug.WriteLine(" the selected item's name  is:  " + obj.Title);

        NavigationRoot = await _volumioService.GetNavigationState(obj.Uri);

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);
    }

    private async void GoToPrevious()
    {
        if (NavigationRoot.Navigation.prev != null)
        {
            NavigationRoot = await _volumioService.GetNavigationState(NavigationRoot.Navigation.prev.uri);
        }
        else
        {
            NavigationRoot = await _volumioService.GetNavigationState();
        }

       

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);
    }


    public HomePageModel(IVolumioService volumioService)
	{
		_volumioService = volumioService;

        // Register event handlers.
        _volumioService.StatePushed += VolumioService_StatePushed;

        // Volumio Action Commands
        TogglePlaybackCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.TogglePlayback));
        // Volumio Actions triggered by sliders.
        SeekSliderDragCompletedCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.SetSeek));

        // Other Commands
        SeekSliderDragStartedCommand = new Command(() => { IsInterpolating = false; });


        var GridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);
		GridLayout.Span = 2;

		ItemsLayout = GridLayout;
		


        Init();

    }
	
	private async void Init()
	{
		await LoadDataAsync();
        await LoadDataAsync();
        await Task.Run(StartTimer);
    }
	
	private async Task LoadDataAsync()
	{
		NavigationRoot = await _volumioService.GetNavigationState();

		NavigationLists =  new ObservableCollection<List>(NavigationRoot.Navigation.Lists);

        var playerState = await _volumioService.GetPlayerState();
        PlayerState = playerState;



    }

    private void StartTimer()
    {
        Timer = new System.Timers.Timer(1000);
        Timer.AutoReset = true;
        Timer.Start();
        Timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        IncrementSeek();
    }

    private void IncrementSeek()
    {
        if (PlayerState.IsPlaying && IsInterpolating) PlayerState.Seek += 1000;
    }

    private async Task ExecuteVolumioAction(VolumioAction volumioAction)
    {
        switch (volumioAction)
        {
            case VolumioAction.TogglePlayback:
                await _volumioService.TogglePlayback();
                break;
            case VolumioAction.ToggleRepeat:
                await _volumioService.ToggleRepeat(PlayerState);
                break;
            case VolumioAction.ToggleShuffle:
                await _volumioService.ToggleShuffle(PlayerState);
                break;
            case VolumioAction.NextTrack:
                await _volumioService.NextTrack();
                break;
            case VolumioAction.PreviousTrack:
                await _volumioService.PreviousTrack();
                break;
            case VolumioAction.MuteVolume:
                await _volumioService.MuteVolume();
                break;
            case VolumioAction.UnmuteVolume:
                await _volumioService.UnmuteVolume();
                break;
            case VolumioAction.SetVolume:
                await _volumioService.ChangeVolume((int)PlayerState.Volume);
                break;
            case VolumioAction.SetSeek:
                await _volumioService.ChangeSeek(PlayerState.Seek / 1000);
                break;
        }
    }

    private void VolumioService_StatePushed(object sender, EventArgs e)
    {
        PlayerState = (PlayerState)sender;
    }

}