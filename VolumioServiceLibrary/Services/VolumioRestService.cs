using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioServiceLibrary.Services;

[Obsolete("VolumioRestService is deprecated, please use VolumioService instead.")]
public class VolumioRestService : IVolumioRestService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<NavigationRoot> GetAlbums()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/browse?uri=albums://");

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            NavigationRoot? navigationRoot = JsonConvert.DeserializeObject<NavigationRoot>(Content);
            if(navigationRoot != null)
            {
                return navigationRoot;
            }         
        }
        return new NavigationRoot();
    }

    public async Task<PlayerState> GetPlayerState()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/getState");

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            PlayerState? playerState = JsonConvert.DeserializeObject<PlayerState>(Content);
            if(playerState != null)
            {
                return playerState;
            }
        }
        return new PlayerState();
    }

    public async Task MuteVolume()
    {
        await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=volume&volume=mute");
    }

    public async Task UnmuteVolume()
    {
        await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=volume&volume=unmute");
    }

    public async Task NextTrack()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=next");
    }

    public async Task PreviousTrack()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=prev");
    }

    public async Task TogglePlayback()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=toggle");
    }

    public async Task ChangeVolume(int volume)
    {
        await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=volume&volume=" + volume);
    }
}
