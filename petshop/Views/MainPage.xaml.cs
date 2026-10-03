using petshop.DataAcces;

namespace petshop.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        using var dbContext = new ShopDbContext();

        Listaproductos.ItemsSource =
            dbContext.Products
                .Where(c => c.CategoryId == 2)
                .ToList();

        client.Text =
            dbContext.Clients.Count().ToString();

        product.Text =
            dbContext.Products.Count().ToString();
    }
}