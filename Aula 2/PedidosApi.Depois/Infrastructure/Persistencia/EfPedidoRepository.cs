using Microsoft.EntityFrameworkCore;
using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Infrastructure.Persistencia;

/// <summary>Implementação do contrato com EF Core. Único lugar que conhece o DbContext.</summary>
public class EfPedidoRepository : IPedidoRepository
{
    private readonly VendasDbContext _db;

    public EfPedidoRepository(VendasDbContext db) => _db = db;

    public Task<Pedido?> ObterPorIdAsync(Guid id) =>
        _db.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Pedido pedido) =>
        await _db.Pedidos.AddAsync(pedido);

    public Task SalvarAsync() => _db.SaveChangesAsync();
}
