namespace Exemplos.Passo05_Heranca;

// =====================================================================
// PASSO 5 — Herança: a classe-base Entity
//
// Pergunta que este passo responde:
//   "O que significa Produto : Entity e para que serve uma classe-base?"
// =====================================================================
public static class Passo05
{
    public static void Executar()
    {
        var produto = new Produto("Teclado", 150m);
        var cliente = new Cliente("Ana", "ana@exemplo.com");

        // Nem Produto nem Cliente declararam Id, mas os dois têm.
        Console.WriteLine($"produto.Id = {produto.Id}");
        Console.WriteLine($"cliente.Id = {cliente.Id}");

        // Como os dois SÃO Entity, cabem na mesma lista.
        var entidades = new List<Entity> { produto, cliente };
        Console.WriteLine("Lista de Entity:");
        foreach (var e in entidades)
            Console.WriteLine($"  {e}");   // usa o ToString da base

        // Não compila: Entity é abstract (descomente para ver):
        // var solta = new Entity();

        // Não compila: Id tem set protegido (descomente para ver):
        // produto.Id = Guid.Empty;

        // EXERCÍCIO 1: crie Fornecedor.cs nesta pasta (Fornecedor : Entity, com Cnpj) e adicione na lista.
        //              Precisou declarar Id?
        // EXERCÍCIO 2: por que Entity é abstract? O que mudaria se fosse uma classe normal?
    }
}
