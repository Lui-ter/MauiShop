using CommunityToolkit.Mvvm.ComponentModel;
using petshop.DataAcces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace petshop.ViewModels;

public partial class HelpSupportDetailViewModel : ViewModelGlobal, IQueryAttributable
{
    private readonly MiComando _addCommand;
    private readonly MiComando _increaseQuantityCommand;
    private readonly MiComando _decreaseQuantityCommand;

    public HelpSupportDetailViewModel()
    {
        using var database = new ShopDbContext();
        Products = new ObservableCollection<Product>(database.Products);
        Compras = new ObservableCollection<Compra>(database.Compras);

        _addCommand = new MiComando(AgregarCompra, PuedeAgregarCompra);
        _increaseQuantityCommand = new MiComando(() => Cantidad++, () => Cantidad < 100);
        _decreaseQuantityCommand = new MiComando(() => Cantidad--, () => Cantidad > 1);
        Cantidad = 1;
    }

    [ObservableProperty]
    public partial ObservableCollection<Product> Products { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<Compra> Compras { get; set; }

    [ObservableProperty]
    public partial Product? ProductoSeleccionado { get; set; }

    [ObservableProperty]
    public partial int ClienteId { get; set; }

    [ObservableProperty]
    public partial int Cantidad { get; set; }

    public ICommand AddCommand => _addCommand;

    public ICommand IncreaseQuantityCommand => _increaseQuantityCommand;

    public ICommand DecreaseQuantityCommand => _decreaseQuantityCommand;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var idValue) ||
            !int.TryParse(idValue?.ToString(), out var clientId))
        {
            throw new ArgumentException("Se requiere un id de cliente válido.", nameof(query));
        }

        ClienteId = clientId;
    }

    partial void OnClienteIdChanged(int value) =>
        _addCommand.RaiseCanExecuteChanged();

    partial void OnProductoSeleccionadoChanged(Product? value) =>
        _addCommand.RaiseCanExecuteChanged();

    partial void OnCantidadChanged(int value)
    {
        _addCommand.RaiseCanExecuteChanged();
        _increaseQuantityCommand.RaiseCanExecuteChanged();
        _decreaseQuantityCommand.RaiseCanExecuteChanged();
    }

    private bool PuedeAgregarCompra() =>
        ClienteId > 0 && ProductoSeleccionado is not null && Cantidad > 0;

    private void AgregarCompra()
    {
        var producto = ProductoSeleccionado ??
            throw new InvalidOperationException("Selecciona un producto antes de agregar la compra.");
        var compra = new Compra(ClienteId, producto.Id, Cantidad);

        using var database = new ShopDbContext();
        database.Compras.Add(compra);
        database.SaveChanges();
        Compras.Add(compra);
    }
}
