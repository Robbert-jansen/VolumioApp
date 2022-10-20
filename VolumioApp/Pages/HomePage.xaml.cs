using VolumioApp.PageModels;

namespace VolumioApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageModel homePageModel)
	{
		InitializeComponent();
		this.BindingContext = homePageModel;
	}
}