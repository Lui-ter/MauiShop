using petshop.DataAcces;

namespace petshop.Views;

public partial class DetalleProducto : ContentPage
{
    public DetalleProducto()
    {
        InitializeComponent();
    }

    public DetalleProducto(Product producto) : this()
    {
        CargarDatos(producto);
    }

    private void CargarDatos(Product producto)
    {
        if (producto == null) return;

        lblNombre.Text = producto.Nombre;
        lblPrecio.Text = $"${producto.Precio:N0}";
        lblId.Text = $"#{producto.Id:D3}";
        lblDescripcion.Text = !string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.Descripcion : "Sin descripción disponible";

        try
        {
            using var dbContext = new ShopDbContext();
            var categoria = dbContext.Categories.FirstOrDefault(c => c.Id == producto.CategoryId);
            lblCategoria.Text = categoria?.Nombre ?? $"Categoría #{producto.CategoryId}";
        }
        catch
        {
            lblCategoria.Text = $"Categoría #{producto.CategoryId}";
        }
    }

    private async void Volver_Tapped(object? sender, TappedEventArgs e)
    {
        if (MainContainer.Current != null)
        {
            MainContainer.Current.MostrarProductos();
        }
        else
        {
            await Navigation.PopAsync();
        }
    }

    private async void Volver_Clicked(object? sender, EventArgs e)
    {
        if (MainContainer.Current != null)
        {
            MainContainer.Current.MostrarProductos();
        }
        else
        {
            await Navigation.PopAsync();
        }
    }
}

