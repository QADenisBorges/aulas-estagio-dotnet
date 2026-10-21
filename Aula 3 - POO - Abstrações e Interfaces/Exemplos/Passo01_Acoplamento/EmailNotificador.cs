namespace Exemplos.Passo01_Acoplamento;

public class EmailNotificador : INotificador
{
    public void Enviar(string destinatario, string mensagem)
        => Console.WriteLine($"  [EMAIL] para {destinatario}: {mensagem}");
}
