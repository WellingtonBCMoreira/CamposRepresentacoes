using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface IProdutosRepository
    {
        Produto CadastrarProduto(Produto produto);
        void AlterarProduto(Produto produto);
        void DeletarProdudo(Guid idProduto);
        Produto ObterProdutoPorId(Guid id);
        IQueryable<Produto> ObterProdutos();
        IQueryable<Produto> ObterProdutos(Produto filtro);
        IQueryable<Fornecedor> ObterFornecedores();
        Task<List<Produto>> ObterProdutosDeTesteAsync();
    }
}
