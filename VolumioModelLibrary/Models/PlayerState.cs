using System.ComponentModel;

namespace VolumioModelLibrary.Models;

public class PlayerState : INotifyPropertyChanged
{
	private string? _albumArt;

	public bool IsPlaying
	{
		get
		{
			if (Status == "play") return true;
			return false;
		}
		set
		{
			if (Status == "stop") IsPlaying = false;

			if (value)
				Status = "play";
			else
				Status = "pause";
			IsPlaying = value;
		}
	}

	public string? Status { get; set; }
	public int Position { get; set; }
	public string? Title { get; set; }
	public string? Artist { get; set; }
	public string? Album { get; set; }

	public string? AlbumArt
	{
		get => _albumArt;
		set
		{
			if (value.Contains("http"))
				_albumArt = value;
			else
				_albumArt = "http://192.168.2.21" + value;
		}
	}

	public string? Uri { get; set; }
	public string? TrackType { get; set; }

	// Current timestamp in miliseconds.
	public int? Seek { get; set; }

	// Duration in seconds.
	public int Duration { get; set; }

	public string? SampleRate { get; set; }
	public string? BitDepth { get; set; }
	public bool? Random { get; set; }
	public bool? Repeat { get; set; }
	public bool RepeatSingle { get; set; }
	public bool Consume { get; set; }
	public int? Volume { get; set; }
	public object? DbVolume { get; set; }
	public bool DisableVolumeControl { get; set; }
	public bool Mute { get; set; }
	public string? Stream { get; set; }
	public bool Updatedb { get; set; }
	public bool Volatile { get; set; }
	public string? Service { get; set; }

	public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.UtcNow;

	#region INotifyPropertyChanged Members

	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion
}