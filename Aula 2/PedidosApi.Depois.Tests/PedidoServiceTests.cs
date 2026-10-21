using PedidosApi.Depois.Application.Pedidos;
using PedidosApi.Depois.Domain.Pedidos;
using Xunit;

namespace PedidosApi.Depois.Tests;

public class PedidoServiceTests
{
    private readonly FakePedidoRepository _repo = new();
    private readonly FakeNotificador _notificador = new();
    private readonly PedidoService _service;

    public PedidoServiceTests()
    {
        // Mesma classe de produção, outras implementações dos contratos.
        _service = new PedidoService(_repo, _notificador);
    }

    [Fact]
    public async Task Confirmar_PedidoSemItens_DeveFalhar()
    {
        var pedido = new Pedido("cliente@exemplo.com");
        _repo.Pedidos.Add(pedido);

        await Assert.ThrowsAsync<DomainException>(() => _service.ConfirmarAsync(pedido.Id));
        Assert.Empty(_notificador.Notificados);
    }

    [Fact]
    public async Task Confirmar_PedidoComItens_DeveConfirmarENotificar()
    {
        var pedido = new Pedido("cliente@exemplo.com");
        pedido.AdicionarItem("Teclado", 2, new Dinheiro(100));
        _repo.Pedidos.Add(pedido);

        var resposta = await _service.ConfirmarAsync(pedido.Id);

        Assert.Equal("Confirmado", resposta!.Status);
        Assert.Equal(200m, resposta.Total);
        Assert.Equal(1, _repo.VezesSalvo);
        Assert.Contains(pedido.Id, _notificador.Notificados);
    }

    [Fact]
    public async Task Confirmar_PedidoInexistente_DeveRetornarNulo()
    {
        var resposta = await _service.ConfirmarAsync(Guid.NewGuid());
        Assert.Null(resposta);
    }

    [Fact]
    public void Pedido_Confirmado_NaoAceitaNovosItens()
    {
        var pedido = new Pedido("cliente@exemplo.com");
        pedido.AdicionarItem("Mouse", 1, new Dinheiro(50));
        pedido.Confirmar();

        Assert.Throws<DomainException>(() => pedido.AdicionarItem("Cabo", 1, new Dinheiro(10)));
    }
}
