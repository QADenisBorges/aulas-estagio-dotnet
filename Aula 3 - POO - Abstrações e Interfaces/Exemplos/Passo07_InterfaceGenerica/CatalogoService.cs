using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo07_InterfaceGenerica;

/// <summary>
/// Um service que depende do CONTRATO IRepository&lt;Produto&gt;,
/// e não da classe RepositorioEmMemoria. Ele não sabe onde os produtos ficam.
/// </summary>
public class CatalogoService
{
    private readonly IRepository<Produto> _produtos;

    public CatalogoService(IRepository<Produto> produtos) => _produtos = produtos;

    public Produto Cadastrar(string nome, decimal preco)
    {
        if (preco < 0) throw new ArgumentException("Preço não pode ser negativo.");
        var produto = new Produto(nome, preco);
        _produtos.Adicionar(produto);
        return produto;
    }

    public decimal ValorTotalDoEstoque() => _produtos.ObterTodos().Sum(p => p.Preco);
}
