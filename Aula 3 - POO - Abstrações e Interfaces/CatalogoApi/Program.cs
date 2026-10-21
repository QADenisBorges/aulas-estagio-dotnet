using CatalogoApi.Controller;
using CatalogoApi.Domain.Repositories;
using CatalogoApi.Domain.Services;
using CatalogoApi.Repository;
using CatalogoApi.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ----- Composição: AQUI, e só aqui, os contratos são ligados às implementações. -----

// Um repositório por entidade. Cada um herda o CRUD de Repository<T> e só acrescenta
// as consultas próprias. Esta é a única parte do sistema que sabe que ProdutoRepository existe.
//
// Singleton porque a lista em memória É o nosso banco: se fosse Scoped, cada requisição
// ganharia uma lista nova e vazia.
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();
builder.Services.AddSingleton<ICategoriaRepository, CategoriaRepository>();

// Services: o contrato (Domain/Services) ligado à implementação (Service/).
// O controller só conhece o contrato; nenhum service sabe que a pasta Repository/ existe.
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// Tradução de exceções em status HTTP (400 / 404), para o controller ficar fino.
builder.Services.AddExceptionHandler<ErrosHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.MapControllers();

app.Run();
