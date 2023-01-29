using System.Diagnostics;
using Newtonsoft.Json;
using SocketIOClient;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioServiceLibrary.Services;

public class VolumioService : IVolumioService
{
	private static HttpClient client = new();

	public VolumioService()
	{
		OnInit();
	}

	private static SocketIO? Socket { get; set; }

	#region IVolumioService Members

	// Socket EventHandlers
	public event EventHandler? StatePushed;
	public event EventHandler? QueuePushed;

	#endregion

	public async void OnInit()
	{
		// Create new socket with server connection.
		//Socket = new SocketIO("http://volumio.local:3000/");
		//client.BaseAddress = new Uri("http://volumio.local/api/v1/");

		//IPs for Android emualtor, Android emulators can't rersolve hostnames.
		client.BaseAddress = new Uri("http://192.168.2.21/api/v1/");
		Socket = new SocketIO("http://192.168.2.21:3000/");


		// Creates listeners for events emitted by server.
		Socket.On("pushState", response =>
		{
			Debug.WriteLine("State pushed");
			var playerState = JsonConvert.DeserializeObject<PlayerState>(response.GetValue().ToString());
			if (playerState != null) StatePushed?.Invoke(playerState, new EventArgs());
		});

		Socket.On("pushQueue", response =>
		{
			Debug.WriteLine("Queue pushed");
			var queue = JsonConvert.DeserializeObject<List<QueueItem>>(response.GetValue().ToString());
			if (queue != null) QueuePushed?.Invoke(queue, new EventArgs());
		});

		Socket.OnDisconnected += Socket_OnDisconnected;
		Socket.OnConnected += Socket_OnConnected;

		// Connects to socket.
		try
		{
			await Socket.ConnectAsync();
		}
		catch (Exception e)
		{
			Socket = new SocketIO("http://192.168.2.21:3000/");
			client = new HttpClient();
			client.BaseAddress = new Uri("http://192.168.2.21/api/v1/");
			await Socket.ConnectAsync();
		}
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

	public async Task PlayTrackFromQueue(int trackPosition)
	{
		await client.GetAsync("commands/?cmd=play&N=" + trackPosition);
	}

	// Is this nececcary? probably not but it's cool.
	private static async Task<object> GetAsync(Type returnType, string uri)
	{
		var response = await client.GetAsync(uri);

		if (response.IsSuccessStatusCode)
		{
			var Content = await response.Content.ReadAsStringAsync();
			var returnObject = JsonConvert.DeserializeObject(Content, returnType);
			if (returnObject != null) return returnObject;
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

	/*public async Task ToggleRepeat(bool repeat, bool repeatSingle)
	{
	    await EmitAsync("setRepeat", new object[]
	    {
	        new { value = repeat, repeatSingle = repeatSingle }
	    });
	}*/

	public async Task ToggleRepeat(PlayerState playerState)
	{
		var data = new object();
		// Checks current state and uses that to determine next state.
		if (playerState.Repeat == false)
			data = new { value = true, repeatSingle = false };
		else if (playerState is { Repeat: true, RepeatSingle: false })
			data = new { value = true, repeatSingle = true };
		else if (playerState.Repeat == true) data = new { value = false, repeatSingle = false };

		await EmitAsync("setRepeat", data);
	}

	public async Task ToggleShuffle(PlayerState playerState)
	{
		await EmitAsync("setRandom", new { value = !playerState.Random });
	}

	public async Task NextTrack()
	{
		await EmitAsync("next");
	}

	public async Task PreviousTrack()
	{
		await EmitAsync("prev");
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
		if (seconds.HasValue) await EmitAsync("seek", seconds);
	}

	// Wraps EmitAsync in method to handle it in one place.
	private async Task EmitAsync(string eventName, params object[] data)
	{
		if (Socket != null && Socket.Connected) await Socket!.EmitAsync(eventName, data);
	}

	#endregion
}