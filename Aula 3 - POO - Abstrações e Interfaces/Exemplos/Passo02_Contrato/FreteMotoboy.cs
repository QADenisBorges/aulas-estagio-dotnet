namespace Exemplos.Passo02_Contrato;

public class FreteMotoboy : ICalculadoraFrete
{
    public string Nome => "Motoboy";
    public decimal Calcular(decimal pesoKg) => pesoKg <= 3 ? 12m : 999m;   // não leva peso grande
}
