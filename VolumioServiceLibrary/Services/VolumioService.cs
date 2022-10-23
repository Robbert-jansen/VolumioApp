using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioServiceLibrary.Services;

public class VolumioService : IVolumioService
{
    private static SocketIO _client { get; set; }

    public event EventHandler StatePushed;

    public VolumioService()
    {
        OnInit();
    }

    public async void OnInit()
    {
        _client = new SocketIO("http://192.168.2.21:3000/");
        _client.On("pushState", response =>
        {
                PlayerState playerState = JsonConvert.DeserializeObject<PlayerState>(response.GetValue(0).ToString());
                StatePushed.Invoke(playerState,null);
            });

        await _client.ConnectAsync();

    }

    public async Task TogglePlayback()
    {
        await _client.EmitAsync("toggle");
    }

    public async Task NextTrack()
    {
        await _client.EmitAsync("next");
    }

    public async Task PreviousTrack()
    {
        await _client.EmitAsync("previous");
    }

    public async Task MuteVolume()
    {
        await _client.EmitAsync("mute");
    }

    public async Task UnmuteVolume()
    {
        await _client.EmitAsync("unmute");
    }

    public async Task ChangeVolume(int volume)
    {
        await _client.EmitAsync("volume", volume);
    }

    public async Task ChangeSeek(int seconds)
    {
        await _client.EmitAsync("seek", seconds);
    }
}

