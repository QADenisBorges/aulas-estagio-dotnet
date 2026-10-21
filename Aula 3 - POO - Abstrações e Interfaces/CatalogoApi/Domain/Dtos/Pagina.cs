namespace CatalogoApi.Domain.Dtos;

/// <summary>
/// Uma página de QUALQUER coisa. Record genérico: Pagina&lt;ProdutoResponse&gt;, Pagina&lt;CategoriaResponse&gt;...
/// </summary>
public record Pagina<T>(IReadOnlyList<T> Itens, int PaginaAtual, int TamanhoPagina, int Total)
{
    public int TotalPaginas => Total == 0 ? 0 : (int)Math.Ceiling(Total / (double)TamanhoPagina);
}
