using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Repositories;

namespace CatalogoApi.Repository;

/// <summary>
/// Repositório de produto.
///
/// Herda o CRUD inteiro de Repository&lt;Produto&gt; (zero linhas de ObterTodos, Adicionar, etc.)
/// e implementa só o que IProdutoRepository acrescenta.
/// </summary>
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public IReadOnlyList<Produto> ObterAtivos()
        => Consultar(p => p.Ativo);

    public bool ExisteComNome(string nome)
        => Consultar(p => string.Equals(p.Nome, nome.Trim(), StringComparison.OrdinalIgnoreCase)).Count > 0;
}
