using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using petshop.DataAcces;
using petshop.Services;
using System.Collections.ObjectModel;

namespace petshop.ViewModels;

public partial class HelpSupportViewModel : ViewModelGlobal
{
    private readonly INavegacionService _navegacionService;
    private readonly IDbContextFactory<ShopDbContext> _dbContextFactory;

    public HelpSupportViewModel(
        INavegacionService navegacionService,
        IDbContextFactory<ShopDbContext> dbContextFactory)
    {
        _navegacionService = navegacionService;
        _dbContextFactory = dbContextFactory;

        using var database = _dbContextFactory.CreateDbContext();
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
