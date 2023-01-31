﻿using VolumioModelLibrary.Models;
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
			await _volumioService.PlayTrackFromQueue(1);
		}
		catch
		{
			Assert.Fail("Method threw an exception");
		}
	}
	
	
}