using Microsoft.Data.SqlClient;
using SistemaEstoque.Models;
using SistemaEstoque.Interfaces;

namespace SistemaEstoque.Repositories;

public class ProdutoRepository : IProdutoRepository
    {
    private readonly string conexao =
        "Server=localhost\\SQLEXPRESS;Database=SistemaEstoquedb;Trusted_Connection=True;TrustServerCertificate=True;";

    public async Task<List<Produto>> BuscarProdutos()
        {
        var produtos = new List<Produto>();

        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();

        string Sql = "SELECT Id, Codigo, Nome, Quantidade FROM Produtos";

        using SqlCommand command = new SqlCommand(Sql, connection);
        using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            {
            var produto = new Produto();

            produto.Id = reader.GetInt32(0);
            produto.Codigo = reader.GetInt32(1);
            produto.Nome = reader.GetString(2);
            produto.Quantidade = reader.GetInt32(3);

            produtos.Add(produto);
            }

        return produtos;
        }

    public async Task CadastrarProduto(Produto produto)
        {
        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();

        string Sql = @"
            INSERT INTO Produtos (Codigo, Nome, Quantidade)
            VALUES (@Codigo, @Nome, @Quantidade)";

        using SqlCommand command = new SqlCommand(Sql, connection);

        command.Parameters.AddWithValue("@Codigo", produto.Codigo);
        command.Parameters.AddWithValue("@Nome", produto.Nome);
        command.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

        await command.ExecuteNonQueryAsync();
        }

    public async Task AlterarProduto(int id, Produto produto)
        {
        using SqlConnection connection = new SqlConnection(conexao);

        await connection.OpenAsync();

        string Sql = @"
            UPDATE Produtos
            SET Codigo = @Codigo,
                Nome = @Nome,
                Quantidade = @Quantidade
            WHERE Id = @Id";

        using SqlCommand command = new SqlCommand(Sql, connection);

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

        await comando.ExecuteNonQueryAsync();
        }
    }



