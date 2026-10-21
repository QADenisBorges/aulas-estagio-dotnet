using CatalogoApi.Domain.Dtos;

namespace CatalogoApi.Domain.Services;

/// <summary>
/// Contrato dos casos de uso de categoria.
/// </summary>
public interface ICategoriaService
{
    Pagina<CategoriaResponse> Listar(int pagina, int tamanho);
    CategoriaResponse Obter(Guid id);
    CategoriaResponse Criar(CriarCategoriaRequest request);
    CategoriaResponse Renomear(Guid id, RenomearCategoriaRequest request);
    void Remover(Guid id);
}
