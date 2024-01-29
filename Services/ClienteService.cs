using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Repositories;

namespace CamposRepresentacoes.Services
{
    public class ClienteService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;

        public ClienteService(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        public void AlterarCliente(Cliente cliente)
        {
            _clientesRepository.AlterarCliente(cliente);
        }

        public Cliente CadastrarCliente(Cliente cliente)
        {
            return _clientesRepository.CadastrarCliente(cliente);
        }

        public void AtivarDesativarCliente(Guid clienteId, bool status)
        {
            _clientesRepository.AtivarDesativarCliente(clienteId, status);
        }

        public Cliente ObterClientePeloId(string id)
        {
            return _clientesRepository.ObterClientePeloId(id);
        }

        public IQueryable<Cliente> ObterClientes()
        {
            return _clientesRepository.ObterClientes();
        }

        public IQueryable<Cliente> ObterClientes(Cliente filtro)
        {
            return _clientesRepository.ObterClientes(filtro);
        }

        public Task<List<Cliente>> ObterClientesAsync()
        {
            return _clientesRepository.ObterClientesAsync();
        }
    }
}
