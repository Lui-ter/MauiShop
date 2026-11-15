using CommunityToolkit.Mvvm.ComponentModel;
using petshop.DataAcces;
using petshop.Services;
using System.Collections.ObjectModel;

namespace petshop.ViewModels;

public partial class HelpSupportViewModel : ViewModelGlobal
{
    private readonly INavegacionService _navegacionService;

    public HelpSupportViewModel(INavegacionService navegacionService)
    {
        _navegacionService = navegacionService;

        using var database = new ShopDbContext();
        Clients = new ObservableCollection<Client>(database.Clients);
    }

    [ObservableProperty]
    public partial ObservableCollection<Client> Clients { get; set; }

    [ObservableProperty]
    public partial Client? ClienteSeleccionado { get; set; }

    [ObservableProperty]
    public partial int VisitasPendientes { get; set; }

    public Task NavigateToDetailAsync(Client client) =>
        _navegacionService.NavegarAClienteAsync(client.Id);
}
