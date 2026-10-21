using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo07_InterfaceGenerica;

// =====================================================================
// PASSO 7 — IRepository<T> em miniatura
//
// Tudo junto: interface genérica + implementação com restrição +
// duas entidades + um service que depende do CONTRATO.
// É a Parte 3 da aula, em escala reduzida.
// =====================================================================
public static class Passo07
{
    public static void Executar()
    {
        // Composição: AQUI o contrato é ligado à implementação. Em uma API, isso fica no Program.cs.
        IRepository<Produto> repoProdutos = new RepositorioEmMemoria<Produto>();
        var catalogo = new CatalogoService(repoProdutos);

        catalogo.Cadastrar("Teclado", 150m);
        catalogo.Cadastrar("Mouse", 80m);
        Console.WriteLine($"Produtos cadastrados: {repoProdutos.ObterTodos().Count}");
        Console.WriteLine($"Valor total: R$ {catalogo.ValorTotalDoEstoque():N2}");

        // A PROVA: a mesma implementação serve para Cliente. Zero código novo de repositório.
        IRepository<Cliente> repoClientes = new RepositorioEmMemoria<Cliente>();
        repoClientes.Adicionar(new Cliente("Ana", "ana@exemplo.com"));
        repoClientes.Adicionar(new Cliente("Bruno", "bruno@exemplo.com"));
        Console.WriteLine($"Clientes cadastrados: {repoClientes.ObterTodos().Count}");

        var primeiro = repoClientes.ObterTodos()[0];
        repoClientes.Remover(primeiro.Id);
        Console.WriteLine($"Após remover {primeiro.Nome}: {repoClientes.ObterTodos().Count} cliente(s)");

        // EXERCÍCIO 1: desenhe no papel as caixas Service -> IRepository<T> <- RepositorioEmMemoria<T>
        //              e marque as setas de dependência. CatalogoService conhece RepositorioEmMemoria?
        // EXERCÍCIO 2: crie RepositorioComLog.cs nesta pasta (RepositorioComLog<T> : IRepository<T>) que
        //              escreve no console a cada operação, e troque na linha de composição. CatalogoService mudou?
    }
}
