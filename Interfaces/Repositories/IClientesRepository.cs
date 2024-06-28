using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface IClientesRepository
    {
        Cliente CadastrarCliente(Cliente cliente);
        void AlterarCliente(Cliente cliente);
        void AtivarDesativarCliente(Guid clienteId, bool status);
        Cliente ObterClientePeloId(string id);
        IQueryable<Cliente> ObterClientes();
        IQueryable<Cliente> ObterClientes(Cliente filtro);        
    }
}
