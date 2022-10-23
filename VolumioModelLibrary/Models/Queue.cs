using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumioModelLibrary.Models;

public class QueueItem
{
    public string? Uri { get; set; }
    public string? Service { get; set; }
    public string? Name { get; set; }
    public string? Artist { get; set; }
    public string? Album { get; set; }
    public string? Type { get; set; }
    public int? Tracknumber { get; set; }
    public string? Albumart { get; set; }
    public int? Duration { get; set; }
    public string? TrackType { get; set; }
    public string? Samplerate { get; set; }
    public string? Bitdepth { get; set; }
    public int? Channels { get; set; }
}

public class Queue
{
    public List<QueueItem>? QueueItems { get; set; }
}

