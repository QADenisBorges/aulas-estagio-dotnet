using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Application.Pedidos;

/// <summary>
/// A aplicação precisa "avisar o cliente". Como (e-mail, SMS, fila)
/// é detalhe de infraestrutura.
/// </summary>
public interface INotificadorPedido
{
    Task NotificarConfirmacaoAsync(Pedido pedido);
}
