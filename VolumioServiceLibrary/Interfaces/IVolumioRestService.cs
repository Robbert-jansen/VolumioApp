using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;

namespace VolumioServiceLibrary.Interfaces;

[Obsolete("IVolumioRestService is deprecated, please use IVolumioService instead.")]
public interface IVolumioRestService
{
    Task<NavigationRoot> GetAlbums();
    Task<PlayerState> GetPlayerState();

    Task TogglePlayback();

    Task NextTrack();

    Task PreviousTrack();

    Task MuteVolume();

    Task UnmuteVolume();

    Task ChangeVolume(int volume);
}
