using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IFornecedoresService
    {
        Fornecedor CadastrarFornecedor(Fornecedor fornecedor);
        void AlterarFornecedor(Fornecedor fornecedor);
        void AtivarDesativarFornecedor(Guid fornecedorId, bool status);
        Fornecedor ObterFornecedorPorId(string id);
        IQueryable<Fornecedor> ObterFornecedores();
        IQueryable<Fornecedor> ObterFornecedores(Fornecedor filtro);        
    }
}
