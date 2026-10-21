namespace Exemplos.Passo03_Duplicacao;

/// <summary>TENTATIVA 3: generics. Uma classe, infinitos tipos, segurança total.</summary>
public class Caixa<T>
{
    public T Valor { get; set; }
    public Caixa(T valor) => Valor = valor;
    public override string ToString() => $"Caixa<{typeof(T).Name}>[{Valor}]";
}
