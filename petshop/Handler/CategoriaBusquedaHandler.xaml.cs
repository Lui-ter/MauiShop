using Microsoft.Maui.Controls;
using petshop.DataAcces;
using petshop.Views;

namespace petshop.Handler;

public partial class CategoriaBusquedaHandler : SearchHandler
{
    public CategoriaBusquedaHandler()
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
            var resultados = dbContext.Categories
                .Where(c => c.Nombre != null && c.Nombre.ToLower().Contains(query))
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

        if (item is Category categoria)
        {
            if (MainContainer.Current != null)
            {
                MainContainer.Current.MostrarDetalleCategoria(categoria);
            }
            else if (Shell.Current != null)
            {
                await Shell.Current.Navigation.PushAsync(new DetalleCategoria(categoria));
            }
            else if (Application.Current?.Windows.FirstOrDefault()?.Page is FlyoutPage flyout && flyout.Detail?.Navigation != null)
            {
                await flyout.Detail.Navigation.PushAsync(new DetalleCategoria(categoria));
            }
        }
    }
}

