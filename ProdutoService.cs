using SistemaEstoque.Models;
using SistemaEstoque.Exceptions;
using SistemaEstoque.Interfaces;

namespace SistemaEstoque.Services;

public class ProdutoService
    {
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
        {
        _repository = repository;
        }
    public async Task<List<Produto>> BuscarProdutos()
        {
        return await _repository.BuscarProdutos();
        }
    public async Task CadastrarProduto(Produto produto)
        {
        await _repository.CadastrarProduto(produto);
        }
    public async Task AlterarProduto(int id, Produto produto)
        {
        await _repository.AlterarProduto(id, produto);
        }
    }
