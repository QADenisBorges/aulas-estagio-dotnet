namespace CatalogoApi.Domain.Dtos;

// DTOs: o que entra e o que sai dos casos de uso de produto.
// A entidade nunca é exposta crua: assim ela pode mudar sem quebrar a resposta HTTP.
// Ficam no Domain porque os contratos dos services (IProdutoService) os usam nas assinaturas.

public record CriarProdutoRequest(string Nome, decimal Preco);
public record AtualizarPrecoRequest(decimal Preco);
public record ProdutoResponse(Guid Id, string Nome, decimal Preco, bool Ativo);
