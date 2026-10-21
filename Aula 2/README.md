# Aula — Abstrações, Repository, DI e um primeiro passo em DDD

Material da aula de 30 minutos (`Aula - Abstracoes, Repository, DI e DDD.pptx`) e os dois projetos de exemplo usados nos slides.

## Projetos

| Pasta | O que é | Porta |
|---|---|---|
| `PedidosApi.Antes` | A API "que funciona, mas dói": o controller cria o banco, aplica regra, salva e manda e-mail. | http://localhost:5101/swagger |
| `PedidosApi.Depois` | A mesma API organizada em Domínio / Aplicação / Infraestrutura, com Repository, DI, entidade rica e objeto de valor. | http://localhost:5102/swagger |
| `PedidosApi.Depois.Tests` | Testes xUnit do caso de uso usando implementações falsas (sem banco). | — |

Os dois projetos expõem os mesmos endpoints:

- `POST /api/pedidos` — cria um pedido com itens
- `POST /api/pedidos/{id}/confirmar` — confirma o pedido
- `GET /api/pedidos/{id}` — consulta o pedido

## Como rodar

```bash
dotnet build PedidosApi.slnx
```

```bash
dotnet run --project PedidosApi.Depois
```

```bash
dotnet test PedidosApi.Depois.Tests
```

- `PedidosApi.Depois` em ambiente `Development` roda **em memória** (a connection string em `appsettings.Development.json` está vazia). Para usar SQL Server, preencha `ConnectionStrings:Vendas`.
- `PedidosApi.Antes` usa LocalDB com a connection string **fixa no código** (`VendasDbContext.cs`). Isso é proposital: é um dos problemas mostrados na aula.

## Mapa slide → código

| Slide | Arquivo |
|---|---|
| 2 — A API que funciona, mas dói | `PedidosApi.Antes/Controllers/PedidosController.cs` |
| 4 — Interface é o contrato | `PedidosApi.Depois/Domain/Pedidos/IPedidoRepository.cs` |
| 5 — Repository | `PedidosApi.Depois/Infrastructure/Persistencia/EfPedidoRepository.cs` e `Application/Pedidos/PedidoService.cs` |
| 7 — Injeção de Dependência | `PedidosApi.Depois/Infrastructure/DependencyInjection.cs` e `Program.cs` |
| 8 — Teste sem banco | `PedidosApi.Depois.Tests/Fakes.cs` e `PedidoServiceTests.cs` |
| 11 — Entidade anêmica × rica | `PedidosApi.Antes/Models/Pedido.cs` × `PedidosApi.Depois/Domain/Pedidos/Pedido.cs` e `Dinheiro.cs` |
| 12 — Antes × depois | estrutura de pastas dos dois projetos |

## Exemplo de requisição

```json
{
  "clienteEmail": "cliente@exemplo.com",
  "itens": [
    { "produto": "Teclado", "quantidade": 2, "precoUnitario": 150.00 },
    { "produto": "Mouse", "quantidade": 1, "precoUnitario": 80.00 }
  ]
}
```

Depois de criar, chame `POST /api/pedidos/{id}/confirmar`. Tente confirmar duas vezes, ou criar um pedido sem itens, para ver a regra de negócio respondendo com `400` em cada projeto.
