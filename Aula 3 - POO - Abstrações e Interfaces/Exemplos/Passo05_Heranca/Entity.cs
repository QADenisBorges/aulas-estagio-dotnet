namespace Exemplos.Passo05_Heranca;

/// <summary>
/// Classe-base de toda entidade.
/// abstract: ninguém faz new Entity(). Ela só existe para ser herdada.
/// protected set: só a própria classe e as filhas podem mexer no Id.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    // Um método comum a TODAS as entidades, escrito uma vez só.
    public override string ToString() => $"{GetType().Name}#{Id.ToString()[..8]}";
}
