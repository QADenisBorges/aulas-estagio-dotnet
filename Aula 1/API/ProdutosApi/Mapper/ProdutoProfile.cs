using AutoMapper;
using ProdutosApi.Dtos;
using ProdutosApi.Models;

namespace ProdutosApi.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoResponse>();
    }
}