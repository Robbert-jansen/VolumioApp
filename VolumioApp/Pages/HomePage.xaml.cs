using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		//var grid = (Grid)sender;

		//var vm = (HomePageModel)BindingContext;

		//if(vm.ListItemSelected.CanExecute(null))
		//{
		//	vm.ListItemSelected.Execute(null);
		//}

		//vm.ListItemSelected.Execute(grid.BindingContext);
		
	}
}