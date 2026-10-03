using Microsoft.Maui.Controls;
using petshop.DataAcces;
using petshop.Views;

namespace petshop.Handler;

public partial class ProductoBusquedaHandler : SearchHandler
{
    public ProductoBusquedaHandler()
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
            var resultados = dbContext.Products
                .Where(p => (p.Nombre != null && p.Nombre.ToLower().Contains(query)) ||
                            (p.Descripcion != null && p.Descripcion.ToLower().Contains(query)))
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

        if (item is Product producto)
        {
            if (MainContainer.Current != null)
            {
                MainContainer.Current.MostrarDetalleProducto(producto);
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.Navigation.PushAsync(new DetalleProducto(producto));
            }
            else if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyout && flyout.Detail?.Navigation != null)
            {
                await flyout.Detail.Navigation.PushAsync(new DetalleProducto(producto));
            }
        }
    }
}

