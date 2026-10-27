using Microsoft.Data.SqlClient;
using SistemaEstoque.Models;
using SistemaEstoque.Interfaces;
using SistemaEstoque.Exceptions;

namespace SistemaEstoque.Repositories;

public class ProdutoRepository : IProdutoRepository
    {
    private readonly string conexao;
    public ProdutoRepository(IConfiguration configuration)
        {
        conexao = configuration.GetConnectionString("Database")
        ?? throw new InvalidOperationException(
         "A conexão 'Database' não foi configurada.");
        }
    public async Task<List<Produto>> BuscarProdutos()
        {
        var produtos = new List<Produto>();
        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();
        string sql = "SELECT Id, Codigo, Nome, Quantidade From Produtos";
        using SqlCommand command = new SqlCommand(sql, connection);
        using SqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            {
            var produto = new Produto
                {
                Id = reader.GetInt32(0),
                Codigo = reader.GetInt32(1),
                Nome = reader.GetString(2),
                Quantidade = reader.GetInt32(3)
                };
            produtos.Add(produto);
            }
        return produtos;
        }
    public async Task CadastrarProduto(Produto produto)
        {
        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();

        string sql = @"
                INSERT INTO Produtos (Codigo, Nome, Quantidade)
                VALUES (@Codigo, @Nome, @Quantidade)";

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

        await command.ExecuteNonQueryAsync();
        }

    public async Task AlterarProduto(int id, Produto produto)
        {
        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();

        string sql = @"
                UPDATE Produtos
                SET Codigo = @Codigo,
                    Nome = @Nome,
                    Quantidade = @Quantidade
                WHERE Id = @Id";

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

        await command.ExecuteNonQueryAsync();
        }

    public async Task ExcluirProduto(int id)
        {
        using var conexaoSql = new SqlConnection(conexao);

        await conexaoSql.OpenAsync();

        string sql = "DELETE FROM Produtos WHERE Id = @Id";

        using var comando = new SqlCommand(sql, conexaoSql);

        comando.Parameters.AddWithValue("@Id", id);

        int linhasAfetadas = await comando.ExecuteNonQueryAsync();

        if (linhasAfetadas == 0)
            {
            throw new CustomExcepetion("Produto não encontrado.", 404);
            }
        }
    }


        

