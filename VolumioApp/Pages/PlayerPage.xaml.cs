using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using VolumioApp.PageModels;

namespace VolumioApp.Pages;

public partial class PlayerPage : ContentPage
{
	public PlayerPage(PlayerPageModel playerPageModel)
	{
		InitializeComponent();
		this.BindingContext = playerPageModel;
    }
}