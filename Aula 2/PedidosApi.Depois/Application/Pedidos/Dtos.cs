namespace PedidosApi.Depois.Application.Pedidos;

// DTOs: o que entra e sai da API. A entidade do domínio não vaza para fora.

public record CriarPedidoRequest(string ClienteEmail, List<ItemRequest> Itens);

public record ItemRequest(string Produto, int Quantidade, decimal PrecoUnitario);

public record PedidoResponse(
    Guid Id,
    string ClienteEmail,
    string Status,
    decimal Total,
    List<ItemResponse> Itens);

public record ItemResponse(string Produto, int Quantidade, decimal PrecoUnitario, decimal Subtotal);
