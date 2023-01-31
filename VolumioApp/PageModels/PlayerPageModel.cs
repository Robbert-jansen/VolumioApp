using System.Diagnostics.CodeAnalysis;
using System.Timers;
using System.Windows.Input;
using VolumioModelLibrary.Enums;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using Timer = System.Timers.Timer;

namespace VolumioApp.PageModels;

[SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task")]
public class PlayerPageModel : BasePageModel
{
	private readonly IVolumioService _volumioService;

	public PlayerPageModel(IVolumioService volumioService)
	{
		_volumioService = volumioService;

		// Register event handlers.
		_volumioService.StatePushed += VolumioService_StatePushed;
		_volumioService.QueuePushed += VolumioService_QueuePushed;

		// Volumio Action Commands
		TogglePlaybackCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.TogglePlayback));
		ToggleShuffleCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.ToggleShuffle));
		ToggleRepeatCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.ToggleRepeat));
		PreviousTrackCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.PreviousTrack));
		NextTrackCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.NextTrack));
		MuteCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.MuteVolume));
		UnmuteCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.UnmuteVolume));

		// Volumio Actions triggered by sliders.
		VolumeSliderDragCompletedCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.SetVolume));
		SeekSliderDragCompletedCommand = new Command(async () => await ExecuteVolumioAction(VolumioAction.SetSeek));

		// Other Commands
		SeekSliderDragStartedCommand = new Command(() => { IsInterpolating = false; });
		DisableOverlayCommand = new Command(() =>
		{
			EditPlaybackValues = false;
			ShowQueue = false;
		});
		EditPlaybackValuesCommand = new Command(() => { EditPlaybackValues = !EditPlaybackValues; });
		ShowQueueCommand = new Command(() => { ShowQueue = !ShowQueue; });

		PlayFromQueueCommand = new Command<QueueItem>(async queueItem =>
		{
			var indexOfQueueitem = Queue.QueueItems.IndexOf(queueItem);
			await _volumioService.PlayTrackFromQueue(indexOfQueueitem);
		});

		Init();
	}

	private Timer Timer { get; set; }

	// Booleans.
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

	// Objects
	public PlayerState PlayerState { get; set; }
	public Queue Queue { get; set; }

	#region Methods

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

	private async void Init()
	{
		// Double load on init is intentional
		// Progress bar in .NET MAUI overwrites the binding value on first binding.
		// Loading it twice in sucession fixed it for now.
		await LoadDataAsync();
		await LoadDataAsync();
		await Task.Run(StartTimer);
	}

	private async Task LoadDataAsync()
	{
		var playerState = await _volumioService.GetPlayerState();
		PlayerState = playerState;

		var queue = await _volumioService.GetQueue();
		Queue = queue;

		MarkPlayingSongInQueue();
	}

	private void StartTimer()
	{
		Timer = new Timer(1000);
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

	#endregion
	
	#region Commands

	// Volumio Actions.
	public ICommand MuteCommand { get; set; }
	public ICommand UnmuteCommand { get; set; }
	public ICommand TogglePlaybackCommand { get; set; }
	public ICommand ToggleShuffleCommand { get; set; }
	public ICommand ToggleRepeatCommand { get; set; }
	public ICommand NextTrackCommand { get; set; }
	public ICommand PreviousTrackCommand { get; set; }

	// UI Commands.
	public ICommand EditPlaybackValuesCommand { get; set; }
	public ICommand ShowQueueCommand { get; set; }
	public ICommand DisableOverlayCommand { get; set; }

	// Slider Commands.
	public ICommand VolumeSliderDragCompletedCommand { get; set; }
	public ICommand SeekSliderDragCompletedCommand { get; set; }
	public ICommand SeekSliderDragStartedCommand { get; set; }

	public ICommand PlayFromQueueCommand { get; set; }

	#endregion
	
	#region Volumio EventHandlers

	private void VolumioService_QueuePushed(object sender, EventArgs e)
	{
		Queue.QueueItems.Clear();
		Queue.QueueItems = (List<QueueItem>)sender;

		MarkPlayingSongInQueue();
	}

	private void VolumioService_StatePushed(object sender, EventArgs e)
	{
		PlayerState = (PlayerState)sender;

		MarkPlayingSongInQueue();
	}

	private void MarkPlayingSongInQueue()
	{
		foreach (var queueItem in Queue.QueueItems)
		{
			queueItem.IsPlaying = false;
			if (queueItem.Uri == PlayerState.Uri) queueItem.IsPlaying = true;
		}
	}

	#endregion
}