using Microsoft.Maui.Controls;
using petshop.DataAcces;
using petshop.Views;

namespace petshop.Handler;

public partial class ClienteBusquedaHandler : SearchHandler
{
    public ClienteBusquedaHandler()
    {
        InitializeComponent();
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
            return;
        }

        try
        {
            using var dbContext = new ShopDbContext();
            var query = newValue.Trim().ToLower();
            var resultados = dbContext.Clients
                .Where(c => (c.Nombre != null && c.Nombre.ToLower().Contains(query)) ||
                            (c.Apellido != null && c.Apellido.ToLower().Contains(query)) ||
                            (c.Direccion != null && c.Direccion.ToLower().Contains(query)))
                .ToList();

            ItemsSource = resultados;
        }
        catch
        {
            ItemsSource = null;
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        if (item is Client cliente)
        {
            if (MainContainer.Current != null)
            {
                MainContainer.Current.MostrarDetalleCliente(cliente);
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.Navigation.PushAsync(new DetalleCliente(cliente));
            }
            else if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyout && flyout.Detail?.Navigation != null)
            {
                await flyout.Detail.Navigation.PushAsync(new DetalleCliente(cliente));
            }
        }
    }
}

