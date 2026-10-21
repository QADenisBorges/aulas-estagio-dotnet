namespace Exemplos.Passo06_Restricoes;

/// <summary>Outras restrições comuns, para reconhecer quando aparecerem.</summary>
public static class OutrasRestricoes
{
    // T precisa ser comparável para que exista um "maior".
    public static T Maior<T>(T a, T b) where T : IComparable<T>
        => a.CompareTo(b) >= 0 ? a : b;

    // T precisa ter construtor sem parâmetros para que new T() seja permitido.
    public static T CriarVazio<T>() where T : new()
        => new T();
}
