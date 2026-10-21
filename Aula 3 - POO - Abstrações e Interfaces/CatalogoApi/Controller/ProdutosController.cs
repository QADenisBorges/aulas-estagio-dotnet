using CatalogoApi.Domain.Dtos;
using CatalogoApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controller;

/// <summary>
/// Controller fino: traduz HTTP para o contrato IProdutoService e o resultado para status code.
/// Nenhuma regra de negócio, nenhum acesso a armazenamento.
/// </summary>
[ApiController]
[Route("api/produtos")]
[Produces("application/json")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service) => _service = service;

    /// GET api/produtos?pagina=1&tamanho=10&somenteAtivos=true
    [HttpGet]
    public ActionResult<Pagina<ProdutoResponse>> Listar(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanho = 10,
        [FromQuery] bool somenteAtivos = false)
        => Ok(_service.Listar(pagina, tamanho, somenteAtivos));

    [HttpGet("{id:guid}")]
    public ActionResult<ProdutoResponse> Obter(Guid id)
        => Ok(_service.Obter(id));

    [HttpPost]
    public ActionResult<ProdutoResponse> Criar(CriarProdutoRequest request)
    {
        var produto = _service.Criar(request);
        return CreatedAtAction(nameof(Obter), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:guid}/preco")]
    public ActionResult<ProdutoResponse> AtualizarPreco(Guid id, AtualizarPrecoRequest request)
        => Ok(_service.AtualizarPreco(id, request));

    [HttpPost("{id:guid}/desativar")]
    public ActionResult<ProdutoResponse> Desativar(Guid id)
        => Ok(_service.Desativar(id));

    [HttpDelete("{id:guid}")]
    public IActionResult Remover(Guid id)
    {
        _service.Remover(id);
        return NoContent();
    }
}
