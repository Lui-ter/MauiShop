using petshop.DataAcces;

namespace petshop.Views;

public partial class DetalleCategoria : ContentPage
{
    public DetalleCategoria()
    {
        InitializeComponent();
    }

    public DetalleCategoria(Category categoria) : this()
    {
        CargarDatos(categoria);
    }

    private void CargarDatos(Category categoria)
    {
        if (categoria == null) return;

        lblNombre.Text = categoria.Nombre;
        lblNombreDetalle.Text = categoria.Nombre;
        lblId.Text = $"#{categoria.Id:D3}";

        try
        {
            using var dbContext = new ShopDbContext();
            int total = dbContext.Products.Count(p => p.CategoryId == categoria.Id);
            lblTotalProductos.Text = $"{total} {(total == 1 ? "producto registrado" : "productos registrados")}";
        }
        catch
        {
            lblTotalProductos.Text = "Información no disponible";
        }
    }

    private async void Volver_Tapped(object? sender, TappedEventArgs e)
    {
        if (MainContainer.Current != null)
        {
            MainContainer.Current.MostrarCategorias();
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
            MainContainer.Current.MostrarCategorias();
        }
        else
        {
            await Navigation.PopAsync();
        }
    }
}

