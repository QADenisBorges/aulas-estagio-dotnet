using PedidosApi.Antes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria o banco na primeira execução (LocalDB).
new VendasDbContext().Database.EnsureCreated();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
