using GameLibrary.Client.Services;
using GameLibrary.Client.ViewModels;
using GameLibrary.Client.Pages;
using GameLibrary.Client.Models;

namespace GameLibrary.Client;

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
			});

		// services
        builder.Services.AddSingleton<IDataService, DataService>();
        builder.Services.AddSingleton<IJsonService, JsonService>();

		// views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainMenuPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<MyGamesPage>();

        //view models
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainMenuViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<MyGamesViewModel>();

        builder.Services.AddHttpClient<ServerAPIClient>(client => client.BaseAddress = new Uri("https://localhost:7031"));

        return builder.Build();
	}
}
