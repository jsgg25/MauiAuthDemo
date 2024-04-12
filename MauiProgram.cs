using MauiAuthDemo.GoogleServices;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MauiAuthDemo
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).ConfigureLifecycleEvents(events =>
                {

#if IOS
                events.AddiOS(iOS => iOS.FinishedLaunching((App, launchOptions) => {
                        Firebase.Core.App.Configure();
                        return false;
                }));
#else
                    events.AddAndroid(android => android.OnCreate((activity, bundle) =>
                    {
                        Firebase.FirebaseApp.InitializeApp(activity);
                    }));
#endif


                });


            builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
            builder.Services.AddSingleton<MainPage> ();

            return builder.Build();
        }
    }
}
