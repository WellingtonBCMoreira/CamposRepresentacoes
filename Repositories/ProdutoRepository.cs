﻿using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;
using Microsoft.Data.SqlClient;

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

                _context.Entry(produto).CurrentValues.SetValues(produto);
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
                return _context.Fornecedores.Where(f => f.Ativo == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Fornecedores: {ex.Message}");
            }
        }

        public Produto ObterProdutoPorId(Guid id)
        {
            try
            {
                if(id == Guid.Empty) throw new ArgumentNullException(nameof(id));

                return (Produto)_context.Produtos.Where(p => p.Id == id);
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
    }
}
