namespace PedidosApi.Depois.Domain.Pedidos;

/// <summary>
/// Raiz do agregado. Toda mudança nos itens e no status passa por aqui,
/// então as regras do negócio ficam em um único lugar.
/// </summary>
public class Pedido
{
    private readonly List<ItemPedido> _itens = new();

    public Guid Id { get; private set; }
    public string ClienteEmail { get; private set; } = "";
    public StatusPedido Status { get; private set; }
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    private Pedido() { } // EF Core

    public Pedido(string clienteEmail)
    {
        if (string.IsNullOrWhiteSpace(clienteEmail) || !clienteEmail.Contains('@'))
            throw new DomainException("E-mail do cliente inválido.");

        Id = Guid.NewGuid();
        ClienteEmail = clienteEmail;
        Status = StatusPedido.Rascunho;
    }

    public Dinheiro Total =>
        _itens.Aggregate(Dinheiro.Zero(), (acc, item) => acc.Somar(item.Subtotal));

    public void AdicionarItem(string produto, int quantidade, Dinheiro precoUnitario)
    {
        GarantirQueAindaPodeMudar();
        _itens.Add(new ItemPedido(produto, quantidade, precoUnitario));
    }

    public void Confirmar()
    {
        GarantirQueAindaPodeMudar();
        if (_itens.Count == 0)
            throw new DomainException("Pedido sem itens não pode ser confirmado.");

        Status = StatusPedido.Confirmado;
    }

    private void GarantirQueAindaPodeMudar()
    {
        if (Status == StatusPedido.Confirmado)
            throw new DomainException("Pedido já confirmado não pode ser alterado.");
    }
}
