using CatalogoApi.Domain.Dtos;
using CatalogoApi.Domain.Entities;
using CatalogoApi.Domain.Exceptions;
using CatalogoApi.Domain.Repositories;
using CatalogoApi.Domain.Services;

namespace CatalogoApi.Service;

/// <summary>
/// Casos de uso de produto.
///
/// Implementa IProdutoService (o contrato que mora no Domain) e depende do CONTRATO
/// IProdutoRepository, nunca de ProdutoRepository.
/// Consequência: este arquivo não muda se o armazenamento virar SQL, arquivo ou outra coisa,
/// e pode ser testado com um repositório falso, sem subir a API.
/// </summary>
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtos;

    public ProdutoService(IProdutoRepository produtos) => _produtos = produtos;

    public Pagina<ProdutoResponse> Listar(int pagina, int tamanho, bool somenteAtivos)
    {
        // Consulta genérica (vem de IRepository<T>) ou específica (vem de IProdutoRepository).
        var produtos = somenteAtivos ? _produtos.ObterAtivos() : _produtos.ObterTodos();

        // Método genérico em ação: o mesmo Paginar serve para Categoria.
        return ServiceHelpers.Paginar(produtos.Select(ParaResponse), pagina, tamanho);
    }

    public ProdutoResponse Obter(Guid id)
        => ParaResponse(_produtos.ObterOuFalhar(id));

    public ProdutoResponse Criar(CriarProdutoRequest request)
    {
        if (_produtos.ExisteComNome(request.Nome))
            throw new DomainException($"Já existe um produto chamado '{request.Nome.Trim()}'.");

        var produto = new Produto(request.Nome, request.Preco);   // a validação de formato mora na entidade
        _produtos.Adicionar(produto);
        return ParaResponse(produto);
    }

    public ProdutoResponse AtualizarPreco(Guid id, AtualizarPrecoRequest request)
    {
        var produto = _produtos.ObterOuFalhar(id);
        produto.AtualizarPreco(request.Preco);
        _produtos.Atualizar(produto);
        return ParaResponse(produto);
    }

    public ProdutoResponse Desativar(Guid id)
    {
        var produto = _produtos.ObterOuFalhar(id);
        produto.Desativar();
        _produtos.Atualizar(produto);
        return ParaResponse(produto);
    }

    public void Remover(Guid id)
    {
        _produtos.ObterOuFalhar(id);   // garante 404 se não existir
        _produtos.Remover(id);
    }

    private static ProdutoResponse ParaResponse(Produto p)
        => new(p.Id, p.Nome, p.Preco, p.Ativo);
}
