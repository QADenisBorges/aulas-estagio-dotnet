namespace PedidosApi.Depois.Domain.Pedidos;

/// <summary>
/// Contrato de acesso a pedidos, escrito na língua do domínio.
/// Quem implementa decide se é EF Core, Dapper, memória...
/// </summary>
public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(Pedido pedido);
    Task SalvarAsync();
}
