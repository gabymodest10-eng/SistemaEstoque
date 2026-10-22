using AutoMapper;
using SistemaEstoque.DTOs;
using SistemaEstoque.Models;

namespace SistemaEstoque.Mappings;

public class ProdutoProfile : Profile
    {
    public ProdutoProfile()
        {
        CreateMap<Produto, ProdutoDto>();
        }
    }
