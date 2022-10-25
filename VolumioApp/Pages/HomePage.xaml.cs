using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using VolumioApp.PageModels;

namespace VolumioApp.Pages;

public partial class HomePage : ContentPage
{
	private int upperBound = 130;
	private int lowerBound = 115;
	public HomePage(HomePageModel homePageModel)
	{
		InitializeComponent();
		this.BindingContext = homePageModel;


        //OnInit();
		//
        //RotateImage();
		//ScaleImage();
		//ScaleEllipse1();
        //ScaleEllipse2();
        //ScaleEllipse3();
        //ScaleEllipse4();
        //ScaleEllipse2();
    }

    void OnTapGestureRecognizerTapped(object sender, EventArgs args)
    {
        OnInit();
    }

    private async Task OnInit()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        //var RotateImageTask = RotateImage();
        //var EllipseOneTask = ScaleEllipse1();
        //var EllipseTwoTask = ScaleEllipse2();
        //var EllipseThreeTask = ScaleEllipse3();
        //var EllipseFourTask = ScaleEllipse4();
        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                await EllipseGrid.RotateTo(360, 5000);

                EllipseGrid.Rotation = 0;

            }
        });

        Task.Factory.StartNew( async() =>
        {
            while (true)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse1.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse1 executed");
            }
        });

        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse2.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse2 executed");
            }
        });

        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse3.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse3 executed");
            }
        });

        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse4.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse4 executed");
            }
        });
        //var apiTasks = new List<Task> { RotateImageTask, EllipseOneTask, EllipseTwoTask, EllipseThreeTask, EllipseFourTask };
        // while (apiTasks.Count > 0)
        // {
        //     Task finishedTask = await Task.WhenAny(apiTasks);

        //     apiTasks.Remove(finishedTask);

        //     if (finishedTask == RotateImageTask)
        //     {
        //         Debug.WriteLine("Rotate Image task finished " + stopwatch.ElapsedMilliseconds + "ms");
        //         apiTasks.Add(RotateImage());
        //     }
        //     else if (finishedTask == EllipseOneTask)
        //     {
        //         Debug.WriteLine("EllipseOne task finished " + stopwatch.ElapsedMilliseconds + "ms");
        //         apiTasks.Add(EllipseOneTask);
        //     }
        //     else if (finishedTask == EllipseTwoTask)
        //     { 
        //         Debug.WriteLine("EllipseTwo task finished " + stopwatch.ElapsedMilliseconds + "ms");
        //         apiTasks.Add(EllipseTwoTask);
        //     }
        //     else if (finishedTask == EllipseThreeTask)
        //     {

        //         Debug.WriteLine("EllipseThree task finished " + stopwatch.ElapsedMilliseconds + "ms");
        //         apiTasks.Add(EllipseThreeTask);
        //     }
        //     else if (finishedTask == EllipseFourTask)
        //     {

        //         Debug.WriteLine("EllipseFour task finished " + stopwatch.ElapsedMilliseconds + "ms");
        //         apiTasks.Add(EllipseFourTask);
        //     }

        // }


    }
    private async Task RotateImage()
	{
		while (true)
		{
			await EllipseGrid.RotateTo(360, 5000);

            EllipseGrid.Rotation = 0;

		}
	}

    //   private async Task ScaleImage()
    //   {
    //       while (true)
    //       {
    //		await playButtonImage.ScaleTo(1.05, 2000);
    //           await playButtonImage.ScaleTo(1.0, 2000);

    //       }
    //   }

    private async Task ScaleEllipse1()
    {
        Random rnd = new Random();

        double randomInt = (double)rnd.Next(lowerBound, upperBound);
        await Ellipse1.ScaleXTo(randomInt / 100);
        await Task.Delay(2000);
        Console.WriteLine("ScaleEllipse1 executed");
    }
    private async Task ScaleEllipse2()
    {
        Random rnd = new Random();
        double randomInt = (double)rnd.Next(lowerBound, upperBound);
        await Ellipse1.ScaleXTo(randomInt / 100);
        await Task.Delay(2000);
        Console.WriteLine("ScaleEllipse2");
    }
    private async Task ScaleEllipse3()
    {
        Random rnd = new Random();
        double randomInt = (double)rnd.Next(lowerBound, upperBound);
        await Ellipse1.ScaleXTo(randomInt / 100);
        await Task.Delay(2000);
        Console.WriteLine("ScaleEllipse3");

    }
    private async Task ScaleEllipse4()
    {
        Random rnd = new Random();
        double randomInt = (double)rnd.Next(lowerBound, upperBound);
        await Ellipse1.ScaleXTo(randomInt / 100);
        await Task.Delay(2000);
        Console.WriteLine("ScaleEllipse4");

    }
}