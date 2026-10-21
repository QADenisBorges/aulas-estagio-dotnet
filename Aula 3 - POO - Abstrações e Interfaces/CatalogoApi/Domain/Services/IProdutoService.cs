using CatalogoApi.Domain.Dtos;

namespace CatalogoApi.Domain.Services;

/// <summary>
/// Contrato dos casos de uso de produto: a regra de negócio, vista de fora.
///
/// Mora no Domain porque descreve O QUE a aplicação faz com produtos, sem dizer COMO.
/// O controller depende deste contrato, nunca de ProdutoService.
/// </summary>
public interface IProdutoService
{
    Pagina<ProdutoResponse> Listar(int pagina, int tamanho, bool somenteAtivos);
    ProdutoResponse Obter(Guid id);
    ProdutoResponse Criar(CriarProdutoRequest request);
    ProdutoResponse AtualizarPreco(Guid id, AtualizarPrecoRequest request);
    ProdutoResponse Desativar(Guid id);
    void Remover(Guid id);
}
