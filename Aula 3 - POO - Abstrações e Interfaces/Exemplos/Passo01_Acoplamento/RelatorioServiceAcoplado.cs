namespace Exemplos.Passo01_Acoplamento;

/// <summary>
/// VERSÃO A: acoplada. O service CRIA a dependência por dentro.
/// Ele sabe que é e-mail, sabe o servidor, sabe a porta. Três coisas que não são da conta dele.
/// </summary>
public class RelatorioServiceAcoplado
{
    public void Fechar()
    {
        var email = new EmailSender("smtp.empresa.com", 587);   // <- acoplamento
        email.Enviar("chefe@empresa.com", "Relatório fechado");
    }
}
