using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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