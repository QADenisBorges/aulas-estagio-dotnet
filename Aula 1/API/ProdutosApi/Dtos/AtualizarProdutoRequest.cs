using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.Dtos;

public class AtualizarProdutoRequest
{
    [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres.")]
    public string? Nome { get; set; }

    [Range(0.01, 1_000_000, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal? Preco { get; set; }

    [StringLength(40, ErrorMessage = "A categoria deve ter no máximo 40 caracteres.")]
    public string? Categoria { get; set; }

    public bool? Ativo { get; set; }
}
