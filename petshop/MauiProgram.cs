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
            builder.Services.AddSingleton<IDatabasePathService, DatabasePathService>();
            builder.Services.AddDbContextFactory<ShopDbContext>((services, options) =>
            {
                var databasePath = services.GetRequiredService<IDatabasePathService>().GetDatabasePath();
                options.UseSqlite($"Data Source={databasePath}");
            });
            builder.Services.AddTransient<HelpSupportViewModel>();
            builder.Services.AddTransient<HelpSupportDetailViewModel>();
            builder.Services.AddTransient<ComprasViewModel>();
            builder.Services.AddTransient<HelpSupportPage>();
            builder.Services.AddTransient<HelpSupportDetailPage>();
            builder.Services.AddTransient<ComprasPage>();

            builder.Logging.AddDebug();

            var app = builder.Build();
            var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<ShopDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            dbContext.Database.EnsureCreated();

            return app;
        }
    }
}