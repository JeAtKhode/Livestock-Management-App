using Microsoft.Extensions.Logging;

namespace Farm_App;

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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<DataView>();
            builder.Services.AddTransient<Query>();
            builder.Services.AddTransient<DeleteAnimal>();
            builder.Services.AddTransient<InsertAnimal>();
            builder.Services.AddTransient<UpdateAnimal>();
            builder.Services.AddTransient<Predictions>();
            builder.Services.AddTransient<Profits>();
            return builder.Build();
        }
    }