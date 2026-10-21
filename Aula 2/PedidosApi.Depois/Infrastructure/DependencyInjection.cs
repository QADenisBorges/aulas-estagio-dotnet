using Microsoft.EntityFrameworkCore;
using PedidosApi.Depois.Application.Pedidos;
using PedidosApi.Depois.Domain.Pedidos;
using PedidosApi.Depois.Infrastructure.Notificacoes;
using PedidosApi.Depois.Infrastructure.Persistencia;

namespace PedidosApi.Depois.Infrastructure;

/// <summary>
/// Composição: aqui, e só aqui, os contratos são ligados às implementações.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Vendas");

        services.AddDbContext<VendasDbContext>(options =>
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                options.UseInMemoryDatabase("Vendas");   // sem banco configurado: roda em memória
            else
                options.UseSqlServer(connectionString);
        });

        services.AddScoped<IPedidoRepository, EfPedidoRepository>();
        services.AddScoped<INotificadorPedido, EmailNotificadorPedido>();

        return services;
    }

    public static IServiceCollection AddAplicacao(this IServiceCollection services)
    {
        services.AddScoped<PedidoService>();
        return services;
    }
}
