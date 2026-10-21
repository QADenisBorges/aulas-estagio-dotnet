using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Domain.Repositories;

/// <summary>
/// Contrato específico de categoria: tudo de IRepository&lt;Categoria&gt; mais uma consulta própria.
/// </summary>
public interface ICategoriaRepository : IRepository<Categoria>
{
    Categoria? ObterPorNome(string nome);
}
