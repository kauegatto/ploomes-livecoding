using System.Data;
using System.Data.SqlClient;
using System.Net.WebSockets;
using MiniPloomes.Domain;

namespace MiniPloomes.Infrastructure.Repositories;

public class ClienteRepository(string connectionString) : IClienteRepository
{
    public async Task<IEnumerable<Cliente>> GetAll(string emailConta)
    {
        List<Cliente> clientes = new List<Cliente>();
        
        await using var conn = new SqlConnection(connectionString);

        await conn.OpenAsync();
        
        var command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente WHERE Email_Conta=@emailConta";

        var emailParam = new SqlParameter("emailConta", SqlDbType.VarChar);
        emailParam.Value = emailConta;
        command.Parameters.Add(emailParam);

        await using var reader = await command.ExecuteReaderAsync();

        while (reader.Read())
        {
            var nome = reader.GetString(reader.GetOrdinal("Nome"));
            var createdAt = reader.GetDateTime(reader.GetOrdinal("Created_At"));
            var emailDb = reader.GetString(reader.GetOrdinal("Email_Conta"));
            var urlAvatar = reader.GetString(reader.GetOrdinal("UrlAvatar"));

            var cliente = new Cliente(nome, createdAt, emailDb, urlAvatar);
            clientes.Add(cliente);
        }

        await conn.CloseAsync();
        return clientes;
    }

    public async Task<Cliente?> GetByNomeAndEmailConta(string nome, string emailConta)
    {
        await using var conn = new SqlConnection(connectionString);

        await conn.OpenAsync();
        
        var command = conn.CreateCommand();
        command.CommandText = "SELECT * FROM Cliente WHERE Email_Conta=@emailConta AND Nome=@nome";

        var emailParam = new SqlParameter("emailConta", SqlDbType.VarChar);
        emailParam.Value = emailConta;
        command.Parameters.Add(emailParam);
        
        var nomeParam = new SqlParameter("nome", SqlDbType.VarChar);
        nomeParam.Value = nome;
        command.Parameters.Add(nomeParam);

        await using var reader = await command.ExecuteReaderAsync();

        if (reader.Read())
        {
            var nomeDb = reader.GetString(reader.GetOrdinal("Nome"));
            var createdAt = reader.GetDateTime(reader.GetOrdinal("Created_At"));
            var email = reader.GetString(reader.GetOrdinal("Email_Conta"));
            var urlAvatar = reader.GetString(reader.GetOrdinal("UrlAvatar"));
            return new Cliente(nomeDb, createdAt, email, urlAvatar);
            
        }

        await conn.CloseAsync();
        return null;
    }

    public async Task<Cliente> Create(Cliente cliente)
    {
        
        await using var conn = new SqlConnection(connectionString);

        await conn.OpenAsync();
        
        var command = conn.CreateCommand();
        command.CommandText = "INSERT INTO Cliente VALUES (@nome, @createdAt, @emailConta, @urlAvatar)";

        var emailParam = new SqlParameter("emailConta", SqlDbType.VarChar);
        emailParam.Value = cliente.EmailConta;
        command.Parameters.Add(emailParam);
        
        var nomeParam = new SqlParameter("nome", SqlDbType.VarChar);
        nomeParam.Value = cliente.Nome;
        command.Parameters.Add(nomeParam);
        
        var createdAtParam = new SqlParameter("createdAt", SqlDbType.DateTime);
        createdAtParam.Value = cliente.CreatedAt;
        command.Parameters.Add(createdAtParam);
        
        var urlAvatarParam = new SqlParameter("urlAvatar", SqlDbType.VarChar);
        urlAvatarParam.Value = cliente.UrlAvatar;
        command.Parameters.Add(urlAvatarParam);

        await command.ExecuteNonQueryAsync();

        await conn.CloseAsync();
        return cliente;
    }

    public Task<Cliente> Update(Cliente cliente)
    {
        return null;
    }
}