using petshop.DataAcces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace petshop.Views;

public partial class HelpSupportDetailPage : ContentPage, IQueryAttributable
{
    public HelpSupportDetailPage()
    {
        InitializeComponent();
    }

    public HelpSupportDetailPage(int clientId) : this()
    {
        ApplyQueryAttributes(new Dictionary<string, object> { ["id"] = clientId });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var idValue) ||
            !int.TryParse(idValue?.ToString(), out var clientId))
        {
            throw new ArgumentException("Se requiere un id de cliente válido.", nameof(query));
        }

        Title = $"Cliente: {clientId}";

        if (BindingContext is HelpSupportDetailData viewModel)
        {
            viewModel.ClienteId = clientId;
        }
    }
}
public class HelpSupportDetailData : BindingUtilObject
{
    private readonly MiComando _addCommand;
    private readonly MiComando _increaseQuantityCommand;
    private readonly MiComando _decreaseQuantityCommand;

    public HelpSupportDetailData()
    {
        using var database = new ShopDbContext();
        Products = new ObservableCollection<Product>(database.Products);
        Compras = new ObservableCollection<Compra>(database.Compras);
        _addCommand = new MiComando(AgregarCompra, PuedeAgregarCompra);
        _increaseQuantityCommand = new MiComando(() => Cantidad++, () => Cantidad < 100);
        _decreaseQuantityCommand = new MiComando(() => Cantidad--, () => Cantidad > 1);
    }

    public ICommand AddCommand => _addCommand;

    public ICommand IncreaseQuantityCommand => _increaseQuantityCommand;

    public ICommand DecreaseQuantityCommand => _decreaseQuantityCommand;

    public ObservableCollection<Compra> Compras { get; }

    private int _clienteId;

    public int ClienteId
    {
        get => _clienteId;
        set
        {
            if (_clienteId != value)
            {
                _clienteId = value;
                RaisePropertyChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }
    }

    private ObservableCollection<Product> _products = new();

    public ObservableCollection<Product> Products
    {
        get { return _products; }
        set
        {
            if (_products != value)
            {
                _products = value;
                RaisePropertyChanged();
            }
        }
    }
    private Product? _productoSeleccionado;

    public Product? ProductoSeleccionado
    {
        get { return _productoSeleccionado; }
        set
        {
            if (_productoSeleccionado != value)
            {
                _productoSeleccionado = value;
                RaisePropertyChanged();
                _addCommand.RaiseCanExecuteChanged();
            }
        }
    }
    private int _cantidad = 1;
    public int Cantidad
    {
        get { return _cantidad; }
        set
        {
            if (_cantidad != value)
            {
                _cantidad = value;
                RaisePropertyChanged();
                _addCommand.RaiseCanExecuteChanged();
                _increaseQuantityCommand.RaiseCanExecuteChanged();
                _decreaseQuantityCommand.RaiseCanExecuteChanged();
            }
        }
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