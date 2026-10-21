namespace CatalogoApi.Domain.Exceptions;

/// <summary>
/// Entidade pedida não existe. Vira 404 na API.
/// </summary>
public class NaoEncontradoException : Exception
{
    public NaoEncontradoException(string mensagem) : base(mensagem) { }
}
