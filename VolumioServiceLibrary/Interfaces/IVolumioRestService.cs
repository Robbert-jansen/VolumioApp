using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;

namespace VolumioServiceLibrary.Interfaces;

public interface IVolumioRestService
{
    Task<NavigationRoot> GetAlbums();
    Task<PlayerState> GetPlayerState();
}
