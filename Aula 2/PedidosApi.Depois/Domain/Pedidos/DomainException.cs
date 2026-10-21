namespace PedidosApi.Depois.Domain.Pedidos;

/// <summary>Uma regra de negócio foi violada.</summary>
public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem) { }
}
