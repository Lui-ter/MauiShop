namespace petshop.Services;

public interface INavegacionService
{
    Task NavegarAClienteAsync(int clienteId);

    Task NavegarAComprasAsync();
}
