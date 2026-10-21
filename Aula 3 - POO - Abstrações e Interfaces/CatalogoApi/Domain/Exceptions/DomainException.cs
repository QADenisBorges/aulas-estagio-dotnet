namespace CatalogoApi.Domain.Exceptions;

/// <summary>
/// Regra do domínio violada (nome vazio, preço negativo...). Vira 400 na API.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem) { }
}
