﻿using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IProdutosService
    {
        Produto CadastrarProduto(Produto produto);
        void AlterarProduto(Produto produto);
        void DeletarProdudo(Guid idProduto);
        Produto ObterProdutoPorId(string id);
        IQueryable<Produto> ObterProdutos();
        IQueryable<Produto> ObterProdutos(Produto filtro);
        IQueryable<Fornecedor> ObterFornecedores();
        Task<List<Produto>> ObterProdutosDeTesteAsync();
        IQueryable<Produto> ObterProdutoPorFornecedor(Guid idFornecedor);
    }
}
