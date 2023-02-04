using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VolumioModelLibrary.Models;
using VolumioServiceLibrary.Interfaces;

namespace VolumioApp.PageModels;

public class HomePageModel : BasePageModel
{
	private readonly IVolumioService _volumioService;
	
	public NavigationRoot NavigationRoot { get; set; }
	
	public ObservableCollection<List> NavigationLists { get; set; }

	public ItemsLayout ItemsLayout { get; set; }

    public ICommand ListItemSelectedCommand => new Command<List>(ListItemSelected);

    public ICommand GoToPreviousCommand => new Command(GoToPrevious);

    public ICommand ItemSelectedCommand => new Command<Item>(ItemSelected);

    private async void ListItemSelected(List obj)
    {
        System.Diagnostics.Debug.WriteLine(" the selected item's name  is:  " + obj.Name);

        NavigationRoot = await _volumioService.GetNavigationState(obj.Uri);

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);

		if(NavigationRoot.Navigation.Lists.Count == 1)
		{
            var linearLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);

            ItemsLayout = linearLayout;
        }
		else
		{
            var GridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);
            GridLayout.Span = 2;

            ItemsLayout = GridLayout;
        }
    }

    private async void ItemSelected(Item obj)
    {
        System.Diagnostics.Debug.WriteLine(" the selected item's name  is:  " + obj.Title);

        NavigationRoot = await _volumioService.GetNavigationState(obj.Uri);

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);
    }

    private async void GoToPrevious()
    {
        if (NavigationRoot.Navigation.prev != null)
        {
            NavigationRoot = await _volumioService.GetNavigationState(NavigationRoot.Navigation.prev.uri);
        }
        else
        {
            NavigationRoot = await _volumioService.GetNavigationState();
        }

       

        NavigationLists = new ObservableCollection<List>(NavigationRoot.Navigation.Lists);
    }


    public HomePageModel(IVolumioService volumioService)
	{
		_volumioService = volumioService;

		var GridLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);
		GridLayout.Span = 2;

		ItemsLayout = GridLayout;
		


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