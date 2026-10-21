# Programa de Estágio — C# / .NET

Material das aulas técnicas do programa de estágio: slides, roteiros e projetos de exemplo em ASP.NET Core.

Cada aula tem a própria pasta, com slides (`.pptx`), um `README.md` de roteiro e os projetos que compilam e rodam sozinhos.

## Aulas

| Aula | Tema | Pasta | O que tem |
|---|---|---|---|
| 1 | APIs REST com ASP.NET Core | [`Aula 1/API`](Aula%201/API) | `ProdutosApi`: CRUD completo em memória, Swagger, DTOs, paginação. Slides e material de estudo em `.docx`. |
| 2 | Abstrações, Repository, Injeção de Dependência e um primeiro passo em DDD | [`Aula 2`](Aula%202) | `PedidosApi.Antes` (a API "que funciona, mas dói") e `PedidosApi.Depois` (Domínio / Aplicação / Infraestrutura), com testes xUnit sem banco. |
| 3 | POO: Abstrações, Interfaces e Generics | [`Aula 3 - POO - Abstrações e Interfaces`](Aula%203%20-%20POO%20-%20Abstra%C3%A7%C3%B5es%20e%20Interfaces) | Roteiro completo (conceitos → exemplos → implementação), projeto console `Exemplos` com 7 passos e `CatalogoApi` em camadas com `IRepository<T>` e listas em memória. |

## Como usar

Pré-requisito: [.NET SDK 10](https://dotnet.microsoft.com/download). Nenhum banco de dados é necessário: os projetos rodam em memória por padrão.

Em cada pasta de aula:

```bash
dotnet build
```

```bash
dotnet run --project <NomeDoProjeto>
```

O `README.md` de cada aula diz as portas, os endpoints e o que testar. Os arquivos `.http` podem ser executados direto no Visual Studio ou no VS Code (extensão REST Client).

## Método das aulas

As aulas seguem sempre a mesma ordem: **entender** o conceito (problema, ideia, analogia, pergunta de verificação), **praticar** com exemplos pequenos e isolados, e só então **implementar** a aplicação completa, um arquivo por vez. Nenhuma aula começa pela solução pronta: o código aqui é para consultar depois de tentar.
