namespace Exemplos.Passo04_MetodosGenericos;

// =====================================================================
// PASSO 4 — Métodos genéricos
//
// Pergunta que este passo responde:
//   "E quando só UM método precisa do tipo em aberto, não a classe inteira?"
// =====================================================================
public static class Passo04
{
    public static void Executar()
    {
        // Trocar: o compilador INFERE que T é int. Não precisa escrever Trocar<int>.
        int x = 1, y = 2;
        Util.Trocar(ref x, ref y);
        Console.WriteLine($"Trocar(int):    x={x} y={y}");

        string a = "esquerda", b = "direita";
        Util.Trocar(ref a, ref b);
        Console.WriteLine($"Trocar(string): a={a} b={b}");

        // Se os tipos forem diferentes, não compila (descomente para ver):
        // Util.Trocar(ref x, ref a);

        Console.WriteLine($"Primeiro de [7,8,9]: {Util.PrimeiroOuPadrao(new List<int> { 7, 8, 9 })}");
        Console.WriteLine($"Primeiro de []:      {Util.PrimeiroOuPadrao(new List<string>()) ?? "(nulo)"}");

        // Paginar: a mesma função serve para números e para textos.
        var numeros = Enumerable.Range(1, 10).ToList();
        var nomes = new List<string> { "Ana", "Bruno", "Carla", "Davi", "Eva" };
        Console.WriteLine($"Página 2 (tam 3) de números: {string.Join(", ", Util.Paginar(numeros, 2, 3))}");
        Console.WriteLine($"Página 2 (tam 2) de nomes:   {string.Join(", ", Util.Paginar(nomes, 2, 2))}");

        // Converter: List<string> -> List<int> (tamanho de cada nome)
        var tamanhos = Util.Converter(nomes, nome => nome.Length);
        Console.WriteLine($"Tamanhos dos nomes: {string.Join(", ", tamanhos)}");

        // EXERCÍCIO 1: escreva Util.Ultimo<T>(List<T> itens) seguindo o modelo de PrimeiroOuPadrao.
        //              Chame com uma lista de int e uma de string sem escrever <int> ou <string>.
        // EXERCÍCIO 2: escreva Util.Contar<T>(List<T> itens, Func<T, bool> condicao) que devolve
        //              quantos itens satisfazem a condição. Use para contar nomes com mais de 3 letras.
    }
}
