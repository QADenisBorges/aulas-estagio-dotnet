using CatalogoApi.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controller;

/// <summary>
/// Traduz exceções da aplicação em respostas HTTP, em um lugar só.
/// Assim os controllers não precisam de try/catch em cada action.
///
///   DomainException        -> 400 (regra violada)
///   NaoEncontradoException -> 404
///   qualquer outra         -> deixa o ASP.NET responder 500
/// </summary>
public class ErrosHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext http, Exception excecao, CancellationToken ct)
    {
        var (status, titulo) = excecao switch
        {
            DomainException => (StatusCodes.Status400BadRequest, "Regra de negócio violada"),
            NaoEncontradoException => (StatusCodes.Status404NotFound, "Não encontrado"),
            _ => (0, ""),
        };

        if (status == 0)
            return false;   // não é nossa: segue para o tratamento padrão

        http.Response.StatusCode = status;
        await http.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = titulo,
            Detail = excecao.Message,
        }, ct);

        return true;
    }
}
