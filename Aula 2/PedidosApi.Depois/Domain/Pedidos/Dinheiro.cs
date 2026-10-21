namespace PedidosApi.Depois.Domain.Pedidos;

/// <summary>
/// Objeto de valor: não tem identidade, é imutável e se compara pelo valor.
/// Dois "R$ 10,00" são a mesma coisa.
/// </summary>
public record Dinheiro(decimal Valor, string Moeda = "BRL")
{
    public static Dinheiro Zero(string moeda = "BRL") => new(0, moeda);

    public Dinheiro Somar(Dinheiro outro)
    {
        if (outro.Moeda != Moeda)
            throw new DomainException("Não é possível somar moedas diferentes.");
        return this with { Valor = Valor + outro.Valor };
    }

    public Dinheiro Multiplicar(int quantidade) => this with { Valor = Valor * quantidade };
}
