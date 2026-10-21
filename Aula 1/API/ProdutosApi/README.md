# Produtos API

API de exemplo em ASP.NET Core para estudo. Um único recurso (`produtos`), CRUD
completo, dados em memória e Swagger. Sem autenticação e sem banco de dados.

Tudo acontece dentro da controller — não há camada de serviço nem repositório.

## Como rodar

```bash
dotnet run --project ProdutosApi
```

Swagger: **http://localhost:5135/swagger**

Os dados ficam em memória. Reiniciar devolve tudo ao estado inicial (4 produtos).

## Endpoints

| Método   | Endpoint            | Respostas           |
| -------- | ------------------- | ------------------- |
| `GET`    | `/produtos`         | `200`, `400`        |
| `GET`    | `/produtos/{id}`    | `200`, `404`        |
| `POST`   | `/produtos`         | `201`, `400`        |
| `PUT`    | `/produtos/{id}`    | `200`, `400`, `404` |
| `PATCH`  | `/produtos/{id}`    | `200`, `400`, `404` |
| `DELETE` | `/produtos/{id}`    | `204`, `404`        |
| `GET`    | `/exemplos/headers` | `200`               |
| `GET`    | `/exemplos/erro`    | `500`               |

Filtros do `GET /produtos`: `nome`, `categoria`, `ativo`, `pagina`, `tamanhoPagina`.

## Estrutura

```
Controllers/ProdutosController.cs   CRUD, dados e regras
Controllers/ExemplosController.cs   headers e erro 500
Models/Produto.cs                   o produto por dentro
Dtos/                               o que entra e o que sai pela API
Program.cs                          Swagger e tratamento de erros
ProdutosApi.http                    requisições prontas para testar
```

## Como testar

- **Swagger** — http://localhost:5135/swagger, botão *Try it out*.
- **`ProdutosApi.http`** — no Visual Studio ou no VS Code com a extensão *REST Client*.
- **curl / Postman / Insomnia.**

## Observações

- `Produtos` é uma lista `static` porque o ASP.NET cria uma controller nova a cada
  requisição.
- `ProdutoResponse` não expõe o campo `CustoInterno` que existe em `Produto`.
- `GET /exemplos/erro` devolve 500 genérico ao cliente; a exception completa vai
  para o log do servidor.
- Sem autenticação, portanto não há `401` nem `403` nesta API.
- Se o restore falhar por causa de feed corporativo, o `nuget.config` do projeto
  já força o uso apenas do nuget.org.
