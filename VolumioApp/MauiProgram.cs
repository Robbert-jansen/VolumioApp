using VolumioApp.PageModels;
using VolumioApp.Pages;
using VolumioServiceLibrary.Interfaces;
using VolumioServiceLibrary.Services;

namespace VolumioApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.RegisterAppServices()
            .RegisterAppViewModels()
			.RegisterAppPages();

        return builder.Build();
	}

    // Registers services for use in App.
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IVolumioService, VolumioService>();

        return mauiAppBuilder;
    }
    // Registers Pages for use in App.
    public static MauiAppBuilder RegisterAppPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<PlayerPage>();
        mauiAppBuilder.Services.AddTransient<HomePage>();
        
        return mauiAppBuilder;
    }

    // Registers ViewModels for use in App.
    public static MauiAppBuilder RegisterAppViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<PlayerPageModel>();
        mauiAppBuilder.Services.AddTransient<HomePageModel>();

        return mauiAppBuilder;
    }
}
