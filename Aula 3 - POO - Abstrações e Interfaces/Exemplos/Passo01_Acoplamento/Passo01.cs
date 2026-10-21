namespace Exemplos.Passo01_Acoplamento;

// =====================================================================
// PASSO 1 — Acoplamento e interface
//
// Pergunta que este passo responde:
//   "Por que não posso simplesmente dar new na classe que eu preciso?"
// =====================================================================
public static class Passo01
{
    public static void Executar()
    {
        Console.WriteLine("Versão acoplada (não dá para trocar o envio sem editar o service):");
        new RelatorioServiceAcoplado().Fechar();

        Console.WriteLine();
        Console.WriteLine("Versão desacoplada (quem monta decide a implementação):");

        // A decisão de QUAL notificador usar fica AQUI, fora do service.
        INotificador notificador = new EmailNotificador();
        var service = new RelatorioService(notificador);
        service.Fechar();

        // EXERCÍCIO 1: troque a linha acima por new ConsoleNotificador().
        // Quantos arquivos você precisou alterar? Esse número mede o acoplamento.
        //
        // EXERCÍCIO 2: crie SmsNotificador.cs nesta pasta, implementando INotificador, e use aqui.
        // O RelatorioService precisou saber que SMS existe?
    }
}
