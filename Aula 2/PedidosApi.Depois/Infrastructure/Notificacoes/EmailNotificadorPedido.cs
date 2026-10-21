using PedidosApi.Depois.Application.Pedidos;
using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Infrastructure.Notificacoes;

/// <summary>Implementação por e-mail. Nos testes, uma implementação falsa toma o lugar desta.</summary>
public class EmailNotificadorPedido : INotificadorPedido
{
    private readonly ILogger<EmailNotificadorPedido> _logger;

    public EmailNotificadorPedido(ILogger<EmailNotificadorPedido> logger) => _logger = logger;

    public Task NotificarConfirmacaoAsync(Pedido pedido)
    {
        // Aqui entraria o SmtpClient / serviço de e-mail de verdade.
        _logger.LogInformation("[EMAIL] Pedido {PedidoId} confirmado para {Email}", pedido.Id, pedido.ClienteEmail);
        return Task.CompletedTask;
    }
}
