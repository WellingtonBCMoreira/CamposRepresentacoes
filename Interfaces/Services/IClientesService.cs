using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IClientesService
    {
        Cliente CadastrarCliente(Cliente cliente);
        void AlterarCliente(Cliente cliente);
        void AtivarDesativarCliente(Guid clienteId, bool status);
        Cliente ObterClientePeloCnpj(string cnpj);
        IQueryable<Cliente> ObterClientes();
        IQueryable<Cliente> ObterClientes(Cliente filtro);
        Task<List<Cliente>> ObterClientesAsync();
    }
}
