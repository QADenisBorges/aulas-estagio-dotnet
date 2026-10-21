namespace Exemplos.Passo05_Heranca;

public class Cliente : Entity
{
    public string Nome { get; }
    public string Email { get; }

    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
