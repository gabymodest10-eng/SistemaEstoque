using SistemaEstoque.Models;

namespace SistemaEstoque.Interfaces;

public interface IProdutoRepository
    {
    Task<List<Produto>> BuscarProdutos();
    Task CadastrarProduto(Produto produto);
    Task AlterarProduto(int id, Produto produto);
    }
    
   
