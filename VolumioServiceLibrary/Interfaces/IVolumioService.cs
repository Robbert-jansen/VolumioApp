using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;

namespace VolumioServiceLibrary.Interfaces;

public interface IVolumioService
{
    public event EventHandler StatePushed;
    public event EventHandler QueuePushed;

    Task TogglePlayback();

    Task NextTrack();

    Task PreviousTrack();

    Task MuteVolume();

    Task UnmuteVolume();

    Task ChangeVolume(int volume);

    Task ChangeSeek(int? seconds);

    Task<Queue> GetQueue();
}
