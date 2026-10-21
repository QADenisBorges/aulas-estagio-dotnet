using CatalogoApi.Domain.Dtos;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Domain.Repositories;
using CatalogoApi.Domain.Services;

namespace CatalogoApi.Service;

/// <summary>
/// Casos de uso de categoria. Repare: depende de ICategoriaRepository e usa os mesmos
/// métodos genéricos (Paginar, ObterOuFalhar). O CRUD veio todo da base genérica.
/// </summary>
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categorias;

    public CategoriaService(ICategoriaRepository categorias) => _categorias = categorias;

    public Pagina<CategoriaResponse> Listar(int pagina, int tamanho)
        => ServiceHelpers.Paginar(_categorias.ObterTodos().Select(ParaResponse), pagina, tamanho);

    public CategoriaResponse Obter(Guid id)
        => ParaResponse(_categorias.ObterOuFalhar(id));

    public CategoriaResponse Criar(CriarCategoriaRequest request)
    {
        if (_categorias.ObterPorNome(request.Nome) is not null)
            throw new DomainException($"Já existe uma categoria chamada '{request.Nome.Trim()}'.");

        var categoria = new Categoria(request.Nome, request.Descricao);
        _categorias.Adicionar(categoria);
        return ParaResponse(categoria);
    }

    public CategoriaResponse Renomear(Guid id, RenomearCategoriaRequest request)
    {
        var categoria = _categorias.ObterOuFalhar(id);
        categoria.Renomear(request.Nome);
        _categorias.Atualizar(categoria);
        return ParaResponse(categoria);
    }

    public void Remover(Guid id)
    {
        _categorias.ObterOuFalhar(id);
        _categorias.Remover(id);
    }

    private static CategoriaResponse ParaResponse(Categoria c)
        => new(c.Id, c.Nome, c.Descricao);
}
