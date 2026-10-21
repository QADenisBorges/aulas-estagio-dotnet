using CatalogoApi.Domain.Exceptions;

namespace CatalogoApi.Domain.Entities;

/// <summary>
/// "Produto É UMA Entity": herda Id sem declarar.
///
/// Os setters são privados de propósito: a entidade é a única que sabe
/// como se manter válida (abstração). Quem está fora só chama os métodos.
/// </summary>
public class Produto : Entity
{
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public bool Ativo { get; private set; }

    public Produto(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome do produto é obrigatório.");
        if (preco < 0)
            throw new DomainException("Preço não pode ser negativo.");

        Nome = nome.Trim();
        Preco = preco;
        Ativo = true;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco < 0)
            throw new DomainException("Preço não pode ser negativo.");

        Preco = novoPreco;
    }

    public void Desativar() => Ativo = false;
}
