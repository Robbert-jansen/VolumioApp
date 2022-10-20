using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioApp.PageModels;

public class HomePageModel
{
    private readonly IVolumioRestService _volumioRestService;

    public PlayerState PlayerState { get; set; }
    public HomePageModel(IVolumioRestService volumioRestService)
    {
        _volumioRestService = volumioRestService;
        init();
    }

    private async void init()
    {
        PlayerState = await _volumioRestService.GetPlayerState();
    }
}
