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
    private static SocketIO? Client { get; set; }

    public event EventHandler? StatePushed;
    public event EventHandler? QueuePushed;

    public VolumioService()
    {    
        OnInit();
    }

    public async void OnInit()
    {
        Client = new SocketIO("http://192.168.2.21:3000/");
        Client.On("pushState", response =>
        {
            PlayerState? playerState = JsonConvert.DeserializeObject<PlayerState>(response.GetValue(0).ToString());
            if(playerState != null)
            {
                StatePushed?.Invoke(playerState, new EventArgs());
            }         
        });

        Client.On("pushQueue", response =>
        {
            Queue? queue = JsonConvert.DeserializeObject<Queue>(response.GetValue(0).ToString());
            if(queue != null)
            {
                QueuePushed?.Invoke(queue, new EventArgs());
            }         
        });

        await Client.ConnectAsync();

    }

    public async Task TogglePlayback()
    {
        await Client!.EmitAsync("toggle");
    }

    public async Task NextTrack()
    {
        await Client!.EmitAsync("next");
    }

    public async Task PreviousTrack()
    {
        await Client!.EmitAsync("previous");
    }

    public async Task MuteVolume()
    {
        await Client!.EmitAsync("mute");
    }

    public async Task UnmuteVolume()
    {
        await Client!.EmitAsync("unmute");
    }

    public async Task ChangeVolume(int volume)
    {
        await Client!.EmitAsync("volume", volume);
    }

    public async Task ChangeSeek(int? seconds)
    {
        await Client!.EmitAsync("seek", seconds);
    }

    public async Task GetQueue()
    {
        await Client!.EmitAsync("getQueue");
    }
}

