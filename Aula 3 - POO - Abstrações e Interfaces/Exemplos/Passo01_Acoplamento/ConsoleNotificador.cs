namespace Exemplos.Passo01_Acoplamento;

public class ConsoleNotificador : INotificador
{
    public void Enviar(string destinatario, string mensagem)
        => Console.WriteLine($"  [CONSOLE] ({destinatario}) {mensagem}");
}
