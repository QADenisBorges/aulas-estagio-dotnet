namespace PedidosApi.Antes.Models;

public class ItemPedido
{
    public Guid Id { get; set; }
    public string Produto { get; set; } = "";
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public Guid PedidoId { get; set; }
}
