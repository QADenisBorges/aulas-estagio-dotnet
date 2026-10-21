namespace Exemplos.Passo02_Contrato;

public class FreteCorreios : ICalculadoraFrete
{
    public string Nome => "Correios";
    public decimal Calcular(decimal pesoKg) => 15m + pesoKg * 2.5m;
}
