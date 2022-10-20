using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumioModelLibrary.Models;
public class PlayerState
{
    public string Status { get; set; }
    public int Position { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Albumart { get; set; }
    public string Uri { get; set; }
    public string TrackType { get; set; }
    public int Seek { get; set; }
    public int Duration { get; set; }
    public object Random { get; set; }
    public object Repeat { get; set; }
    public bool RepeatSingle { get; set; }
    public bool Consume { get; set; }
    public int Volume { get; set; }
    public object DbVolume { get; set; }
    public bool DisableVolumeControl { get; set; }
    public bool Mute { get; set; }
    public string Stream { get; set; }
    public bool Updatedb { get; set; }
    public bool Volatile { get; set; }
    public string Service { get; set; }
}

