namespace Exemplos.Passo03_Duplicacao;

/// <summary>TENTATIVA 1: uma classe por tipo. Veja CaixaDeTexto: é o mesmo código.</summary>
public class CaixaDeInteiro
{
    public int Valor { get; set; }
    public override string ToString() => $"Caixa[{Valor}]";
}
