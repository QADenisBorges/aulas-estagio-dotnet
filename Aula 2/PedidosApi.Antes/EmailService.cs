using PedidosApi.Antes.Models;

namespace PedidosApi.Antes;

// PROBLEMA: o controller faz "new EmailService()" direto.
// Em um teste automatizado, isso tentaria enviar e-mail de verdade.
public class EmailService
{
    public void EnviarConfirmacao(Pedido pedido)
    {
        // Aqui iria um SmtpClient apontando para o servidor da empresa.
        Console.WriteLine($"[EMAIL] Pedido {pedido.Id} confirmado para {pedido.ClienteEmail}");
    }
}
