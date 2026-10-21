using CatalogoApi.Domain.Entities;

namespace CatalogoApi.Domain.Repositories;

/// <summary>
/// Contrato ESPECÍFICO de produto.
///
/// Herda as cinco operações de IRepository&lt;Produto&gt; (não precisa repetir nenhuma)
/// e acrescenta só o que faz sentido para produto. É aqui que entra a resposta
/// da Verificação 6: consultas específicas moram no contrato específico.
/// </summary>
public interface IProdutoRepository : IRepository<Produto>
{
    IReadOnlyList<Produto> ObterAtivos();
    bool ExisteComNome(string nome);
}
