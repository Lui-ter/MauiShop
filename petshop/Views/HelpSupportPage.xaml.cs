using Microsoft.Extensions.DependencyInjection;
using petshop.DataAcces;
using petshop.ViewModels;

namespace petshop.Views;

public partial class HelpSupportPage : ContentPage
{
    private readonly HelpSupportViewModel _viewModel;

    public HelpSupportPage(HelpSupportViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        Content.BindingContext = viewModel;
    }

    public HelpSupportPage()
        : this(ResolveViewModel())
    {
    }

    private static HelpSupportViewModel ResolveViewModel()
    {
        var services = Application.Current?.Handler?.MauiContext?.Services
            ?? throw new InvalidOperationException("No se encontró el contenedor de dependencias de MAUI.");

        return services.GetRequiredService<HelpSupportViewModel>();
    }

    private async void OnClientSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Client client)
        {
            await _viewModel.NavigateToDetailAsync(client);
        }
    }
}
