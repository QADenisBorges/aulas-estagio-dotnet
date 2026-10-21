namespace PedidosApi.Antes.Models;

// PROBLEMA: modelo "anêmico". Só carrega dados.
// Status é string livre ("Rascunho", "Confirmado", "confirmado", "CONFIRMADO"...).
// Qualquer código pode alterar Itens, Total e Status sem respeitar regra nenhuma.
public class Pedido
{
    public Guid Id { get; set; }
    public string ClienteEmail { get; set; } = "";
    public string Status { get; set; } = "";
    public decimal Total { get; set; }
    public List<ItemPedido> Itens { get; set; } = new();
}
