using petshop.DataAcces;

namespace petshop.Views;

public partial class DetalleCliente : ContentPage
{
    public DetalleCliente()
    {
        InitializeComponent();
    }

    public DetalleCliente(Client cliente) : this()
    {
        CargarDatos(cliente);
    }

    private void CargarDatos(Client cliente)
    {
        if (cliente == null) return;

        lblNombreCompleto.Text = $"{cliente.Nombre} {cliente.Apellido}".Trim();
        lblId.Text = $"#{cliente.Id:D3}";
        lblNombre.Text = cliente.Nombre;
        lblApellido.Text = cliente.Apellido;
        lblDireccion.Text = !string.IsNullOrWhiteSpace(cliente.Direccion) ? cliente.Direccion : "No especificada";
    }

    private async void Volver_Tapped(object? sender, TappedEventArgs e)
    {
        if (MainContainer.Current != null)
        {
            MainContainer.Current.MostrarClientes();
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
            MainContainer.Current.MostrarClientes();
        }
        else
        {
            await Navigation.PopAsync();
        }
    }
}

