using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Dtos;
using ProdutosApi.Interface;
using ProdutosApi.Models;

namespace ProdutosApi.Controllers;

[ApiController]
[Route("produtos")]
[Produces("application/json")]
[Tags("Produtos")]
public class ProdutosController : ControllerBase
{
    public IMapper Mapper { get; set; }
    public IProdutoRepository ProdutoRepository { get; set; }
    public ProdutosController(IMapper mapper, IProdutoRepository produtoRepository)
    {
        Mapper = mapper;
        produtoRepository
    }

    private static readonly Lock Trava = new();

    private static int _proximoId = 5;

    /// <summary>Lista produtos, com filtros opcionais e paginação.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(RespostaPaginada<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<RespostaPaginada<ProdutoResponse>> Listar([FromQuery] FiltroDeProdutos filtro)
    {
        lock (Trava)
        {
            IEnumerable<Produto> consulta = Produtos;

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                consulta = consulta.Where(p => p.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Categoria))
            {
                consulta = consulta.Where(p => string.Equals(p.Categoria, filtro.Categoria, StringComparison.OrdinalIgnoreCase));
            }

            if (filtro.Ativo is not null)
            {
                consulta = consulta.Where(p => p.Ativo == filtro.Ativo);
            }

            var encontrados = consulta.OrderBy(p => p.Id).ToList();

            var itens = encontrados
                .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
                .Take(filtro.TamanhoPagina)
                .Select(ParaResposta)
                .ToList();

            return Ok(new RespostaPaginada<ProdutoResponse>
            {
                Itens = itens,
                Pagina = filtro.Pagina,
                TamanhoPagina = filtro.TamanhoPagina,
                TotalDeItens = encontrados.Count,
                TotalDePaginas = (int)Math.Ceiling(encontrados.Count / (double)filtro.TamanhoPagina),
            });
        }
    }

    /// <summary>Busca um produto pelo Id.</summary>
    [HttpGet("{id:int}", Name = nameof(ObterPorId))]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<ProdutoResponse> ObterPorId(int id)
    {
        lock (Trava)
        {
            var produto = 

            return produto is null
                ? NaoEncontrado(id)
                : Ok(Mapper.Map<ProdutoResponse>(produto));
        }
    }

    /// <summary>Cria um produto.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ProdutoResponse> Criar([FromBody] CriarProdutoRequest requisicao)
    {
        lock (Trava)
        {
            if (Produtos.Any(p => string.Equals(p.Nome, requisicao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                return RegraDeNegocio($"Já existe um produto chamado '{requisicao.Nome}'.");
            }

            var produto = new Produto
            {
                Id = _proximoId++,
                Nome = requisicao.Nome.Trim(),
                Preco = requisicao.Preco,
                Categoria = requisicao.Categoria.Trim().ToLowerInvariant(),
                Ativo = true,
                CustoInterno = 0m,
                DataCriacao = DateTime.UtcNow,
            };

            

            return CreatedAtRoute(nameof(ObterPorId), new { id = produto.Id }, ParaResposta(produto));
        }
    }

    /// <summary>Substitui um produto inteiro.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<ProdutoResponse> Substituir(int id, [FromBody] SubstituirProdutoRequest requisicao)
    {
        lock (Trava)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NaoEncontrado(id);
            }

            if (Produtos.Any(p => p.Id != id && string.Equals(p.Nome, requisicao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                return RegraDeNegocio($"Já existe outro produto chamado '{requisicao.Nome}'.");
            }

            produto.Nome = requisicao.Nome.Trim();
            produto.Preco = requisicao.Preco;
            produto.Categoria = requisicao.Categoria.Trim().ToLowerInvariant();
            produto.Ativo = requisicao.Ativo;
            produto.DataAlteracao = DateTime.UtcNow;

            return Ok(ParaResposta(produto));
        }
    }

    /// <summary>Atualiza apenas os campos enviados.</summary>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public ActionResult<ProdutoResponse> AtualizarParcial(int id, [FromBody] AtualizarProdutoRequest requisicao)
    {
        lock (Trava)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NaoEncontrado(id);
            }

            if (requisicao.Nome is not null &&
                Produtos.Any(p => p.Id != id && string.Equals(p.Nome, requisicao.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                return RegraDeNegocio($"Já existe outro produto chamado '{requisicao.Nome}'.");
            }

            

            return Ok(ParaResposta(produto));
        }
    }

    /// <summary>Remove um produto.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Remover(int id)
    {
        lock (Trava)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NaoEncontrado(id);
            }

            Produtos.Remove(produto);
            return NoContent();
        }
    }

    private ObjectResult NaoEncontrado(int id) => Problem(
        title: "Recurso não encontrado.",
        detail: $"Produto {id} não encontrado.",
        statusCode: StatusCodes.Status404NotFound);

    private ObjectResult RegraDeNegocio(string mensagem) => Problem(
        title: "Requisição inválida.",
        detail: mensagem,
        statusCode: StatusCodes.Status400BadRequest);

    private static ProdutoResponse ParaResposta(Produto produto) => new()
    {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria,
        Ativo = produto.Ativo,
        DataCriacao = produto.DataCriacao,
        DataAlteracao = produto.DataAlteracao,
    };
}
