using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace PizzaMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            AddPizzaServices(builder.Services);
            return builder.Build();
        }
        private static IServiceCollection AddPizzaServices(IServiceCollection services)
        {
            services.AddSingleton<PizzaService> ();
            services.AddSingletonWithShellRoute<HomePage, HomeViewModel>(nameof(HomePage));
            services.AddTransientWithShellRoute<AllPizzasPage, AllPizzasViewModel>(nameof(AllPizzasPage));
            services.AddTransientWithShellRoute<DetailPage, DetailsViewModel>(nameof(DetailPage));

            return services;
        }
    }
}
