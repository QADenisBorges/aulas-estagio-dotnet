namespace Exemplos.Passo01_Acoplamento;

/// <summary>O CONTRATO: diz O QUE precisa existir, não COMO.</summary>
public interface INotificador
{
    void Enviar(string destinatario, string mensagem);
}
