using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using petshop.DataAcces;
using petshop.Views;

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

            builder.Logging.AddDebug();

            var dbContext = new ShopDbContext();

            dbContext.Database.EnsureCreated();

            dbContext.Dispose();

            return builder.Build();
        }
    }
}