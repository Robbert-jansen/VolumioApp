using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumioModelLibrary.Models;
public class PlayerState : INotifyPropertyChanged
{
    public bool IsPlaying
    {
        get
        {
            if (Status == "play")
            {
                return true;
            }
            return false;
        }
        set
        {
            if(Status == "stop")
            {
                IsPlaying = false;
            }

            if(value)
            {
                Status = "play";
            }
            else
            {
                Status = "pause";
            }
            IsPlaying = value;
         }
    }

    public string Status { get; set; }
    public int Position { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }

    private string albumart;
    public string Albumart
    {
        get
        {
            return albumart;
        }
        set
        {
            albumart = "http://192.168.2.21" + value;
        }
    }
    public string Uri { get; set; }
    public string TrackType { get; set; }

    // Current timestamp in miliseconds.
    public int Seek { get; set; } 
    // Duration in seconds.
    public int Duration { get; set; }

    public string SampleRate { get; set; }
    public string BitDepth { get; set; }
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

    public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.UtcNow;

    public event PropertyChangedEventHandler? PropertyChanged;
}

