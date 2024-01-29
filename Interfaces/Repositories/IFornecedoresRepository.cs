using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface IFornecedoresRepository
    {
        Fornecedor CadastrarFornecedor(Fornecedor fornecedor);
        void AlterarFornecedor(Fornecedor fornecedor);
        void AtivarDesativarFornecedor(Guid fornecedorId, bool status);
        Fornecedor ObterFornecedorPorId(string id);
        IQueryable<Fornecedor> ObterFornecedores();
        IQueryable<Fornecedor> ObterFornecedores(Fornecedor filtro);
        Task<List<Fornecedor>> ObterFornecedoresAsync();
    }
}
