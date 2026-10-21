using CatalogoApi.Domain.Dtos;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Domain.Repositories;

namespace CatalogoApi.Service;

/// <summary>
/// MÉTODOS GENÉRICOS usados por todos os services.
/// O algoritmo é o mesmo para qualquer tipo, então é escrito uma vez só.
/// </summary>
public static class ServiceHelpers
{
    /// <summary>
    /// Paginar não precisa saber NADA sobre T: só conta, pula e pega.
    /// Por isso não tem restrição nenhuma.
    /// </summary>
    public static Pagina<T> Paginar<T>(IEnumerable<T> itens, int pagina, int tamanho)
    {
        if (pagina < 1) pagina = 1;
        if (tamanho < 1) tamanho = 10;
        if (tamanho > 100) tamanho = 100;

        var lista = itens.ToList();
        var recorte = lista.Skip((pagina - 1) * tamanho).Take(tamanho).ToList();

        return new Pagina<T>(recorte, pagina, tamanho, lista.Count);
    }

    /// <summary>
    /// Busca no repositório e falha com 404 padronizado se não achar.
    /// Aqui a restrição "where T : Entity" é OBRIGATÓRIA: IRepository&lt;T&gt; exige T : Entity,
    /// então este método só pode aceitar os mesmos T que a interface aceita.
    /// </summary>
    public static T ObterOuFalhar<T>(this IRepository<T> repositorio, Guid id) where T : Entity
    {
        return repositorio.ObterPorId(id)
            ?? throw new NaoEncontradoException($"{typeof(T).Name} com Id {id} não encontrado(a).");
    }
}
