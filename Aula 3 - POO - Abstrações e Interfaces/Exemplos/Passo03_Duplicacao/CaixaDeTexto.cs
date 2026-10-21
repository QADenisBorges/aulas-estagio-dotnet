namespace Exemplos.Passo03_Duplicacao;

/// <summary>TENTATIVA 1 (continuação): duplicação. E viriam CaixaDeDecimal, CaixaDeProduto...</summary>
public class CaixaDeTexto
{
    public string Valor { get; set; } = "";
    public override string ToString() => $"Caixa[{Valor}]";
}
