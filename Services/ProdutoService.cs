using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Services
{
    public class ProdutoService : IProdutosService
    {
        private readonly IProdutosRepository _produtosRepository;

        public ProdutoService(IProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        public void AlterarProduto(Produto produto)
        {
            _produtosRepository.AlterarProduto(produto);
        }

        public Produto CadastrarProduto(Produto produto)
        {
            return _produtosRepository.CadastrarProduto(produto);
        }

        public void DeletarProdudo(Guid idProduto)
        {
            _produtosRepository.DeletarProdudo(idProduto);
        }

        public IQueryable<Fornecedor> ObterFornecedores()
        {
            return _produtosRepository.ObterFornecedores();
        }

        public IQueryable<Produto> ObterProdutoPorFornecedor(Guid idFornecedor)
        {
            return _produtosRepository.ObterProdutoPorFornecedor(idFornecedor);
        }

        public Produto ObterProdutoPorId(string id)
        {
            return _produtosRepository.ObterProdutoPorId(id);
        }

        public IQueryable<Produto> ObterProdutos()
        {
            return _produtosRepository.ObterProdutos();
        }

        public IQueryable<Produto> ObterProdutos(Produto filtro)
        {
            return _produtosRepository.ObterProdutos(filtro);
        }

        public Task<List<Produto>> ObterProdutosDeTesteAsync()
        {
            return _produtosRepository.ObterProdutosDeTesteAsync();
        }
    }
}
