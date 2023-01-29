using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;

namespace VolumioServiceLibrary.Interfaces;

public interface IVolumioService
{
    /// <summary>
    /// Event that is triggered when a new/updated PlayerState is pushed.
    /// </summary>
    public event EventHandler StatePushed;
    /// <summary>
    /// Event that is triggered when a new/updated Queue is pushed.
    /// </summary>
    public event EventHandler QueuePushed;

    #region Rest
    /// <summary>
    /// Gets current PlayerState of Volumio Server.
    /// </summary>
    /// <returns>Current PlayerState.</returns>
    Task<PlayerState> GetPlayerState();
    /// <summary>
    /// Returns the current Queue of songs.
    /// </summary>
    /// <returns>Current Queue.</returns>
    Task<Queue> GetQueue();

    /// <summary>
    /// Starts playing specified track from the current Queue.
    /// </summary>
    /// <param name="trackPosition">The index of the desired song in the current Queue.</param>
    Task PlayTrackFromQueue(int trackPosition);
    
    /// <summary>
    /// Toggles between different repeating modes.
    /// </summary>
    /// <param name="playerState">Current PlayerState which will be used to determine the next repeat state.</param>
    Task ToggleRepeat(PlayerState playerState);
    
    /// <summary>
    /// Toggles between linear and shuffle playback.
    /// </summary>
    /// <param name="playerState">Current PlayerState which will be used to determine the next playback state.</param>
    Task ToggleShuffle(PlayerState playerState);
    #endregion

    #region Socket.io
    /// <summary>
    /// Toggles between play and pause states.
    /// </summary>
    Task TogglePlayback();

    /// <summary>
    /// Skips to next track in Queue
    /// </summary>
    Task NextTrack();

    /// <summary>
    /// Skips to previous track in Queue
    /// </summary>
    Task PreviousTrack();

    /// <summary>
    /// Mutes volume.
    /// </summary>
    Task MuteVolume();

    /// <summary>
    /// Unmutes Volume.
    /// </summary>
    /// <returns></returns>
    Task UnmuteVolume();

    /// <summary>
    /// Sets volume to specified percentage.
    /// </summary>
    /// <param name="volume">Integer value between 0 and 100.</param>
    /// <returns></returns>
    Task ChangeVolume(int volume);

    /// <summary>
    /// Moves seek to specified second.
    /// </summary>
    /// <param name="seconds">Integer value representing whole seconds.</param>
    Task ChangeSeek(int? seconds);
    #endregion
}
