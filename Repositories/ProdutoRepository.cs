using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Repositories
{
    public class ProdutoRepository : IProdutosRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AlterarProduto(Produto produto)
        {
            try
            {
                if(produto is null) throw new ArgumentNullException(nameof(produto));

                var consultaProd = _context.Produtos.Find(produto.Id) ?? throw new ArgumentException($"Produto com ID {produto.Id} não encontrado na base de dados.");
                
                var preco = produto.Preco2.Replace("R$ ", "").Replace(".", "");
                produto.Preco = decimal.Parse(preco, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                produto.DataCadastro = DateTime.Now;
                
                _context.Entry(consultaProd).CurrentValues.SetValues(produto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Alterar o Produto: {ex.Message}");
            }

        }        
        public Produto CadastrarProduto(Produto produto)
        {
            try
            {
                if (produto is null) throw new ArgumentNullException(nameof (produto));

                produto.Id = Guid.NewGuid();
                produto.Status = true;
                produto.DataCadastro = DateTime.Now;
                
                if(!string.IsNullOrEmpty(produto.Preco2))
                {
                    var preco = produto.Preco2.Replace("R$ ", "").Replace(".", "");
                    produto.Preco = decimal.Parse(preco, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                }                

                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return produto;

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao Cadastrar o Produto: {ex.Message}");
            }
        }
        public void DeletarProdudo(Guid idProduto)
        {
            try
            {
                var produto = _context.Produtos.Where(x => x.Id == idProduto).FirstOrDefault();

                if (produto is null) throw new ArgumentNullException(nameof(produto));

                produto.Status = false;

                _context.Entry(produto).CurrentValues.SetValues(produto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Deletar o Produto: {ex.Message}");
            }
        }
        public IQueryable<Fornecedor> ObterFornecedores()
        {
            try
            {
                return _context.Fornecedores.Where(f => f.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Fornecedores: {ex.Message}");
            }
        }
        public IQueryable<Produto> ObterProdutoPorFornecedor(Guid idFornecedor)
        {
            try
            {
                if (idFornecedor == null) throw new ArgumentException(nameof(idFornecedor));

                return _context.Produtos.Where(p => p.IdFornecedor == idFornecedor).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter a lista de produtos do fornecedor com id: {idFornecedor}");
            }
        }
        public Produto ObterProdutoPorId(string id)
        {
            try
            {
                if(string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

                Guid idProduto = Guid.Parse(id);

                return _context.Produtos.Where(p => p.Id == idProduto).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao Obter o Produto: {ex.Message}"); ;
            }            
        }
        public IQueryable<Produto> ObterProdutos()
        {
            try
            {
                return _context.Produtos.Where(p => p.Status == true).AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Obter os Produtos: {ex.Message}"); ;
            }
        }
        public IQueryable<Produto> ObterProdutos(Produto filtro)
        {
            try
            {
                IQueryable<Produto> produtos = _context.Produtos;

                if (filtro.Id != Guid.Empty)
                    produtos = produtos.Where(p => p.Id == filtro.Id);

                if (!string.IsNullOrEmpty(filtro.Nome))
                    produtos = produtos.Where(p => p.Nome.Contains(filtro.Nome));

                if (!string.IsNullOrEmpty(filtro.Descricao))
                    produtos = produtos.Where(p => p.Descricao.Contains(filtro.Descricao));

                if (filtro.Preco > 0)
                    produtos = produtos.Where(p => p.Preco == filtro.Preco);
                
                if (filtro.Status != null)
                    produtos = produtos.Where(p => p.Status == filtro.Status);
                else
                    produtos = produtos.Where(p => p.Status == true);

                if (filtro.IdFornecedor != Guid.Empty)
                    produtos = produtos.Where(p => p.IdFornecedor == filtro.IdFornecedor);

                produtos = produtos.OrderByDescending(p => p.DataCadastro);

                produtos = from produto in produtos
                           join fornecedor in _context.Fornecedores on produto.IdFornecedor equals fornecedor.Id
                           select new Produto
                           {
                               Id = produto.Id,
                               Codigo = produto.Codigo,
                               Nome = produto.Nome,
                               Descricao = produto.Descricao,
                               Preco = produto.Preco,
                               DataCadastro = produto.DataCadastro,
                               RazaoSocialFornecedor = fornecedor.RazaoSocial,
                               Status = produto.Status,
                           };
                
                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Listar os Produtos: {ex.Message}"); ;
            }
        }
        public async Task<List<Produto>> ObterProdutosDeTesteAsync()
        {
            // Aqui, você pode criar alguns produtos de teste manualmente
            var produtosDeTeste = new List<Produto>
            {
                new Produto { Id = Guid.NewGuid(), Nome = "Produto 1", Descricao = "Descrição 1", Preco = 19.99m },
                new Produto { Id = Guid.NewGuid(), Nome = "Produto 2", Descricao = "Descrição 2", Preco = 29.99m },
                new Produto { Id = Guid.NewGuid(), Nome = "Produto 3", Descricao = "Descrição 3", Preco = 56.99m },
                
            };           

            return produtosDeTeste;
        }
        public IQueryable<Produto> BuscarProdutos(string descricao, Guid idFornecedor)
        {
            try
            {
                return _context.Produtos.Where(p => p.Descricao.Contains(descricao) && p.IdFornecedor == idFornecedor).AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro na função BuscarProdutos: {ex.Message}",ex);
            }
        }
    }
}
