using Microsoft.AspNetCore.Mvc;

namespace ProdutosApi.Controllers;

[ApiController]
[Route("exemplos")]
[Produces("application/json")]
[Tags("Exemplos")]
public class ExemplosController : ControllerBase
{
    /// <summary>Mostra os headers recebidos na requisição.</summary>
    [HttpGet("headers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VerHeaders([FromHeader(Name = "X-Origem-Requisicao")] string? origem)
    {
        return Ok(new
        {
            origemInformada = origem ?? "(header X-Origem-Requisicao não enviado)",
            contentType = Request.Headers.ContentType.ToString(),
            accept = Request.Headers.Accept.ToString(),
            userAgent = Request.Headers.UserAgent.ToString(),
            todosOsHeaders = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
        });
    }

    /// <summary>Provoca um erro inesperado, para demonstrar a resposta 500.</summary>
    [HttpGet("erro")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IActionResult ProvocarErro()
    {
        var divisor = 0;
        return Ok(42 / divisor);
    }
}
