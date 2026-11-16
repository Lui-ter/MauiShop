using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using petshop.DataAcces;
using System.Collections.ObjectModel;

namespace petshop.ViewModels;

public partial class ComprasViewModel : ViewModelGlobal
{
    public ComprasViewModel(IDbContextFactory<ShopDbContext> dbContextFactory)
    {
        using var database = dbContextFactory.CreateDbContext();
        var compras = database.Compras.AsNoTracking().ToList();
        var productos = database.Products.AsNoTracking().ToDictionary(producto => producto.Id);
        var clientes = database.Clients.AsNoTracking().ToDictionary(cliente => cliente.Id);

        Compras = new ObservableCollection<CompraDbItem>(compras.Select(compra =>
        {
            if (!productos.TryGetValue(compra.ProductId, out var producto))
            {
                throw new InvalidOperationException(
                    $"La compra {compra.Id} referencia el producto {compra.ProductId}, que no existe.");
            }

            if (!clientes.TryGetValue(compra.ClientId, out var cliente))
            {
                throw new InvalidOperationException(
                    $"La compra {compra.Id} referencia el cliente {compra.ClientId}, que no existe.");
            }

            return new CompraDbItem(
                compra.Id,
                $"{cliente.Nombre} {cliente.Apellido}",
                producto.Nombre,
                compra.Cantidad,
                producto.Precio,
                producto.Precio * compra.Cantidad);
        }));
    }

    [ObservableProperty]
    public partial ObservableCollection<CompraDbItem> Compras { get; set; }
}

public sealed record CompraDbItem(
    int Id,
    string Cliente,
    string Producto,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Total);
