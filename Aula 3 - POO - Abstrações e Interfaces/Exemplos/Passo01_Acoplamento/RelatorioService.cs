namespace Exemplos.Passo01_Acoplamento;

/// <summary>
/// VERSÃO B: desacoplada. O service RECEBE a dependência e só conhece o contrato.
/// Quem decide se é e-mail ou console é quem monta o objeto, lá fora.
/// </summary>
public class RelatorioService
{
    private readonly INotificador _notificador;

    public RelatorioService(INotificador notificador)
        => _notificador = notificador;

    public void Fechar()
        => _notificador.Enviar("chefe@empresa.com", "Relatório fechado");
}
