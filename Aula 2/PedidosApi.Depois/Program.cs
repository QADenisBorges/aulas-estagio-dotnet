using Microsoft.EntityFrameworkCore;
using PedidosApi.Depois.Api.Erros;
using PedidosApi.Depois.Infrastructure;
using PedidosApi.Depois.Infrastructure.Persistencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Composição da aplicação: contratos -> implementações.
builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.AddAplicacao();

builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Cria o banco na primeira execução (apenas para a aula; em produção use migrations).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VendasDbContext>();
    if (db.Database.IsRelational())
        db.Database.EnsureCreated();
}

app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
