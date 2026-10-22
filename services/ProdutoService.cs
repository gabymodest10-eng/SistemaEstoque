using SistemaEstoque.Models;
using SistemaEstoque.Repositories;

namespace SistemaEstoque.Services;

public class ProdutoService
{
    private readonly ProdutoRepository _repository;

    public ProdutoService(ProdutoRepository repository)
    {
        _repository = repository;
    }
}