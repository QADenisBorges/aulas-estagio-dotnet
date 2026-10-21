namespace Exemplos.Passo03_Duplicacao;

// =====================================================================
// PASSO 3 — Duplicação → object → generics
//
// Pergunta que este passo responde:
//   "Por que generics existem? O que havia antes?"
// =====================================================================
public static class Passo03
{
    public static void Executar()
    {
        Console.WriteLine("1) Uma classe por tipo:");
        Console.WriteLine($"  {new CaixaDeInteiro { Valor = 10 }}  {new CaixaDeTexto { Valor = "oi" }}");
        Console.WriteLine("  (funciona, mas é o mesmo código repetido para cada tipo)");

        Console.WriteLine();
        Console.WriteLine("2) object para tudo:");
        var caixaObj = new CaixaObject { Valor = 10 };
        Console.WriteLine($"  {caixaObj}");
        Console.WriteLine("  (compila com qualquer coisa... inclusive o erro abaixo)");

        // ERRO EM TEMPO DE EXECUÇÃO — descomente para ver o InvalidCastException:
        // string texto = (string)caixaObj.Valor!;
        // Console.WriteLine(texto);

        Console.WriteLine();
        Console.WriteLine("3) Generics:");
        var inteiro = new Caixa<int>(10);
        var texto = new Caixa<string>("oi");
        var preco = new Caixa<decimal>(19.90m);
        Console.WriteLine($"  {inteiro}  {texto}  {preco}");

        // O compilador sabe que inteiro.Valor é int: pode somar sem cast.
        int dobro = inteiro.Valor * 2;
        Console.WriteLine($"  inteiro.Valor * 2 = {dobro}  (sem cast, sem risco)");

        // ERRO EM TEMPO DE COMPILAÇÃO — descomente e veja que nem roda:
        // string s = inteiro.Valor;

        // EXERCÍCIO: qual dos dois erros você prefere receber: o de execução (2) ou o de compilação (3)?
        // Pense em quem descobre cada um: você, agora, ou o usuário, em produção?
    }
}
