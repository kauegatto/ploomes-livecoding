namespace MiniPloomes.Domain;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAll(string emailConta);
    Task<Cliente?> GetByNomeAndEmailConta(String nome, String emailConta);
    Task<Cliente> Create(Cliente cliente);
    Task<Cliente> Update(Cliente cliente);
}