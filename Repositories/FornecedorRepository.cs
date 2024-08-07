﻿using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Repositories
{
    public class FornecedorRepository : IFornecedoresRepository
    {
        private readonly ApplicationDbContext _context;

        public FornecedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AlterarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (fornecedor is null) throw new ArgumentNullException(nameof(fornecedor));

                var consultarFornecedor = _context.Fornecedores.Find(fornecedor.Id) ?? throw new ArgumentException($"Fornecedor com id {fornecedor.Id} não encontrado na base de dados.");

                _context.Entry(consultarFornecedor).CurrentValues.SetValues(fornecedor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Alterar o produto: {ex.Message}");
            }
        }

        public Fornecedor CadastrarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (fornecedor is null) throw new ArgumentException(nameof(fornecedor));

                fornecedor.Status = true;
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
                return fornecedor;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao cadastrar o Fornecedor: {ex.Message}"); ;
            }
        }

        public void AtivarDesativarFornecedor(Guid fornecedorId, bool status)
        {
            try
            {
                var fornecedor = _context.Fornecedores.FirstOrDefault(c => c.Id == fornecedorId);

                if (fornecedor is null) new ArgumentNullException(nameof(fornecedor));

                fornecedor.Status = status;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao desativar o Fornecedor: {ex.Message}");
            }
        }

        public IQueryable<Fornecedor> ObterFornecedores()
        {
            try
            {
                return _context.Fornecedores.AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Fornecedores: {ex.Message}");
            }
        }

        public IQueryable<Fornecedor> ObterFornecedores(Fornecedor filtro)
        {
            try
            {
                IQueryable<Fornecedor> fornecedores = _context.Fornecedores;

                if (filtro.Id != Guid.Empty)
                    fornecedores = fornecedores.Where(c => c.Id == filtro.Id);

                if (!string.IsNullOrEmpty(filtro.RazaoSocial))
                    fornecedores = fornecedores.Where(c => c.RazaoSocial.Contains(filtro.RazaoSocial));

                if (!string.IsNullOrEmpty(filtro.CNPJ))
                    fornecedores = fornecedores.Where(c => c.CNPJ.Contains(filtro.CNPJ));

                if (!string.IsNullOrEmpty(filtro.Rua))
                    fornecedores = fornecedores.Where(c => c.Rua.Contains(filtro.Rua));

                if (!string.IsNullOrEmpty(filtro.Bairro))
                    fornecedores = fornecedores.Where(c => c.Bairro.Contains(filtro.Bairro));

                if (!string.IsNullOrEmpty(filtro.Cidade))
                    fornecedores = fornecedores.Where(c => c.Cidade.Contains(filtro.Cidade));                               

                if (!string.IsNullOrEmpty(filtro.CEP))
                    fornecedores = fornecedores.Where(c => c.CEP.Contains(filtro.CEP));

                if (!string.IsNullOrEmpty(filtro.Telefone))
                    fornecedores = fornecedores.Where(c => c.Telefone.Contains(filtro.Telefone));

                if (filtro.Status == true || filtro.Status == false)
                    fornecedores = fornecedores.Where(c => c.Status == filtro.Status);

                return fornecedores;

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Fornecedores utilizando os filtros: {ex.Message}"); ;
            }
        }

        public Fornecedor ObterFornecedorPorId(string id)
        {
            try
            {
                if (id is null) new ArgumentNullException(nameof(id));

                Guid idFornecedor = Guid.Parse(id);

                return _context.Fornecedores.Where(c => c.Id == idFornecedor).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter o Fornecedor: {ex.Message}"); ;
            }
        }
    }
}
