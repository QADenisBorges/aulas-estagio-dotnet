using CatalogoApi.Domain.Dtos;
using CatalogoApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controller;

[ApiController]
[Route("api/categorias")]
[Produces("application/json")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service) => _service = service;

    [HttpGet]
    public ActionResult<Pagina<CategoriaResponse>> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        => Ok(_service.Listar(pagina, tamanho));

    [HttpGet("{id:guid}")]
    public ActionResult<CategoriaResponse> Obter(Guid id)
        => Ok(_service.Obter(id));

    [HttpPost]
    public ActionResult<CategoriaResponse> Criar(CriarCategoriaRequest request)
    {
        var categoria = _service.Criar(request);
        return CreatedAtAction(nameof(Obter), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id:guid}/nome")]
    public ActionResult<CategoriaResponse> Renomear(Guid id, RenomearCategoriaRequest request)
        => Ok(_service.Renomear(id, request));

    [HttpDelete("{id:guid}")]
    public IActionResult Remover(Guid id)
    {
        _service.Remover(id);
        return NoContent();
    }
}
