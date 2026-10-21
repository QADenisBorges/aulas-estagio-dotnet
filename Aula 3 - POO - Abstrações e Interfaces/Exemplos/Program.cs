// Aula 3 — Exemplos pequenos e progressivos.
// Cada passo é uma pasta, com uma classe por arquivo e um PassoNN.cs que roda a demonstração.
// Leia a pasta, preveja a saída, depois rode.

using Exemplos.Passo01_Acoplamento;
using Exemplos.Passo02_Contrato;
using Exemplos.Passo03_Duplicacao;
using Exemplos.Passo04_MetodosGenericos;
using Exemplos.Passo05_Heranca;
using Exemplos.Passo06_Restricoes;
using Exemplos.Passo07_InterfaceGenerica;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Titulo("Passo 1 — Acoplamento e interface");
Passo01.Executar();

Titulo("Passo 2 — Um contrato, várias implementações");
Passo02.Executar();

Titulo("Passo 3 — Duplicação → object → generics");
Passo03.Executar();

Titulo("Passo 4 — Métodos genéricos");
Passo04.Executar();

Titulo("Passo 5 — Herança: a classe-base Entity");
Passo05.Executar();

Titulo("Passo 6 — Restrições genéricas: where T : Entity");
Passo06.Executar();

Titulo("Passo 7 — IRepository<T> em miniatura");
Passo07.Executar();

static void Titulo(string texto)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 60));
    Console.WriteLine(texto);
    Console.WriteLine(new string('=', 60));
}
