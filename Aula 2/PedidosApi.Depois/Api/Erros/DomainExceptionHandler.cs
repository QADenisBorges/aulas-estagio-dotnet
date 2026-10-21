using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PedidosApi.Depois.Domain.Pedidos;

namespace PedidosApi.Depois.Api.Erros;

/// <summary>Regra de negócio violada vira HTTP 400 em um único lugar.</summary>
public class DomainExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DomainException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Regra de negócio violada.",
            Detail = exception.Message,
            Instance = httpContext.Request.Path,
        }, cancellationToken);

        return true;
    }
}
