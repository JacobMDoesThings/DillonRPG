using Radzen;

namespace DillonRPG.Maui;

public static class MauiProgram
{
    public static string BaseAddress =
    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5159" : "https://localhost:7036";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(platform =>
                {
                    platform.OnActivityResult((activity, rc, result, data) =>
                    {
                        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(rc, result, data);
                    });
                });
#endif
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
        builder.Services.AddScoped<IAccountManager>(s => s.GetRequiredService<AuthStateProvider>());
        builder.Services.AddHttpClient<ServiceClientCaller>(c => c.BaseAddress = new Uri(BaseAddress));
        builder.Services.AddScoped<IWeatherServiceClient, WeatherServiceClient>();
        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<TooltipService>();
        return builder.Build();
    }
}