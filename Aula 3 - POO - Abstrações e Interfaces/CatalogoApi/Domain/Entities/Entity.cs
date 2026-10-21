namespace CatalogoApi.Domain.Entities;

/// <summary>
/// Classe-base de toda entidade do sistema.
/// Concentra o que é comum: a identidade.
///
/// abstract  -> ninguém cria um "new Entity()" solto; ela existe para ser herdada.
/// protected -> só a própria classe e as filhas podem mexer no Id.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
