// =====================================================================
// SEM restrição: NÃO COMPILA. Descomente o bloco para ver o erro:
//   CS1061: 'T' não contém uma definição para 'Id'
// Faz sentido: T pode ser int, string, qualquer coisa. O compilador não
// tem como saber que T tem Id.
// =====================================================================

// namespace Exemplos.Passo06_Restricoes;
//
// public class RepositorioSemRestricao<T>
// {
//     private readonly List<T> _itens = new();
//     public T? ObterPorId(Guid id) => _itens.FirstOrDefault(x => x.Id == id);
// }
