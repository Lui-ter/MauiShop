namespace petshop.Views;

public partial class MainContainer : ContentPage
{
    public static MainContainer? Current { get; private set; }

    public MainContainer()
    {
        InitializeComponent();
        Current = this;

        // Mostrar Home al iniciar
        MostrarHome();
    }


    private void Home_Tapped(object? sender, TappedEventArgs e)
    {
        MostrarHome();
    }


    private void Clientes_Tapped(object? sender, TappedEventArgs e)
    {
        MostrarClientes();
    }


    private void Productos_Tapped(object? sender, TappedEventArgs e)
    {
        MostrarProductos();
    }


    private void Categorias_Tapped(object? sender, TappedEventArgs e)
    {
        MostrarCategorias();
    }


    public void MostrarHome()
    {
        Contenido.Content = new MainPage().Content;
    }


    public void MostrarClientes()
    {
        Contenido.Content = new Cliente().Content;
    }


    public void MostrarProductos()
    {
        Contenido.Content = new Producto().Content;
    }


    public void MostrarCategorias()
    {
        Contenido.Content = new Categoria().Content;
    }


    public void MostrarDetalleCliente(petshop.DataAcces.Client cliente)
    {
        Contenido.Content = new DetalleCliente(cliente).Content;
    }


    public void MostrarDetalleCategoria(petshop.DataAcces.Category categoria)
    {
        Contenido.Content = new DetalleCategoria(categoria).Content;
    }


    public void MostrarDetalleProducto(petshop.DataAcces.Product producto)
    {
        Contenido.Content = new DetalleProducto(producto).Content;
    }


    public void MostrarAbout()
    {
        Contenido.Content = new AboutPage().Content;
    }
}

