using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Repositories;

namespace CatalogoApi.Repository;

/// <summary>
/// Implementação genérica em memória do contrato IRepository&lt;T&gt;.
/// É a CLASSE-BASE dos repositórios específicos: ProdutoRepository, CategoriaRepository...
/// Eles herdam todo o CRUD daqui e só escrevem as consultas próprias.
///
/// "where T : Entity" é NECESSÁRIO aqui: sem ele, o compilador não sabe que T tem Id
/// e as linhas com x.Id não compilam (CS1061). Com ele, ganhamos duas coisas:
///   1) podemos usar .Id dentro do código genérico;
///   2) ninguém consegue criar Repository&lt;string&gt; ou Repository&lt;int&gt;.
///
/// abstract: ninguém usa Repository&lt;T&gt; direto; sempre pela classe específica.
/// </summary>
public abstract class Repository<T> : IRepository<T> where T : Entity
{
    private readonly List<T> _itens = new();
    private readonly object _trava = new();   // a API atende várias requisições ao mesmo tempo

    public IReadOnlyList<T> ObterTodos()
    {
        lock (_trava)
            return _itens.ToList();            // cópia: quem recebe não altera a lista interna
    }

    public T? ObterPorId(Guid id)
    {
        lock (_trava)
            return _itens.FirstOrDefault(x => x.Id == id);
    }

    public void Adicionar(T entidade)
    {
        lock (_trava)
        {
            if (_itens.Any(x => x.Id == entidade.Id))
                throw new InvalidOperationException($"Já existe {typeof(T).Name} com Id {entidade.Id}.");

            _itens.Add(entidade);
        }
    }

    public void Atualizar(T entidade)
    {
        // Como a lista guarda referências, alterar o objeto já alterou a lista.
        // O método existe para honrar o contrato (um repositório SQL precisa dele)
        // e para o caso de receber um objeto que ainda não está na lista.
        lock (_trava)
        {
            var indice = _itens.FindIndex(x => x.Id == entidade.Id);
            if (indice < 0)
                throw new InvalidOperationException($"{typeof(T).Name} com Id {entidade.Id} não existe para atualizar.");

            _itens[indice] = entidade;
        }
    }

    public void Remover(Guid id)
    {
        lock (_trava)
            _itens.RemoveAll(x => x.Id == id);
    }

    /// <summary>
    /// Ponto de extensão para as classes filhas: consulta com filtro, já protegida pela trava.
    /// protected: só os repositórios específicos enxergam; o service nunca vê a lista.
    /// </summary>
    protected IReadOnlyList<T> Consultar(Func<T, bool> filtro)
    {
        lock (_trava)
            return _itens.Where(filtro).ToList();
    }
}
