namespace Exemplos.Passo02_Contrato;

public class FreteTransportadora : ICalculadoraFrete
{
    public string Nome => "Transportadora";
    public decimal Calcular(decimal pesoKg) => pesoKg <= 5 ? 30m : 30m + (pesoKg - 5) * 4m;
}
