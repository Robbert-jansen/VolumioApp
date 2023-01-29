using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;
using VolumioServiceLibrary.Services;

namespace VolumioAppUnitTests;

public class ServiceLibraryTests
{
	private readonly IVolumioService _volumioService;

	public ServiceLibraryTests()
	{
		_volumioService = new VolumioService();
	}
	
	[Fact]
	public async void GetPlayerState_AsyncAwait_ReturnsPlayerStateType()
	{
		var playerState = await _volumioService.GetPlayerState();

		Assert.IsType<PlayerState>(playerState);
	}

	[Fact]
	public async void GetQueue_AsyncAwait_ReturnsQueueType()
	{
		var queue = await _volumioService.GetQueue();

		Assert.IsType<Queue>(queue);
	}
	
	[Fact]
	public async void PlayTrackFromQueue_AsyncAwait_NoException()
	{
		
		try
		{
			var queue = await _volumioService.GetQueue();
			var rnd = new Random();
			if (queue.QueueItems.Count == 0)
			{
				Assert.Fail("Queue empty so method can't be tested");
			}
			await _volumioService.PlayTrackFromQueue(rnd.Next(0,queue.QueueItems.Count -1));
			
		}
		catch
		{
			Assert.Fail("Method threw an exception");
		}
	}
	
	
}