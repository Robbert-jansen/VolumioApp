using System.ComponentModel;
using Newtonsoft.Json;

namespace VolumioModelLibrary.Models;

public class QueueItem : INotifyPropertyChanged
{
	private string? _albumArt;

	public string? Uri { get; set; }
	public string? Service { get; set; }
	public string? Name { get; set; }
	public string? Artist { get; set; }
	public string? Album { get; set; }
	public string? Type { get; set; }
	public int? TrackNumber { get; set; }

	public string? AlbumArt
	{
		get => _albumArt;
		set
		{
			if (value.Contains("http"))
				_albumArt = value;
			else
				_albumArt = "http://192.168.2.21" + value;
			//_albumArt = "http://volumio.local" + value;
		}
	}

	public int? Duration { get; set; }
	public string? TrackType { get; set; }
	public string? SampleRate { get; set; }
	public string? BitDepth { get; set; }
	public int? Channels { get; set; }

	public bool IsPlaying { get; set; }

	#region INotifyPropertyChanged Members

	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion
}

public class Queue : INotifyPropertyChanged
{
	[JsonProperty("Queue")] public List<QueueItem>? QueueItems { get; set; }

	#region INotifyPropertyChanged Members

	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion
}