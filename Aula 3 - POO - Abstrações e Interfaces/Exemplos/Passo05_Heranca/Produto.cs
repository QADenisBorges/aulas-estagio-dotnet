namespace Exemplos.Passo05_Heranca;

/// <summary>"Produto É UMA Entity": herda Id e ToString sem declarar.</summary>
public class Produto : Entity
{
    public string Nome { get; }
    public decimal Preco { get; }

    public Produto(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }
}
