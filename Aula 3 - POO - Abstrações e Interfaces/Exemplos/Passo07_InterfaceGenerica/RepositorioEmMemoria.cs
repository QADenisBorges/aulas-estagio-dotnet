using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo07_InterfaceGenerica;

/// <summary>UMA implementação em memória, também genérica.</summary>
public class RepositorioEmMemoria<T> : IRepository<T> where T : Entity
{
    private readonly List<T> _itens = new();

    public IReadOnlyList<T> ObterTodos() => _itens.AsReadOnly();
    public T? ObterPorId(Guid id) => _itens.FirstOrDefault(x => x.Id == id);
    public void Adicionar(T entidade) => _itens.Add(entidade);
    public void Remover(Guid id) => _itens.RemoveAll(x => x.Id == id);
}
