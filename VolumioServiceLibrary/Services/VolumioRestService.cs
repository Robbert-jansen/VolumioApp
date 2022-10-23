using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioServiceLibrary.Services;

public class VolumioRestService : IVolumioRestService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<NavigationRoot> GetAlbums()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/browse?uri=albums://");

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<Navigation>(Content);
            return JsonConvert.DeserializeObject<NavigationRoot>(Content);
        }
        return null;
    }

    public async Task<PlayerState> GetPlayerState()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/getState");

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PlayerState>(Content);

        }
        return null;
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

        if (response.IsSuccessStatusCode)
        {


        }
    }

    public async Task PreviousTrack()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=prev");

        if (response.IsSuccessStatusCode)
        {


        }
    }

    public async Task TogglePlayback()
    {
        var response = await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=toggle");

        if (response.IsSuccessStatusCode)
        {


        }
    }

    public async Task ChangeVolume(int volume)
    {
        await client.GetAsync("http://192.168.2.21/api/v1/commands/?cmd=volume&volume=" + volume);
    }
}
