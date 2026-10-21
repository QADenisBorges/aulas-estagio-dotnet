# Aula 3 — POO: Abstrações, Interfaces e Generics

> Objetivo: sair da aula sabendo **explicar** abstração, interface, acoplamento, herança, métodos genéricos, interfaces genéricas e restrições genéricas — e depois **implementar** tudo isso em C#, montando um `IRepository<T>` em memória integrado com Service e Controller.
>
> Regra da aula: **entender primeiro, praticar depois, implementar sozinho no final.** Não há solução pronta no começo. O código completo existe na pasta `CatalogoApi`, mas ele só deve ser aberto na Parte 3, e preferencialmente depois de você ter tentado.

## Material desta pasta

| Pasta / arquivo | O que é | Quando usar |
|---|---|---|
| `Aula - Abstracoes, Interfaces e Generics.pptx` | Slides da aula (29 slides, com notas do apresentador) | Para conduzir a aula |
| `README.md` | Este roteiro: conceitos, exemplos, perguntas e exercícios | Durante toda a aula |
| `Exemplos/` | Projeto console com 7 passos pequenos e isolados, uma pasta por passo (`Passo01_…`…`Passo07_…`) | Parte 2 |
| `CatalogoApi/` | API em camadas (`Controller`, `Service`, `Repository` e `Domain` com `Entities`, `Repositories`, `Services`, `Dtos`, `Exceptions`) com listas em memória | Parte 3 |
| `CatalogoApi/CatalogoApi.http` | Requisições prontas para testar a API | Parte 3 |

```bash
dotnet run --project Exemplos
```

```bash
dotnet run --project CatalogoApi
```

Nenhum pacote externo, nenhum banco, nenhum Entity Framework. Só C# e ASP.NET Core.

---

# Parte 1 — Entender os conceitos antes do código

Nesta parte não abrimos o Visual Studio. A ideia é construir a intuição. Cada conceito vem com: o problema que ele resolve, uma analogia, e uma pergunta de verificação. Responda a pergunta em voz alta (ou por escrito) antes de seguir.

## 1.1 Abstração

**O problema.** Todo sistema real é cheio de detalhes. Se cada parte do sistema precisar conhecer todos os detalhes das outras partes, qualquer mudança vira uma reação em cadeia.

**A ideia.** Abstrair é **esconder o detalhe que não importa para quem usa e expor só o que importa**. Você decide o que o mundo precisa saber de um objeto, e guarda o resto dentro dele.

**Analogia.** O pedal do acelerador. Você pisa, o carro acelera. Você não sabe (nem precisa saber) se o motor é a combustão, elétrico ou híbrido. O pedal é a abstração: um ponto de contato simples que esconde um mecanismo complexo. Trocar o motor não muda o jeito de dirigir.

**No código.** Quando você escreve `lista.Add(produto)`, você não sabe como a `List<T>` guarda os itens na memória, como ela cresce, como ela realoca o array interno. Isso é abstração funcionando a seu favor todos os dias.

> **Verificação 1.** Pense em um aparelho da sua casa. Qual é a "interface" que você usa e qual é o "detalhe" que ela esconde? Se o fabricante trocar o detalhe sem mudar a interface, você percebe?

## 1.2 Interfaces: contratos

**O problema.** Eu quero que o meu código dependa de uma *capacidade* ("alguém que sabe enviar notificação") e não de uma *peça específica* ("a classe `EmailSender`, que usa SMTP na porta 587").

**A ideia.** Uma interface é um **contrato**: uma lista de métodos e propriedades que quem assinar se compromete a ter. A interface diz *o que* deve existir. Ela não diz *como*.

```csharp
public interface INotificador
{
    void Enviar(string destinatario, string mensagem);
}
```

Quem escreve `class EmailNotificador : INotificador` está dizendo: "eu assino o contrato, eu prometo ter um método `Enviar` com essa assinatura". O compilador cobra a promessa.

**Analogia.** Tomada de parede. A tomada é um contrato: dois ou três pinos, em posições padronizadas, com certa tensão. Qualquer aparelho que respeite o contrato funciona ali: geladeira, notebook, ventilador. A parede não sabe o que está ligado nela, e não precisa saber.

**Vocabulário.**
- *Definir* a interface: escrever o contrato.
- *Implementar* a interface: uma classe assinar o contrato e fornecer o código de cada método.
- *Depender* da interface: outra classe receber `INotificador` (e não `EmailNotificador`) e usar só o que o contrato garante.

> **Verificação 2.** Uma interface pode ter código dentro dos métodos? O que acontece se uma classe implementa `INotificador` mas esquece o método `Enviar`? Quem reclama: o compilador ou o usuário em produção?

## 1.3 Acoplamento e separação de responsabilidades

**Acoplamento** é o quanto uma peça do código *sabe* sobre outra. Quanto mais ela sabe, mais ela quebra quando a outra muda.

Alto acoplamento é assim:

```csharp
public class RelatorioService
{
    public void Fechar()
    {
        var email = new EmailSender("smtp.empresa.com", 587); // sabe demais
        email.Enviar("chefe@empresa.com", "Relatório fechado");
    }
}
```

O `RelatorioService` sabe que a notificação é por e-mail, sabe o servidor, sabe a porta. Se amanhã for WhatsApp, você edita o `RelatorioService`, que não tem nada a ver com WhatsApp. Se quiser testar o `RelatorioService`, ele tenta mandar e-mail de verdade.

Baixo acoplamento é assim:

```csharp
public class RelatorioService
{
    private readonly INotificador _notificador;
    public RelatorioService(INotificador notificador) => _notificador = notificador;

    public void Fechar() => _notificador.Enviar("chefe@empresa.com", "Relatório fechado");
}
```

Agora o `RelatorioService` só sabe que *existe alguém que envia*. Quem decide se é e-mail ou WhatsApp é quem monta o objeto, lá fora. Isso se chama **inversão de dependência**: a classe não cria o que ela usa, ela recebe pronto.

**Separação de responsabilidades** é o irmão desse conceito: cada classe tem **um motivo para mudar**. O `RelatorioService` muda se a regra do relatório mudar. O `EmailNotificador` muda se a forma de enviar e-mail mudar. Nunca os dois pelo mesmo motivo.

**Analogia.** Em um restaurante, o garçom anota o pedido e o cozinheiro prepara. O garçom não precisa saber cozinhar, e o cozinheiro não precisa saber atender mesa. Se o restaurante troca de cozinheiro, o garçom continua trabalhando do mesmo jeito. O "contrato" entre eles é a comanda.

> **Verificação 3.** Na versão acoplada, liste três coisas que o `RelatorioService` sabe e não deveria saber. Na versão desacoplada, *quem* decide qual notificador será usado?

## 1.4 Generics: por que existem

**O problema.** Imagine que você precisa de uma "caixa" que guarda um valor. Uma para inteiro, outra para texto, outra para `Produto`:

```csharp
class CaixaDeInteiro { public int Valor; }
class CaixaDeTexto   { public string Valor; }
class CaixaDeProduto { public Produto Valor; }
```

Três classes idênticas, só muda o tipo. Isso é **duplicação de código**: o mesmo raciocínio escrito várias vezes. Quando achar um bug em uma, tem que lembrar de corrigir nas outras.

A saída antiga era usar `object` para tudo:

```csharp
class Caixa { public object Valor; }
var c = new Caixa { Valor = 10 };
string s = (string)c.Valor; // compila! Explode em tempo de execução.
```

Você perdeu a segurança de tipos: o compilador não consegue mais te ajudar.

**A ideia.** Generics deixam você escrever a classe **uma vez, com o tipo em aberto**, e preencher o tipo quando for usar:

```csharp
class Caixa<T> { public T Valor; }

var a = new Caixa<int>    { Valor = 10 };
var b = new Caixa<string> { Valor = "oi" };
```

`T` é um **parâmetro de tipo**: um espaço em branco que o compilador preenche. `Caixa<int>` e `Caixa<string>` são tipos diferentes, cada um com segurança total. Você escreveu uma classe e ganhou infinitas.

**Analogia.** Uma forma de bolo. A forma é uma só; o que varia é a massa que você coloca. Você não compra uma forma para chocolate, outra para cenoura e outra para laranja.

Você já usa generics o tempo todo: `List<Produto>`, `Dictionary<string, int>`, `Task<Pedido>`. Alguém escreveu `List<T>` uma vez e você usa com qualquer tipo.

> **Verificação 4.** Qual a diferença prática entre `Caixa` com `object` e `Caixa<T>`? Em qual das duas o erro de tipo aparece *antes* de rodar o programa?

## 1.5 Métodos genéricos

Às vezes não é a classe inteira que precisa do tipo em aberto, é só **um método**. Aí o parâmetro de tipo vai no método:

```csharp
static void Trocar<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

Esse método troca dois valores de **qualquer tipo**, desde que sejam do mesmo tipo. O compilador geralmente **infere** o `T` sozinho: `Trocar(ref x, ref y)` já funciona sem você escrever `Trocar<int>`.

**Quando são úteis.** Quando o algoritmo é o mesmo para qualquer tipo: trocar, paginar uma lista, pegar o primeiro item, converter para resposta. Se você está escrevendo `PaginarProdutos`, `PaginarClientes`, `PaginarPedidos` com o mesmo corpo, é um método genérico pedindo para nascer.

> **Verificação 5.** `Paginar<T>(List<T> itens, int pagina, int tamanho)` precisa saber o que é um `T` para funcionar? Que operações ele faz com `T`?

## 1.6 Interfaces genéricas: `IRepository<T>`

Junte os dois conceitos: contrato + tipo em aberto.

Toda entidade do sistema precisa das mesmas operações de armazenamento: listar, obter por id, incluir, atualizar, remover. Sem generics, você escreveria `IProdutoRepository`, `IClienteRepository`, `IPedidoRepository`, cada um com os mesmos cinco métodos, só mudando o tipo de retorno.

Com uma interface genérica, o contrato é escrito **uma vez**:

```csharp
public interface IRepository<T>
{
    IReadOnlyList<T> ObterTodos();
    T? ObterPorId(Guid id);
    void Adicionar(T entidade);
    void Atualizar(T entidade);
    void Remover(Guid id);
}
```

`IRepository<Produto>` é "o contrato de armazenamento de produtos". `IRepository<Cliente>` é "o contrato de armazenamento de clientes". Mesmo contrato, tipos diferentes. O service de produtos depende de `IRepository<Produto>` e não sabe nem se os produtos estão em lista, em SQL ou em arquivo.

> **Verificação 6.** Se você precisar de uma operação que **só** faz sentido para produto (ex.: `ObterPorCodigoDeBarras`), onde ela entra? Em `IRepository<T>` ou em algum lugar específico? Por quê?

## 1.7 Herança: `Produto : Entity`

**O problema.** Todas as entidades do sistema têm um `Id`. Se cada classe declarar o seu, você repete a propriedade, repete a geração do id, e nada garante que todo mundo faça igual.

**A ideia.** Uma **classe-base** concentra o que é comum. As classes concretas **herdam** dela e só adicionam o que é específico.

```csharp
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}

public class Produto : Entity
{
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
}
```

`Produto : Entity` lê-se "Produto **é uma** Entity". Todo `Produto` tem `Id` sem declarar. `abstract` significa que ninguém cria um `new Entity()` solto: ela só existe para ser base.

**Diferença entre herança e interface.** Herança diz "**é um**" e **traz código** (a propriedade `Id` já vem pronta). Interface diz "**sabe fazer**" e só traz a **promessa** (você implementa). Uma classe herda de **uma** base, mas implementa **quantas** interfaces quiser.

**Cuidado.** Herança é para o que é de fato comum e estável. Não crie uma classe-base só para reaproveitar três linhas; isso gera hierarquias confusas. Aqui `Entity` tem um motivo claro: todo objeto que será armazenado precisa ser identificável.

> **Verificação 7.** Por que `Entity` é `abstract`? O que aconteceria se não fosse? `Produto` precisa declarar `Id`?

## 1.8 Restrições genéricas: `where T : Entity`

**O problema.** Dentro de `Repository<T>`, você quer implementar `ObterPorId(Guid id)` assim:

```csharp
return _itens.FirstOrDefault(x => x.Id == id);
```

Mas o compilador reclama: **"T não tem uma propriedade Id"**. Faz sentido: `T` pode ser qualquer coisa, inclusive `int` ou `string`, que não têm `Id`.

**A ideia.** Uma restrição diz ao compilador **o que `T` garante ser**:

```csharp
public class Repository<T> : IRepository<T> where T : Entity
```

Agora `T` só pode ser `Entity` ou algo que herda dela. O compilador sabe que todo `T` tem `Id` e deixa você usar. Em troca, ninguém consegue criar um `Repository<int>`, o que de qualquer forma não faria sentido.

**Por que usar.** Duas razões: (1) **poder** usar os membros da classe-base dentro do código genérico; (2) **impedir** usos sem sentido já em tempo de compilação.

Outras restrições que você vai encontrar:

| Restrição | Significa |
|---|---|
| `where T : Entity` | `T` herda de `Entity` (ou é ela) |
| `where T : IComparable<T>` | `T` implementa a interface |
| `where T : class` | `T` é tipo de referência |
| `where T : new()` | `T` tem construtor sem parâmetros (você pode fazer `new T()`) |

> **Verificação 8.** Sem a restrição, `Repository<T>` compila? Com a restrição, `Repository<string>` compila? Explique os dois casos.

## Pausa de síntese

Antes de ir para o código, explique com suas palavras, em uma frase cada: abstração, interface, acoplamento, generics, método genérico, interface genérica, herança, restrição genérica. Se alguma frase saiu travada, releia a seção. É mais barato reler agora do que debugar depois.

---

# Parte 2 — Compreender com exemplos pequenos

Abra o projeto `Exemplos`. São 7 pastas, uma por passo, cada classe em seu próprio arquivo e um `PassoNN.cs` que roda a demonstração no console. Rode tudo com:

```bash
dotnet run --project Exemplos
```

Leia **os arquivos de cada pasta antes de rodar**, tente prever a saída, e só depois confira. O arquivo `PassoNN.cs` de cada pasta tem um bloco `// EXERCÍCIO` no final.

| Passo | Arquivo | Conceito | O que observar |
|---|---|---|---|
| 1 | `Passo01_Acoplamento/` | Acoplamento e interface | A mesma classe `RelatorioService` em duas versões: a acoplada cria `new EmailSender()` por dentro; a desacoplada recebe `INotificador`. Troque a implementação sem tocar no service. |
| 2 | `Passo02_Contrato/` | Interface com várias implementações | Três cálculos de frete atrás de `ICalculadoraFrete`. O laço `foreach` não sabe qual é qual. |
| 3 | `Passo03_Duplicacao/` | Por que generics existem | `CaixaDeInteiro`, `CaixaDeTexto`, depois `Caixa` com `object` (e o erro em tempo de execução), depois `Caixa<T>`. |
| 4 | `Passo04_MetodosGenericos/` | Métodos genéricos | `Trocar<T>`, `PrimeiroOuPadrao<T>`, `Paginar<T>`. Note que você quase nunca escreve o `<T>` ao chamar. |
| 5 | `Passo05_Heranca/` | Classe-base `Entity` | `Produto` e `Cliente` herdam `Id`. Uma `List<Entity>` guarda os dois. |
| 6 | `Passo06_Restricoes/` | `where T : Entity` | Reusa `Entity`, `Produto` e `Cliente` do Passo 5. A versão sem restrição está comentada com o erro que o compilador dá. Descomente e veja. |
| 7 | `Passo07_InterfaceGenerica/` | `IRepository<T>` completo em miniatura | Um contrato, uma implementação em memória, duas entidades, um service que depende do contrato. É a Parte 3 em 60 linhas. |

### Roteiro sugerido por passo

**Passo 1.** Rode. Depois, troque `new EmailNotificador()` por `new ConsoleNotificador()` na montagem. Quantos arquivos você alterou? Esse número é a medida do acoplamento.

**Passo 2.** Adicione `FreteRetiradaNaLoja` (custo zero). O `foreach` precisou mudar? Isso é o que a interface compra para você.

**Passo 3.** Descomente a linha marcada `// ERRO EM TEMPO DE EXECUÇÃO`. Rode. Agora descomente a marcada `// ERRO EM TEMPO DE COMPILAÇÃO`. Qual dos dois erros você prefere receber? Por quê?

**Passo 4.** Escreva `Ultimo<T>(List<T> itens)` seguindo o modelo de `PrimeiroOuPadrao<T>`. Chame com uma lista de `int` e uma de `string` sem escrever `<int>` ou `<string>`.

**Passo 5.** Crie `Fornecedor : Entity` com `Cnpj`. Adicione na `List<Entity>`. Precisou declarar `Id`?

**Passo 6.** Remova o `where T : Entity` de `RepositorioSimples<T>`. Leia a mensagem de erro inteira. Coloque de volta. Tente `new RepositorioSimples<string>()`. Leia esse erro também.

**Passo 7.** Antes de abrir a Parte 3: com base só neste passo, desenhe em papel as caixinhas `Controller → Service → IRepository<T> ← Repository<T>` e indique a seta de dependência. Quem conhece quem?

> **Checkpoint da Parte 2.** Você deve conseguir, sem olhar o código, escrever de cabeça: (a) uma interface com dois métodos; (b) uma classe genérica `Caixa<T>`; (c) um método genérico `Trocar<T>`; (d) uma classe-base `Entity` e uma derivada; (e) a assinatura `class Repository<T> : IRepository<T> where T : Entity`. Se faltou algum, volte ao passo correspondente.

---

# Parte 3 — Evoluir para a implementação prática

Agora montamos a aplicação de verdade: um **catálogo de produtos** com API HTTP, organizado em quatro pastas:

```
CatalogoApi/
  Controller/        fala HTTP, chama IProdutoService, devolve status code
  Service/           implementa os contratos de service; depende de IProdutoRepository
  Repository/        Repository<T> genérico (base) + ProdutoRepository, CategoriaRepository
  Domain/            tudo que é contrato ou regra, sem detalhe técnico
    Entities/        Entity, Produto, Categoria
    Repositories/    IRepository<T>, IProdutoRepository, ICategoriaRepository
    Services/        IProdutoService, ICategoriaService
    Dtos/            Pagina<T>, requests e responses
    Exceptions/      DomainException, NaoEncontradoException
```

A seta de dependência é sempre **para dentro**: `Controller → Domain ← Service` e `Domain ← Repository`. Todo mundo aponta para `Domain`, e `Domain` não aponta para ninguém. O controller conhece `IProdutoService`, não `ProdutoService`. O service conhece `IProdutoRepository`, não `ProdutoRepository`. Os contratos dos services ficam no `Domain` porque descrevem a regra de negócio vista de fora: *o que* a aplicação faz, sem dizer *como*.

### Modo de fazer

Faça **na ordem abaixo, um passo de cada vez**, compilando a cada passo (`dotnet build`). Para cada passo há: o que criar, por que, e a conexão com a Parte 1. Tente escrever antes de olhar o arquivo correspondente em `CatalogoApi/`. Se travar por mais de 10 minutos, olhe, entenda, feche e reescreva.

### Passo 3.1 — Criar o projeto

```bash
dotnet new webapi --use-controllers -n CatalogoApi
```

Apague o `WeatherForecast` e crie as quatro pastas. Nada de pacotes extras.

### Passo 3.2 — `Domain/Entities/Entity.cs`

A classe-base. `abstract`, com `Id` do tipo `Guid` gerado no construtor. (Conceito 1.7.)

Pergunta antes de escrever: o `set` de `Id` deve ser público? Quem deveria poder trocar o id de uma entidade depois de criada?

### Passo 3.3 — `Domain/Entities/Produto.cs`

`Produto : Entity` com `Nome`, `Preco`, `Ativo`. Construtor que valida (nome vazio e preço negativo são erro). Métodos `AtualizarPreco` e `Desativar`. (Conceitos 1.1 e 1.7: a entidade esconde como se mantém válida.)

Por que não deixar `set` público em tudo? Porque aí qualquer linha do sistema pode colocar preço `-10`. A validação viraria responsabilidade de todo mundo, ou seja, de ninguém.

### Passo 3.4 — `Domain/Repositories/IRepository.cs`

O contrato genérico, exatamente como na seção 1.6. Cinco operações: `ObterTodos`, `ObterPorId`, `Adicionar`, `Atualizar`, `Remover`.

Pergunta: a interface precisa de `where T : Entity`? Ela usa `.Id` em algum lugar? (Ela não usa, então tecnicamente não precisa; a restrição é obrigatória só na **implementação**, que é quem usa `.Id`. No `CatalogoApi` colocamos na interface também, para deixar o contrato explícito: "só guardo entidades". Discuta os dois lados.)

### Passo 3.5 — `Domain/Repositories/IProdutoRepository.cs`

O contrato **específico** de produto, que herda o genérico:

```csharp
public interface IProdutoRepository : IRepository<Produto>
{
    IReadOnlyList<Produto> ObterAtivos();
    bool ExisteComNome(string nome);
}
```

Esta é a resposta da Verificação 6. As cinco operações comuns vêm de `IRepository<Produto>` sem repetir nada; aqui só entra o que faz sentido para produto. (Conceito 1.6.)

### Passo 3.6 — `Repository/Repository.cs` e `Repository/ProdutoRepository.cs`

Primeiro a base: `abstract class Repository<T> : IRepository<T> where T : Entity`, com uma `List<T>` privada e um método `protected Consultar(Func<T, bool>)` para as filhas. (Conceitos 1.6, 1.7 e 1.8.)

Tente escrever `ObterPorId` **sem** a restrição primeiro. Leia o erro. Adicione a restrição. Esse erro é o motivo de existir a seção 1.8.

Depois a específica: `class ProdutoRepository : Repository<Produto>, IProdutoRepository`. Ela herda o CRUD inteiro e escreve só `ObterAtivos` e `ExisteComNome`, usando `Consultar`. Repare que ela **herda** de uma classe (traz código) e **implementa** uma interface (assina contrato) na mesma linha: é a diferença da seção 1.7 em ação.

Atenção ao `Atualizar`: como a lista guarda referências, alterar o objeto já alterou a lista. O método existe para honrar o contrato (um repositório SQL precisa dele) e para o caso de receber um objeto que ainda não está na lista.

### Passo 3.7 — `Domain/Dtos/` e `Domain/Services/IProdutoService.cs`

Primeiro os DTOs (records simples de entrada e saída) e o `Pagina<T>`. Eles ficam no `Domain`, em pasta própria, porque as assinaturas dos contratos de service os usam.

Depois o contrato do service: `IProdutoService` com listar, obter, criar, atualizar preço, desativar, remover. Ele descreve a regra de negócio de produto vista de fora, por isso mora no `Domain`. O controller vai depender dele.

### Passo 3.8 — `Service/ProdutoService.cs`

`class ProdutoService : IProdutoService`, recebendo `IProdutoRepository` pelo construtor. (Conceito 1.3: depende do contrato, não da implementação.)

Ele devolve sempre DTOs, nunca a entidade crua. Isso mantém a entidade livre para mudar sem quebrar a resposta HTTP. A regra "não pode haver dois produtos com o mesmo nome" fica aqui, usando `ExisteComNome` do contrato específico.

### Passo 3.9 — Um método genérico: `Service/ServiceHelpers.cs`

Dois métodos genéricos que **qualquer** service vai usar:

- `Paginar<T>(IEnumerable<T>, int pagina, int tamanho)`: devolve uma página de qualquer coisa.
- `ObterOuFalhar<T>(IRepository<T>, Guid id) where T : Entity`: busca no repositório e lança exceção padronizada se não achar. Observe que aqui a restrição é **necessária**: o método usa `IRepository<T>`, que exige `T : Entity`.

Use os dois no `ProdutoService`. (Conceito 1.5.)

### Passo 3.10 — `Controller/ProdutosController.cs`

Fino. Recebe `IProdutoService` (o contrato, não a classe), traduz HTTP para chamadas e resultado para status code. Zero regra de negócio aqui.

### Passo 3.11 — `Program.cs`: ligar os contratos às implementações

```csharp
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
```

Este é o único lugar do sistema que sabe que `ProdutoRepository` e `ProdutoService` existem. Todo o resto só conhece interfaces.

Por que `Singleton` no repositório? Porque a lista em memória **é** o nosso banco. Se fosse `Scoped`, cada requisição ganharia uma lista nova e vazia. Discuta: quando trocarmos por um banco real, esse tempo de vida continua fazendo sentido?

### Passo 3.12 — Rodar e testar

```bash
dotnet run --project CatalogoApi
```

Use o arquivo `CatalogoApi.http` (o Visual Studio e o VS Code com a extensão REST Client executam direto). Crie dois produtos, tente criar um com nome repetido (400), liste, atualize o preço de um, desative, remova, tente buscar o removido (404).

### Passo 3.13 — A prova dos nove: segunda entidade

Agora o teste real de que a base é genérica. Crie `Categoria : Entity` (com `Nome` e `Descricao`), `ICategoriaRepository`, `CategoriaRepository`, `ICategoriaService`, `CategoriaService` e `CategoriasController`.

Conte quantas linhas de CRUD você escreveu em `CategoriaRepository`. **Zero.** Ela tem uma única consulta própria (`ObterPorNome`) e herda `ObterTodos`, `ObterPorId`, `Adicionar`, `Atualizar` e `Remover` da base. Isso é reutilização de verdade: não é copiar e colar, é escrever uma vez e herdar N vezes.

> **Checkpoint da Parte 3.** Responda, olhando o seu código: (1) onde está a única linha que sabe que `ProdutoRepository` existe? (2) se amanhã `Repository<T>` passar a salvar em arquivo JSON, quais arquivos mudam? (3) e se passar a usar SQL? (4) `ProdutoService` consegue ser testado sem subir a API? Como? (5) por que `IProdutoService` fica no `Domain` e `ProdutoService` não?

---

# Exercícios finais (para fazer sozinho)

1. **Nova entidade do zero.** Crie `Fornecedor : Entity` com `Nome` e `Cnpj`, com `IFornecedorRepository`, `FornecedorRepository`, `IFornecedorService`, `FornecedorService` e controller. Meta: não tocar em `IRepository<T>` nem em `Repository<T>`, e não escrever nenhuma linha de CRUD.
2. **Novo método genérico.** Em `ServiceHelpers`, crie `Ordenar<T, TChave>(IEnumerable<T> itens, Func<T, TChave> chave)` e use em `ProdutoService.Listar` para ordenar por nome. Dica: olhe a assinatura do `OrderBy` do LINQ.
3. **Segunda implementação do contrato.** Crie `Repository/ProdutoRepositoryComLog` que implementa `IProdutoRepository` escrevendo no console a cada operação e delegando para um `ProdutoRepository` interno. Troque o registro no `Program.cs`. Quantos arquivos além desses dois você mudou? `ProdutoService` percebeu?
4. **Restrição extra.** Crie uma interface `IAtivavel { bool Ativo { get; } void Desativar(); }`, faça `Produto` implementar, e escreva um método genérico `DesativarTodos<T>(IRepository<T> repo) where T : Entity, IAtivavel`. Por que precisa das duas restrições?
5. **Para explicar, não para codar.** Escreva um parágrafo para um colega que nunca viu generics explicando por que `IRepository<T>` é melhor do que `IProdutoRepository` + `ICategoriaRepository` + `IFornecedorRepository`. Depois escreva outro parágrafo dizendo em que situação você ainda criaria um `IProdutoRepository` específico.

---

# Resumo em uma tela

| Conceito | Frase-chave | Onde está no `CatalogoApi` |
|---|---|---|
| Abstração | Esconder o detalhe, expor o essencial | `Produto` valida por dentro; quem usa só chama `AtualizarPreco` |
| Interface | Contrato: diz *o que*, não *como* | `Domain/Repositories/IRepository.cs`, `Domain/Services/IProdutoService.cs` |
| Acoplamento | Quanto uma classe sabe da outra; quanto menos, melhor | Controller recebe `IProdutoService`; service recebe `IProdutoRepository`; ninguém dá `new` |
| Generics | Escrever uma vez, tipo em aberto | `IRepository<T>`, `Repository<T>`, `Pagina<T>` |
| Método genérico | Algoritmo igual para qualquer tipo | `ServiceHelpers.Paginar<T>`, `ObterOuFalhar<T>` |
| Interface genérica | Contrato reutilizável por tipo | `IProdutoRepository : IRepository<Produto>` |
| Herança | "É um"; a base traz código comum | `Produto : Entity`, `ProdutoRepository : Repository<Produto>` |
| Restrição genérica | O que `T` garante ser | `where T : Entity` em `Repository<T>` |

Próxima aula: o que muda quando o domínio cresce e as entidades passam a carregar regras de negócio de verdade (DDD). Hoje o foco foi a ferramenta; lá será o desenho.

---

# Mapa slide → material

| Slide | Conteúdo | Arquivo |
|---|---|---|
| 4 a 12 | Parte 1: um conceito por slide, com analogia e pergunta de verificação | Seções 1.1 a 1.8 deste README |
| 13 | Pausa de síntese | — |
| 15 | Os sete passos | `Exemplos/Passo01_…` a `Passo07_…` |
| 16 | Como trabalhar cada passo e checkpoint | `Exemplos/*/PassoNN.cs` |
| 18 | Arquitetura e setas de dependência | Estrutura de pastas de `CatalogoApi/` |
| 19 | `Entity` e `Produto` | `CatalogoApi/Domain/Entities/` |
| 20 | `IRepository<T>` e `IProdutoRepository` | `CatalogoApi/Domain/Repositories/` |
| 21 | `Repository<T>` e `ProdutoRepository` | `CatalogoApi/Repository/` |
| 22 | `IProdutoService`, DTOs e `ProdutoService` | `CatalogoApi/Domain/Services/`, `Domain/Dtos/`, `Service/ProdutoService.cs` |
| 23 | Métodos genéricos `Paginar<T>` e `ObterOuFalhar<T>` | `CatalogoApi/Service/ServiceHelpers.cs` |
| 24 | Controller e composição | `CatalogoApi/Controller/ProdutosController.cs`, `Program.cs` |
| 25 | Prova dos nove: `Categoria` | `CatalogoApi/Repository/CategoriaRepository.cs` |
| 26 a 28 | Checkpoint, exercícios finais e resumo | Seções correspondentes deste README |
