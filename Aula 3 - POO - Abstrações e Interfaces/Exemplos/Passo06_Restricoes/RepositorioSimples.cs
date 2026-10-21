using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo06_Restricoes;

/// <summary>
/// COM restrição: "T só pode ser Entity ou alguém que herda de Entity."
/// Agora o compilador SABE que todo T tem Id.
/// </summary>
public class RepositorioSimples<T> where T : Entity
{
    private readonly List<T> _itens = new();

    public void Adicionar(T item) => _itens.Add(item);

    public T? ObterPorId(Guid id) => _itens.FirstOrDefault(x => x.Id == id);   // x.Id funciona

    public int Quantidade => _itens.Count;
}
