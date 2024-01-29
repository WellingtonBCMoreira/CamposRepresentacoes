﻿using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;

namespace CamposRepresentacoes.Repositories
{
    public class TransportadoraRepository : ITransportadorasRepository
    {
        private readonly ApplicationDbContext _context;

        public TransportadoraRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public void AlterarTransportadora(Transportadora transportadora)
        {
            try
            {
                if (transportadora is null) throw new ArgumentNullException(nameof(transportadora));

                var consultarTransportadora = _context.Transportadoras.Find(transportadora.Id) ?? throw new ArgumentException($"Transportadora com id {transportadora.Id} não encontrado na base de dados.");

                _context.Entry(consultarTransportadora).CurrentValues.SetValues(transportadora);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Alterar o produto: {ex.Message}");
            }
        }

        public void AtivarDesativarTransportadora(Guid transportadoraId, bool status)
        {
            try
            {
                var transportadora = _context.Transportadoras.FirstOrDefault(t => t.Id == transportadoraId);

                if (transportadora is null) new ArgumentNullException(nameof(transportadora));

                transportadora.Status = status;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao desativar o Cliente: {ex.Message}");
            }
        }

        public Transportadora ObterTransportadoraPeloId(string id)
        {
            try
            {
                if (id is null) new ArgumentNullException(nameof(id));

                Guid idTransportadora = Guid.Parse(id);

                return _context.Transportadoras.Where(c => c.Id == idTransportadora).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter o cliente: {ex.Message}"); ;
            }
        }

        public IQueryable<Transportadora> ObterTransportadoras(Transportadora filtro)
        {
            try
            {
                IQueryable<Transportadora> transportadoras = _context.Transportadoras;

                if (filtro.Id != Guid.Empty)
                    transportadoras = transportadoras.Where(c => c.Id == filtro.Id);

                if (!string.IsNullOrEmpty(filtro.RazaoSocial))
                    transportadoras = transportadoras.Where(c => c.RazaoSocial.Contains(filtro.RazaoSocial));

                if (!string.IsNullOrEmpty(filtro.CNPJ))
                    transportadoras = transportadoras.Where(c => c.CNPJ.Contains(filtro.CNPJ));

                if (!string.IsNullOrEmpty(filtro.Rua))
                    transportadoras = transportadoras.Where(c => c.Rua.Contains(filtro.Rua));

                if (!string.IsNullOrEmpty(filtro.Bairro))
                    transportadoras = transportadoras.Where(c => c.Bairro.Contains(filtro.Bairro));

                if (!string.IsNullOrEmpty(filtro.Cidade))
                    transportadoras = transportadoras.Where(c => c.Cidade.Contains(filtro.Cidade));               

                if (!string.IsNullOrEmpty(filtro.CEP))
                    transportadoras = transportadoras.Where(c => c.CEP.Contains(filtro.CEP));

                if (!string.IsNullOrEmpty(filtro.Telefone))
                    transportadoras = transportadoras.Where(c => c.Telefone.Contains(filtro.Telefone));

                if (filtro.Status == true || filtro.Status == false)
                    transportadoras = transportadoras.Where(c => c.Status == filtro.Status);

                return transportadoras;

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Clientes utilizando os filtros: {ex.Message}"); ;
            }
        }

        public async Task<List<Transportadora>> ObterTransportadorasAsync()
        {
            var transportadoras = new List<Transportadora>
            {
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 1", CNPJ = "11111111111111", Rua = "Teste 1", Bairro = "Teste 1", Cidade = "Teste 1", Numero = 1, Complemento = string.Empty, CEP = "11111111", Telefone = "1111111111", Status = true},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 2", CNPJ = "22222222222222", Rua = "Teste 2", Bairro = "Teste 2", Cidade = "Teste 2", Numero = 2, Complemento = string.Empty, CEP = "22222222", Telefone = "2222222222", Status = true},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 3", CNPJ = "33333333333333", Rua = "Teste 3", Bairro = "Teste 3", Cidade = "Teste 3", Numero = 3, Complemento = string.Empty, CEP = "33333333", Telefone = "3333333333", Status = false},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 4", CNPJ = "44444444444444", Rua = "Teste 4", Bairro = "Teste 4", Cidade = "Teste 4", Numero = 4, Complemento = string.Empty, CEP = "44444444", Telefone = "4444444444", Status = false}

            };

            return transportadoras;
        }

        public IQueryable<Transportadora> ObterTransportadoras()
        {
            try
            {
                return _context.Transportadoras.AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Clientes: {ex.Message}");
            }
        }

        public Transportadora CadastrarTransportadora(Transportadora transportadora)
        {
            try
            {
                if (transportadora is null) throw new ArgumentException(nameof(transportadora));

                transportadora.Status= true;

                _context.Transportadoras.Add(transportadora);
                _context.SaveChanges();
                return transportadora;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao cadastrar o Cliente: {ex.Message}"); ;
            }
        }
    }
}
