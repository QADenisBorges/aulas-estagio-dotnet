using ProdutosApi.Models;

namespace ProdutosApi.Interface;

public interface IProdutoRepository
{
    public Produto Get(int id);
    public void Insert(Produto produto);
    public void Update(Produto produto);
}