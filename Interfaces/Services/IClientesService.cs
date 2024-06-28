using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IClientesService
    {
        Cliente CadastrarCliente(Cliente cliente);
        void AlterarCliente(Cliente cliente);
        void AtivarDesativarCliente(Guid clienteId, bool status);
        Cliente ObterClientePeloId(string id);
        IQueryable<Cliente> ObterClientes();
        IQueryable<Cliente> ObterClientes(Cliente filtro);        
    }
}
