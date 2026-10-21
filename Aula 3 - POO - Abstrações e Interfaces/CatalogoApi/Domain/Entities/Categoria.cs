using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

/// <summary>
/// Segunda entidade. Existe para PROVAR que o repositório genérico é reutilizável:
/// nenhuma linha nova foi escrita em Repository/ para guardá-la.
/// </summary>
public class Categoria : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }

    public Categoria(string nome, string? descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome da categoria é obrigatório.");

        Nome = nome.Trim();
        Descricao = descricao?.Trim() ?? "";
    }

    public void Renomear(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new DomainException("Nome da categoria é obrigatório.");

        Nome = novoNome.Trim();
    }
}
