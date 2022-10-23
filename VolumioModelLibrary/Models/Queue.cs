namespace VolumioModelLibrary.Models;

public class QueueItem
{
    
    public string? Uri { get; set; }
    public string? Service { get; set; }
    public string? Name { get; set; }
    public string? Artist { get; set; }
    public string? Album { get; set; }
    public string? Type { get; set; }
    public int? TrackNumber { get; set; }
    public string? AlbumArt { get; set; }
    public int? Duration { get; set; }
    public string? TrackType { get; set; }
    public string? SampleRate { get; set; }
    public string? BitDepth { get; set; }
    public int? Channels { get; set; }
}

public class Queue
{
    public List<QueueItem>? QueueItems { get; set; }
}

