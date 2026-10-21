using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosApi.Antes.Models;

namespace PedidosApi.Antes.Controllers;

// A API FUNCIONA. O problema é o que acontece quando ela precisa mudar.
//
// Tudo está aqui dentro: criação do banco, regra de negócio, persistência
// e envio de e-mail. Repare nos comentários "PROBLEMA".
[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    [HttpPost]
    public IActionResult Criar(Pedido pedido)
    {
        var db = new VendasDbContext(); // PROBLEMA: o controller cria o banco.

        // PROBLEMA: regra de negócio no controller...
        if (pedido.Itens == null || pedido.Itens.Count == 0)
            return BadRequest("Pedido sem itens.");

        pedido.Id = Guid.NewGuid();
        pedido.Status = "Rascunho";
        pedido.Total = 0;
        foreach (var item in pedido.Itens)
        {
            if (item.Quantidade <= 0) return BadRequest("Quantidade inválida.");
            item.Id = Guid.NewGuid();
            pedido.Total += item.Preco * item.Quantidade; // ...e cálculo também.
        }

        db.Pedidos.Add(pedido);
        db.SaveChanges();
        return Created($"/api/pedidos/{pedido.Id}", pedido); // PROBLEMA: devolve a entidade do banco.
    }

    [HttpPost("{id}/confirmar")]
    public IActionResult Confirmar(Guid id)
    {
        var db = new VendasDbContext(); // PROBLEMA: de novo.

        var pedido = db.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);
        if (pedido == null) return NotFound();

        // PROBLEMA: a mesma regra "pedido sem itens" repetida (e pode divergir da de cima).
        if (pedido.Status == "Confirmado") return BadRequest("Pedido já confirmado.");
        if (pedido.Itens.Count == 0) return BadRequest("Pedido sem itens.");

        pedido.Status = "Confirmado"; // PROBLEMA: string livre. "confirmado" seria outro status.
        db.SaveChanges();

        new EmailService().EnviarConfirmacao(pedido); // PROBLEMA: dependência concreta, sem como substituir.

        return Ok(pedido);
    }

    [HttpGet("{id}")]
    public IActionResult Obter(Guid id)
    {
        var db = new VendasDbContext();
        var pedido = db.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == id);
        return pedido == null ? NotFound() : Ok(pedido);
    }
}
