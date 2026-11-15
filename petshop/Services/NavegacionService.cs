using Microsoft.Extensions.DependencyInjection;
using petshop.ViewModels;
using petshop.Views;

namespace petshop.Services;

public sealed class NavegacionService : INavegacionService
{
    private readonly IServiceProvider _serviceProvider;

    public NavegacionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task NavegarAClienteAsync(int clienteId)
    {
        var window = Application.Current?.Windows.FirstOrDefault()
            ?? throw new InvalidOperationException("No se encontró una ventana activa.");

        if (window.Page is not About { Detail: NavigationPage navigationPage })
        {
            throw new InvalidOperationException("No se encontró la navegación principal de la aplicación.");
        }

        var page = _serviceProvider.GetRequiredService<HelpSupportDetailPage>();
        if (page.BindingContext is not IQueryAttributable viewModel)
        {
            throw new InvalidOperationException("El ViewModel de detalle no recibe parámetros de navegación.");
        }

        viewModel.ApplyQueryAttributes(new Dictionary<string, object> { ["id"] = clienteId });
        await navigationPage.PushAsync(page);
    }
}
