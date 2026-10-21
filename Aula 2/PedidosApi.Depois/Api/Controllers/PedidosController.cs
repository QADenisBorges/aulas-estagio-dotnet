using Microsoft.AspNetCore.Mvc;
using PedidosApi.Depois.Application.Pedidos;

namespace PedidosApi.Depois.Api.Controllers;

/// <summary>
/// Controller fino: traduz HTTP para o caso de uso e o resultado para HTTP.
/// Nenhuma regra de negócio, nenhum acesso a banco.
/// </summary>
[ApiController]
[Route("api/pedidos")]
[Produces("application/json")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _service;

    public PedidosController(PedidoService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoResponse>> Criar(CriarPedidoRequest request)
    {
        var pedido = await _service.CriarAsync(request);
        return CreatedAtAction(nameof(Obter), new { id = pedido.Id }, pedido);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponse>> Obter(Guid id)
    {
        var pedido = await _service.ObterAsync(id);
        return pedido is null ? NotFound() : Ok(pedido);
    }

    [HttpPost("{id:guid}/confirmar")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoResponse>> Confirmar(Guid id)
    {
        var pedido = await _service.ConfirmarAsync(id);
        return pedido is null ? NotFound() : Ok(pedido);
    }
}
