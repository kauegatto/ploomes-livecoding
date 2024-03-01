using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Domain;
using MiniPloomes.Domain.Dto;

namespace MiniPloomes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController(IClienteRepository clienteRepository) : ControllerBase
    {
        // GET: api/Conta/email/<Cliente>
        [HttpGet("{email}/Cliente")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClients(string email)
        {
            return Ok(await clienteRepository.GetAll(email));
        }

        // GET api/email/<Cliente>/5
        [HttpGet("{email}/Cliente/{name}")]
        public async Task<ActionResult<Cliente>> Get(string email, string name)
        {
            return Ok(await clienteRepository.GetByNomeAndEmailConta(name,email));
        }

        // POST api/Email<Cliente>
        [HttpPost("{email}/Cliente")]
        public async Task<Cliente> Post(string email, [FromBody] ClienteDto clienteDto)
        {
            var cliente = new Cliente(clienteDto.Nome, DateTime.Now, email, clienteDto.UrlAvatar);
            return await clienteRepository.Create(cliente);
        }

        // PUT api/Conta/<Cliente>/
        [HttpPatch("{email}/Cliente/{name}")]
        public async Task<Cliente> Patch(string  email, String name, [FromBody] ClienteDto clienteDto)
        {
            return await clienteRepository.Update(new Cliente(name, DateTime.Now, email, clienteDto.UrlAvatar));
        }

        // DELETE api/<Cliente>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
