namespace ProdutosApi.Models;

public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public string Categoria { get; set; } = string.Empty;

    public bool Ativo { get; set; }

    public decimal CustoInterno { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAlteracao { get; set; }
}
