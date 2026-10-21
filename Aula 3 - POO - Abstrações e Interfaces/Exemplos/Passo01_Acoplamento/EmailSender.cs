namespace Exemplos.Passo01_Acoplamento;

/// <summary>Detalhe técnico: sabe servidor e porta. Quem usa isto diretamente fica acoplado a e-mail.</summary>
public class EmailSender
{
    private readonly string _servidor;
    private readonly int _porta;

    public EmailSender(string servidor, int porta)
    {
        _servidor = servidor;
        _porta = porta;
    }

    public void Enviar(string destinatario, string mensagem)
        => Console.WriteLine($"  [EMAIL via {_servidor}:{_porta}] para {destinatario}: {mensagem}");
}
