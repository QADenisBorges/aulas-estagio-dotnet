namespace ProdutosApi.Dtos;

public class RespostaPaginada<T>
{
    public IReadOnlyList<T> Itens { get; set; } = [];

    public int Pagina { get; set; }

    public int TamanhoPagina { get; set; }

    public int TotalDeItens { get; set; }

    public int TotalDePaginas { get; set; }
}
