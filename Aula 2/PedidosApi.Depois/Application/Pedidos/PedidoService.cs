using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Application.Pedidos;

/// <summary>
/// Casos de uso de pedido. Orquestra domínio, repositório e notificação.
/// Não sabe o que é HTTP, SQL ou SMTP: só conhece os contratos.
/// </summary>
public class PedidoService
{
    private readonly IPedidoRepository _repositorio;
    private readonly INotificadorPedido _notificador;

    public PedidoService(IPedidoRepository repositorio, INotificadorPedido notificador)
    {
        _repositorio = repositorio;
        _notificador = notificador;
    }

    public async Task<PedidoResponse> CriarAsync(CriarPedidoRequest request)
    {
        var pedido = new Pedido(request.ClienteEmail);

        foreach (var item in request.Itens)
            pedido.AdicionarItem(item.Produto, item.Quantidade, new Dinheiro(item.PrecoUnitario));

        await _repositorio.AdicionarAsync(pedido);
        await _repositorio.SalvarAsync();

        return ParaResponse(pedido);
    }

    public async Task<PedidoResponse?> ObterAsync(Guid id)
    {
        var pedido = await _repositorio.ObterPorIdAsync(id);
        return pedido is null ? null : ParaResponse(pedido);
    }

    public async Task<PedidoResponse?> ConfirmarAsync(Guid id)
    {
        var pedido = await _repositorio.ObterPorIdAsync(id);
        if (pedido is null) return null;

        pedido.Confirmar();               // a regra mora no domínio
        await _repositorio.SalvarAsync();
        await _notificador.NotificarConfirmacaoAsync(pedido);

        return ParaResponse(pedido);
    }

    private static PedidoResponse ParaResponse(Pedido pedido) => new(
        pedido.Id,
        pedido.ClienteEmail,
        pedido.Status.ToString(),
        pedido.Total.Valor,
        pedido.Itens
            .Select(i => new ItemResponse(i.Produto, i.Quantidade, i.PrecoUnitario.Valor, i.Subtotal.Valor))
            .ToList());
}
