using Microsoft.EntityFrameworkCore;
using PedidosApi.Antes.Models;

namespace PedidosApi.Antes;

// PROBLEMA: a string de conexão está fixa no código e o contexto
// é criado com "new" em cada action. Ninguém consegue trocar o banco
// (nem para um teste) sem editar este arquivo e os controllers.
public class VendasDbContext : DbContext
{
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=PedidosAntes;Trusted_Connection=True;");
    }
}
