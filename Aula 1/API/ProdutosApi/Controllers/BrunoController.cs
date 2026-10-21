using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Dtos;
using ProdutosApi.Models;

namespace ProdutosApi.Controllers;

[ApiController]
[Route("item")]
[Produces("application/json")]
public class BrunoController : ControllerBase
{
    [HttpPut]
    [Route("desativar")]
    public ActionResult Listar()
    {
        return Ok(new { Message = "Hello from BrunoController!" });
    }
}
