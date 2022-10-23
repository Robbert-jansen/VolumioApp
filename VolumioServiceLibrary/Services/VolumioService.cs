using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using Queue = VolumioModelLibrary.Models.Queue;

namespace VolumioServiceLibrary.Services;

public class VolumioService : IVolumioService
{
    private static SocketIO? Socket { get; set; }
    private static readonly HttpClient client = new HttpClient();

    public event EventHandler? StatePushed;
    public event EventHandler? QueuePushed;

    public VolumioService()
    {    
        OnInit();
    }

    public async void OnInit()
    {
        Socket = new SocketIO("http://volumio.local:3000/");
        Socket.On("pushState", response =>
        {
            PlayerState? playerState = JsonConvert.DeserializeObject<PlayerState>(response.GetValue(0).ToString());
            if(playerState != null)
            {
                StatePushed?.Invoke(playerState, new EventArgs());
            }         
        });

        Socket.On("pushQueue", response =>
        {
            List<QueueItem> queue = JsonConvert.DeserializeObject<List<QueueItem>>(response.GetValue(0).ToString());
            if (queue != null)
            {
                QueuePushed?.Invoke(queue, new EventArgs());
            }
   
        });

        await Socket.ConnectAsync();

    }

    public async Task<Queue> GetQueue()
    {
        var response = await client.GetAsync("http://volumio.local/api/v1/getQueue");

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            Queue? queue = JsonConvert.DeserializeObject<Queue>(Content);
            if (queue != null)
            {
                return queue;
            }
        }
        return new Queue();
    }

    public async Task TogglePlayback()
    {
        await Socket!.EmitAsync("toggle");
    }

    public async Task NextTrack()
    {
        await Socket!.EmitAsync("next");
    }

    public async Task PreviousTrack()
    {
        await Socket!.EmitAsync("previous");
    }

    public async Task MuteVolume()
    {
        await Socket!.EmitAsync("mute");
    }

    public async Task UnmuteVolume()
    {
        await Socket!.EmitAsync("unmute");
    }

    public async Task ChangeVolume(int volume)
    {
        await Socket!.EmitAsync("volume", volume);
    }

    public async Task ChangeSeek(int? seconds)
    {
        await Socket!.EmitAsync("seek", seconds);
    }

    //public async Task GetQueue()
    //{
    //    await Socket!.EmitAsync("getQueue");
    //}
}

