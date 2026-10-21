using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using ProdutosApi.Dtos;
using ProdutosApi.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(opcoes =>
{
    opcoes.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Produtos API",
        Version = "v1",
        Description = "API de exemplo para estudo. Dados em memória, sem autenticação.",
    });

    var caminhoXml = Path.Combine(
        AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

    if (File.Exists(caminhoXml))
    {
        opcoes.IncludeXmlComments(caminhoXml);
    }
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(ProdutoProfile).Assembly);
});

var app = builder.Build();

app.UseExceptionHandler(rotaDeErro =>
{
    rotaDeErro.Run(async contexto =>
    {
        var excecao = contexto.Features.Get<IExceptionHandlerFeature>()?.Error;
        var traceId = contexto.TraceIdentifier;

        var log = contexto.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("Erros");
        log.LogError(
            excecao,
            "Erro inesperado em {Metodo} {Caminho}. TraceId: {TraceId}",
            contexto.Request.Method,
            contexto.Request.Path,
            traceId);

        var problema = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Erro interno no servidor.",
            Detail = "Não foi possível concluir a requisição. Informe o traceId ao suporte.",
            Instance = contexto.Request.Path,
        };
        problema.Extensions["traceId"] = traceId;

        contexto.Response.StatusCode = StatusCodes.Status500InternalServerError;
        contexto.Response.ContentType = "application/problem+json";

        await contexto.Response.WriteAsJsonAsync(problema);
    });
});

app.UseSwagger();
app.UseSwaggerUI(opcoes =>
{
    opcoes.SwaggerEndpoint("/swagger/v1/swagger.json", "Produtos API v1");
    opcoes.DocumentTitle = "Produtos API";
});

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.MapControllers();

app.Run();
