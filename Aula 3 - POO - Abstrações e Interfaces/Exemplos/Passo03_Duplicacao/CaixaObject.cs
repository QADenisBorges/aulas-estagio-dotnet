namespace Exemplos.Passo03_Duplicacao;

/// <summary>TENTATIVA 2: object para tudo. Uma classe só, mas perde a segurança de tipo.</summary>
public class CaixaObject
{
    public object? Valor { get; set; }
    public override string ToString() => $"Caixa[{Valor}]";
}
