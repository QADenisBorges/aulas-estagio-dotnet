namespace CatalogoApi.Domain.Dtos;

public record CriarCategoriaRequest(string Nome, string? Descricao);
public record RenomearCategoriaRequest(string Nome);
public record CategoriaResponse(Guid Id, string Nome, string Descricao);
