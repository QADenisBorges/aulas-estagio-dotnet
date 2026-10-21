using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Domain.Repositories;

/// <summary>
/// O CONTRATO genérico de armazenamento. Escrito uma vez, vale para qualquer entidade.
///
/// Diz O QUE existe (consultar, incluir, atualizar, remover).
/// Não diz COMO (lista, SQL, arquivo...). Quem implementa decide.
///
/// A restrição "where T : Entity" aqui não é obrigatória (a interface não usa .Id),
/// mas deixa o contrato explícito: este repositório só guarda entidades.
/// </summary>
public interface IRepository<T> where T : Entity
{
    IReadOnlyList<T> ObterTodos();
    T? ObterPorId(Guid id);
    void Adicionar(T entidade);
    void Atualizar(T entidade);
    void Remover(Guid id);
}
