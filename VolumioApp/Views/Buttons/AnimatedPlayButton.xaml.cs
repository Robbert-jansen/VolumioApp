using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace VolumioApp.Views.Buttons;

public partial class AnimatedPlayButton : ContentView
{

    public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create("IsPlaying", typeof(bool), typeof(AnimatedPlayButton), false, BindingMode.TwoWay, propertyChanged: OnIsPlayingChanged);

    public static readonly BindableProperty CommandProperty =BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimatedPlayButton));

   

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    static void OnIsPlayingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var instance = (AnimatedPlayButton)bindable;

        instance.PlayImage.IsVisible = !(bool)newValue;
        instance.PauseImage.IsVisible = (bool)newValue;

        if ((bool)newValue)
        {
            instance.OnInit();
        }

    }

    private int upperBound = 130;
    private int lowerBound = 115;

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);          
    }
    public AnimatedPlayButton()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizerTapped(object sender, EventArgs e)
    {
        if (Command.CanExecute(null))
        {
            Command.Execute(null);
        }

    }

    private async Task OnInit()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        Task.Factory.StartNew(async () =>
        {
            while (IsPlaying)
            {
                await EllipseGrid.RotateTo(360, 5000);

                EllipseGrid.Rotation = 0;

                Console.WriteLine("Rotate Grid executed");
            }

        });

        Task.Factory.StartNew(async () =>
        {
            while (IsPlaying)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse1.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse1 executed");
            }

            await Ellipse1.ScaleXTo(1, 1000, Easing.CubicInOut);
        });

        Task.Factory.StartNew(async () =>
        {
            while (IsPlaying)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse2.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse2 executed");
            }
            await Ellipse2.ScaleXTo(1, 1000, Easing.CubicInOut);
        });

        Task.Factory.StartNew(async () =>
        {
            while (IsPlaying)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse3.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse3 executed");
            }
            await Ellipse3.ScaleXTo(1, 1000, Easing.CubicInOut);
        });

        Task.Factory.StartNew(async () =>
        {
            while (IsPlaying)
            {
                Random rnd = new Random();
                double randomInt = (double)rnd.Next(lowerBound, upperBound);
                await Ellipse4.ScaleXTo(randomInt / 100, 2000, Easing.CubicInOut);
                Console.WriteLine("ScaleEllipse4 executed");
            }
            await Ellipse4.ScaleXTo(1, 1000, Easing.CubicInOut);
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
}