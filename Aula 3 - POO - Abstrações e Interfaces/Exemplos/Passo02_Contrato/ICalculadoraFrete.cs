namespace Exemplos.Passo02_Contrato;

public interface ICalculadoraFrete
{
    string Nome { get; }
    decimal Calcular(decimal pesoKg);
}
