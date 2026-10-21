namespace PedidosApi.Depois.Domain.Pedidos;

/// <summary>
/// Entidade interna do agregado Pedido. Só existe dentro de um pedido,
/// por isso não tem repositório próprio.
/// </summary>
public class ItemPedido
{
    public Guid Id { get; private set; }
    public string Produto { get; private set; } = "";
    public int Quantidade { get; private set; }
    public Dinheiro PrecoUnitario { get; private set; } = Dinheiro.Zero();

    private ItemPedido() { } // EF Core

    internal ItemPedido(string produto, int quantidade, Dinheiro precoUnitario)
    {
        if (string.IsNullOrWhiteSpace(produto))
            throw new DomainException("Produto é obrigatório.");
        if (quantidade <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");
        if (precoUnitario.Valor < 0)
            throw new DomainException("Preço não pode ser negativo.");

        Id = Guid.NewGuid();
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    public Dinheiro Subtotal => PrecoUnitario.Multiplicar(Quantidade);
}
