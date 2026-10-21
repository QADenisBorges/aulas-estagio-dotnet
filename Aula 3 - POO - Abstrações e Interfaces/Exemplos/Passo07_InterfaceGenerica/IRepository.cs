using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo07_InterfaceGenerica;

/// <summary>O CONTRATO genérico. Escrito uma vez, serve para qualquer entidade.</summary>
public interface IRepository<T> where T : Entity
{
    IReadOnlyList<T> ObterTodos();
    T? ObterPorId(Guid id);
    void Adicionar(T entidade);
    void Remover(Guid id);
}
