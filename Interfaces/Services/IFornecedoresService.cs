using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IFornecedoresService
    {
        Fornecedor CadastrarFornecedor(Fornecedor fornecedor);
        void AlterarFornecedor(Fornecedor fornecedor);
        void AtivarDesativarFornecedor(Guid fornecedorId, bool status);
        Fornecedor ObterFornecedorPorCnpj(string cnpj);
        IQueryable<Fornecedor> ObterFornecedores();
        IQueryable<Fornecedor> ObterFornecedores(Fornecedor filtro);
        Task<List<Fornecedor>> ObterFornecedoresAsync();
    }
}
