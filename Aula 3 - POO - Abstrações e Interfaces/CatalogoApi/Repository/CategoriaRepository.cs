using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Repositories;

namespace CatalogoApi.Repository;

/// <summary>
/// Repositório de categoria. A PROVA de reutilização: todo o CRUD vem da base genérica;
/// aqui só existe a consulta que é exclusiva de categoria.
/// </summary>
public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public Categoria? ObterPorNome(string nome)
        => Consultar(c => string.Equals(c.Nome, nome.Trim(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
}
