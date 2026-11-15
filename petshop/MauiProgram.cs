using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using petshop.DataAcces;
using petshop.Services;
using petshop.Views;
using petshop.ViewModels;

namespace petshop
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
                });

            builder.Services.AddSingleton<INavegacionService, NavegacionService>();
            builder.Services.AddTransient<HelpSupportViewModel>();
            builder.Services.AddTransient<HelpSupportDetailViewModel>();
            builder.Services.AddTransient<HelpSupportPage>();
            builder.Services.AddTransient<HelpSupportDetailPage>();

            builder.Logging.AddDebug();

            var dbContext = new ShopDbContext();

            dbContext.Database.EnsureCreated();

            dbContext.Dispose();

            return builder.Build();
        }
    }
}