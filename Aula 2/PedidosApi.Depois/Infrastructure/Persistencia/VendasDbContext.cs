using Microsoft.EntityFrameworkCore;
using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Infrastructure.Persistencia;

public class VendasDbContext : DbContext
{
    public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(pedido =>
        {
            pedido.HasKey(p => p.Id);
            pedido.Property(p => p.ClienteEmail).HasMaxLength(200);
            pedido.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);
            pedido.Ignore(p => p.Total); // calculado no domínio

            // O EF grava a lista privada; o domínio continua mandando nas mudanças.
            pedido.HasMany(p => p.Itens).WithOne().OnDelete(DeleteBehavior.Cascade);
            pedido.Navigation(p => p.Itens).HasField("_itens");
        });

        modelBuilder.Entity<ItemPedido>(item =>
        {
            item.HasKey(i => i.Id);
            item.Property(i => i.Produto).HasMaxLength(200);
            item.Ignore(i => i.Subtotal);

            // Objeto de valor salvo nas colunas do próprio item.
            item.OwnsOne(i => i.PrecoUnitario, preco =>
            {
                preco.Property(d => d.Valor).HasColumnName("PrecoUnitario").HasPrecision(18, 2);
                preco.Property(d => d.Moeda).HasColumnName("Moeda").HasMaxLength(3);
            });
        });
    }
}
