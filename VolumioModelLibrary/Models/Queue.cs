using Newtonsoft.Json;
using System.ComponentModel;

namespace VolumioModelLibrary.Models;

public class QueueItem : INotifyPropertyChanged
{
    
    public string? Uri { get; set; }
    public string? Service { get; set; }
    public string? Name { get; set; }
    public string? Artist { get; set; }
    public string? Album { get; set; }
    public string? Type { get; set; }
    public int? TrackNumber { get; set; }
    private string? _albumArt;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? AlbumArt
    {
        get => _albumArt;
        set
        {
            if (value.Contains("http"))
            {
                _albumArt = value;
            }
            else
            {
                _albumArt = "http://volumio.local" + value;
            }

        }
    }
    public int? Duration { get; set; }
    public string? TrackType { get; set; }
    public string? SampleRate { get; set; }
    public string? BitDepth { get; set; }
    public int? Channels { get; set; }

    public bool IsPlaying { get; set; }
}

public class Queue : INotifyPropertyChanged
{
    [JsonProperty("Queue")]
    public List<QueueItem>? QueueItems { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
}

