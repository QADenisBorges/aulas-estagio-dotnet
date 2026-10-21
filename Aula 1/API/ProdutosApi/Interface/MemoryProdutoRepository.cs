using ProdutosApi.Models;

namespace ProdutosApi.Interface;

public class MemoryProdutoRepository : IProdutoRepository
{
    private static readonly List<Produto> Produtos = [
            new() { Id = 1, Nome = "Notebook Gamer",       Preco = 7500.00m, Categoria = "informatica", Ativo = true,  CustoInterno = 5200.00m, DataCriacao = DateTime.UtcNow },
            new() { Id = 2, Nome = "Mouse sem fio",        Preco =  149.90m, Categoria = "perifericos", Ativo = true,  CustoInterno =   70.00m, DataCriacao = DateTime.UtcNow },
            new() { Id = 3, Nome = "Teclado mecânico",     Preco =  399.00m, Categoria = "perifericos", Ativo = true,  CustoInterno =  210.00m, DataCriacao = DateTime.UtcNow },
            new() { Id = 4, Nome = "Monitor 27 polegadas", Preco = 1899.00m, Categoria = "informatica", Ativo = false, CustoInterno = 1300.00m, DataCriacao = DateTime.UtcNow },
    ];

    public Produto Get(int id)
    {
        return Produtos.FirstOrDefault(p => p.Id == id);
    }

    public void Insert(Produto produto)
    {
        Produtos.Add(produto);
    }

    public void Update(Produto produto)
    {
        var produtoBanco = Produtos.FirstOrDefault(p => p.Id == id);

        produtoBanco.Nome = produto.Nome;
        produtoBanco.Preco = produto.Preco;
        produtoBanco.Categoria = produto.Categoria;
        produtoBanco.Ativo = produto.Ativo;
        produtoBanco.DataAlteracao = DateTime.UtcNow;
    }
}
