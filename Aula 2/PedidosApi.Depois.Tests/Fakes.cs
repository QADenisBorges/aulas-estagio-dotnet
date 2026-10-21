using PedidosApi.Depois.Application.Pedidos;
using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Tests;

/// <summary>Implementação falsa do contrato: uma lista em memória. Sem banco.</summary>
public class FakePedidoRepository : IPedidoRepository
{
    public List<Pedido> Pedidos { get; } = new();
    public int VezesSalvo { get; private set; }

    public Task<Pedido?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(Pedidos.FirstOrDefault(p => p.Id == id));

    public Task AdicionarAsync(Pedido pedido)
    {
        Pedidos.Add(pedido);
        return Task.CompletedTask;
    }

    public Task SalvarAsync()
    {
        VezesSalvo++;
        return Task.CompletedTask;
    }
}

/// <summary>Não envia nada: só registra que foi chamado.</summary>
public class FakeNotificador : INotificadorPedido
{
    public List<Guid> Notificados { get; } = new();

    public Task NotificarConfirmacaoAsync(Pedido pedido)
    {
        Notificados.Add(pedido.Id);
        return Task.CompletedTask;
    }
}
