using AcademicHybridApp.Services;
using Microsoft.Extensions.Logging;

namespace AcademicHybridApp
{
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(ApiSettings.BaseAddress),
                Timeout = TimeSpan.FromSeconds(10)
            });

            // Service yang akan dipakai oleh Blazor
            builder.Services.AddTransient<DosenService>();
            builder.Services.AddTransient<MahasiswaService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
