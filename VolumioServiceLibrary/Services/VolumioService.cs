using Newtonsoft.Json;
using SocketIOClient;
using System.Diagnostics;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using Queue = VolumioModelLibrary.Models.Queue;

namespace VolumioServiceLibrary.Services;

public class VolumioService : IVolumioService
{
    private static SocketIO? Socket { get; set; }
    private static readonly HttpClient client = new HttpClient();

    // Socket EventHandlers
    public event EventHandler? StatePushed;
    public event EventHandler? QueuePushed;

    public VolumioService()
    {    
        OnInit();
    }

    public async void OnInit()
    {
        // Create new socket with server connection.
        Socket = new SocketIO("http://volumio.local:3000/");
        client.BaseAddress = new Uri("http://volumio.local/api/v1/");

        // Creates listeners for events emitted by server.
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

        Socket.OnDisconnected += Socket_OnDisconnected;
        Socket.OnConnected += Socket_OnConnected;

        // Connects to socket.
        await Socket.ConnectAsync();

    }


    #region Rest

    // HTTP Requests.
    public async Task<Queue> GetQueue()
    {
        return (Queue)await GetAsync(typeof(Queue), "getQueue");
    }

    public async Task<PlayerState> GetPlayerState()
    {
        return (PlayerState)await GetAsync(typeof(PlayerState), "getState");
    }


    // Is this nececcary? probably not but it's cool.
    private static async Task<object> GetAsync(Type returnType, string uri)
    {
        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var Content = await response.Content.ReadAsStringAsync();
            object returnObject = JsonConvert.DeserializeObject(Content, returnType);
            if (returnObject != null)
            {
                return returnObject;
            }
        }

        return Activator.CreateInstance(returnType); 
    }

    #endregion

    #region Socket.io

    // Socket connection handlers.
    private void Socket_OnConnected(object? sender, EventArgs e)
    {
        Debug.WriteLine("!!!!!!!!!!!!!!!Socket connected");
    }
    private void Socket_OnDisconnected(object? sender, string e)
    {
        Debug.WriteLine("!!!!!!!!!!!!!!!Socket Disconnected");
    }

    // Socket Emit methods.
    public async Task TogglePlayback()
    {
        await EmitAsync("toggle");
    }

    public async Task NextTrack()
    {
        await EmitAsync("next");
    }

    public async Task PreviousTrack()
    {
        await EmitAsync("previous");
    }

    public async Task MuteVolume()
    {
        await EmitAsync("mute");
    }

    public async Task UnmuteVolume()
    {
        await EmitAsync("unmute");
    }

    public async Task ChangeVolume(int volume)
    {
        await EmitAsync("volume", volume);
    }

    public async Task ChangeSeek(int? seconds)
    {
        if(seconds.HasValue)
        {
            await EmitAsync("seek", seconds);
        }
    }

    // Wraps EmitAsync in method to handle it in one place.
    private async Task EmitAsync(string eventName, params object[] data)
    {
        if(Socket != null && Socket.Connected)
        {
            await Socket!.EmitAsync(eventName, data);
        }
    }

    #endregion
}

