using Exemplos.Passo05_Heranca;

namespace Exemplos.Passo06_Restricoes;

// =====================================================================
// PASSO 6 — Restrições genéricas: where T : Entity
//
// Pergunta que este passo responde:
//   "Por que preciso escrever where T : Entity? O que acontece sem?"
// Reusa Entity, Produto e Cliente do Passo 5.
// =====================================================================
public static class Passo06
{
    public static void Executar()
    {
        var repo = new RepositorioSimples<Produto>();
        var teclado = new Produto("Teclado", 150m);
        var mouse = new Produto("Mouse", 80m);
        repo.Adicionar(teclado);
        repo.Adicionar(mouse);

        var achado = repo.ObterPorId(mouse.Id);
        Console.WriteLine($"Repositório de Produto com {repo.Quantidade} itens. Buscado por Id: {achado?.Nome}");

        // A mesma classe serve para Cliente, sem escrever nada novo.
        var repoClientes = new RepositorioSimples<Cliente>();
        repoClientes.Adicionar(new Cliente("Ana", "ana@exemplo.com"));
        Console.WriteLine($"Repositório de Cliente com {repoClientes.Quantidade} item.");

        // NÃO COMPILA: string não é Entity (descomente para ver):
        //   CS0311: O tipo 'string' não pode ser usado como parâmetro de tipo 'T'...
        // var repoTextos = new RepositorioSimples<string>();

        Console.WriteLine($"Maior(3, 9) = {OutrasRestricoes.Maior(3, 9)}");
        Console.WriteLine($"Maior(\"abacate\", \"uva\") = {OutrasRestricoes.Maior("abacate", "uva")}");
        Console.WriteLine($"CriarVazio<List<int>>() tem {OutrasRestricoes.CriarVazio<List<int>>().Count} itens");

        // EXERCÍCIO 1: remova o "where T : Entity" de RepositorioSimples. Leia a mensagem de erro inteira.
        // EXERCÍCIO 2: coloque de volta e tente new RepositorioSimples<string>(). Leia esse erro também.
        // EXERCÍCIO 3: tente OutrasRestricoes.CriarVazio<Produto>(). Por que não compila?
    }
}
