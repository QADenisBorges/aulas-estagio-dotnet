namespace Exemplos.Passo02_Contrato;

// =====================================================================
// PASSO 2 — Um contrato, várias implementações
//
// Pergunta que este passo responde:
//   "O que eu ganho quando várias classes assinam a mesma interface?"
// Resposta: o código que USA a interface não precisa saber qual é qual.
// =====================================================================
public static class Passo02
{
    public static void Executar()
    {
        // A lista é de ICalculadoraFrete. Quem lê o laço não sabe (nem precisa) quais são as classes.
        var opcoes = new List<ICalculadoraFrete>
        {
            new FreteCorreios(),
            new FreteTransportadora(),
            new FreteMotoboy(),
        };

        decimal peso = 4m;
        Console.WriteLine($"Frete para {peso} kg:");

        foreach (var frete in opcoes)
            Console.WriteLine($"  {frete.Nome,-15} R$ {frete.Calcular(peso):N2}");

        var maisBarato = opcoes.MinBy(f => f.Calcular(peso))!;
        Console.WriteLine($"  Mais barato: {maisBarato.Nome}");

        // EXERCÍCIO: crie FreteRetiradaNaLoja.cs (sempre R$ 0,00) e adicione na lista.
        // O foreach e o MinBy precisaram mudar? Isso é o que o contrato compra para você.
    }
}
