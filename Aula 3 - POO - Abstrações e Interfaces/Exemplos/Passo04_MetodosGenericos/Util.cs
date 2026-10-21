namespace Exemplos.Passo04_MetodosGenericos;

/// <summary>Métodos genéricos: o &lt;T&gt; vai no método, e T só existe dentro dele.</summary>
public static class Util
{
    public static void Trocar<T>(ref T a, ref T b)
    {
        T temporario = a;
        a = b;
        b = temporario;
    }

    // Funciona com List<int>, List<string>, List<Produto>... o algoritmo é o mesmo.
    public static T? PrimeiroOuPadrao<T>(List<T> itens)
    {
        return itens.Count == 0 ? default : itens[0];
    }

    // Paginar não precisa saber NADA sobre T. Só conta e pula.
    public static List<T> Paginar<T>(List<T> itens, int pagina, int tamanho)
    {
        return itens.Skip((pagina - 1) * tamanho).Take(tamanho).ToList();
    }

    // Dois parâmetros de tipo: converte uma lista de TOrigem em lista de TDestino
    // usando uma função que o chamador fornece.
    public static List<TDestino> Converter<TOrigem, TDestino>(List<TOrigem> itens, Func<TOrigem, TDestino> conversor)
    {
        var resultado = new List<TDestino>();
        foreach (var item in itens)
            resultado.Add(conversor(item));
        return resultado;
    }
}
