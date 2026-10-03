using Microsoft.Maui.Controls.Shapes;
using petshop.DataAcces;

namespace petshop.Views;

public partial class Categoria : ContentPage
{
	public Categoria()
	{
		InitializeComponent();

        var dbContext = new ShopDbContext();

        foreach (var categoria in dbContext.Categories)
        {
            var tarjeta = new Border
            {
                BackgroundColor = Color.FromArgb("#1E1E1E"),
                Stroke = Color.FromArgb("#333333"),
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 15
                },
                Padding = 18,
                Margin = new Thickness(0, 5)
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
            };

            var nombre = new Label
            {
                Text = categoria.Nombre,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center
            };

            var iconoDetalle = new Label
            {
                Text = "➜",
                FontSize = 18,
                TextColor = Color.FromArgb("#A98BFF"),
                VerticalOptions = LayoutOptions.Center
            };

            grid.Children.Add(nombre);
            Grid.SetColumn(nombre, 0);

            grid.Children.Add(iconoDetalle);
            Grid.SetColumn(iconoDetalle, 1);

            tarjeta.Content = grid;

            var cat = categoria;
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                if (MainContainer.Current != null)
                {
                    MainContainer.Current.MostrarDetalleCategoria(cat);
                }
                else
                {
                    var nav = container.Navigation ?? (Application.Current?.Windows.FirstOrDefault()?.Page as FlyoutPage)?.Detail?.Navigation;
                    if (nav != null)
                    {
                        await nav.PushAsync(new DetalleCategoria(cat));
                    }
                }
            };
            tarjeta.GestureRecognizers.Add(tapGesture);

            container.Children.Add(tarjeta);
        }
    }
}
