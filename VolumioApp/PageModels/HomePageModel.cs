using System.Collections.ObjectModel;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioApp.PageModels;

public class HomePageModel : BasePageModel
{
	private readonly IVolumioService _volumioService;
	
	public NavigationRoot NavigationRoot { get; set; }
	
	public ObservableCollection<List> NavigationLists { get; set; }

	public HomePageModel(IVolumioService volumioService)
	{
		_volumioService = volumioService;
		
		Init();
	}
	
	private async void Init()
	{
		await LoadDataAsync();
	}
	
	private async Task LoadDataAsync()
	{
		NavigationRoot = await _volumioService.GetNavigationState();

		NavigationLists =  new ObservableCollection<List>(NavigationRoot.Navigation.Lists);

	}

}