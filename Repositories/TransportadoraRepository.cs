using CamposRepresentacoes.Data;
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

                _context.Entry(transportadora).CurrentValues.SetValues(transportadora);
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
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == transportadoraId);

                if (cliente is null) new ArgumentNullException(nameof(cliente));

                cliente.Ativo = status;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao desativar o Cliente: {ex.Message}");
            }
        }

        public Transportadora ObterTransportadoraPeloCnpj(string cnpj)
        {
            try
            {
                if (cnpj is null) new ArgumentNullException(nameof(cnpj));

                return _context.Transportadoras.Where(c => c.CNPJ == cnpj).FirstOrDefault();

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

                if (filtro.Ativo == true || filtro.Ativo == false)
                    transportadoras = transportadoras.Where(c => c.Ativo == filtro.Ativo);

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
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 1", CNPJ = "11111111111111", Rua = "Teste 1", Bairro = "Teste 1", Cidade = "Teste 1", Numero = 1, Complemento = string.Empty, CEP = "11111111", Telefone = "1111111111", Ativo = true},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 2", CNPJ = "22222222222222", Rua = "Teste 2", Bairro = "Teste 2", Cidade = "Teste 2", Numero = 2, Complemento = string.Empty, CEP = "22222222", Telefone = "2222222222", Ativo = true},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 3", CNPJ = "33333333333333", Rua = "Teste 3", Bairro = "Teste 3", Cidade = "Teste 3", Numero = 3, Complemento = string.Empty, CEP = "33333333", Telefone = "3333333333", Ativo = false},
                new Transportadora {Id = Guid.NewGuid(), RazaoSocial = "Teste 4", CNPJ = "44444444444444", Rua = "Teste 4", Bairro = "Teste 4", Cidade = "Teste 4", Numero = 4, Complemento = string.Empty, CEP = "44444444", Telefone = "4444444444", Ativo = false}

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
