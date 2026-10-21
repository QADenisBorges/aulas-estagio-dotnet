using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.Dtos;

public class FiltroDeProdutos
{
    public string? Nome { get; set; }

    public string? Categoria { get; set; }

    public bool? Ativo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A página deve ser maior ou igual a 1.")]
    public int Pagina { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "O tamanho da página deve estar entre 1 e 100.")]
    public int TamanhoPagina { get; set; } = 10;
}
