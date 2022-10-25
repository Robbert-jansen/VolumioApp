using Bumptech.Glide.Manager;
using VolumioApp.PageModels;

namespace VolumioApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageModel homePageModel)
	{
		InitializeComponent();
		this.BindingContext = homePageModel;

		RotateImage();
		ScaleImage();
	}

	private async Task RotateImage()
	{
		while (true)
		{
			await playButtonImage.RotateTo(360, 5000);

			playButtonImage.Rotation = 0;

        }
	}

    private async Task ScaleImage()
    {
        while (true)
        {
			await playButtonImage.ScaleTo(1.05, 2000);
            await playButtonImage.ScaleTo(1.0, 2000);

        }
    }
}