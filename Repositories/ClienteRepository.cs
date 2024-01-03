using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Repositories
{
    public class ClienteRepository : IClientesRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AlterarCliente(Cliente cliente)
        {
            try
            {
                if (cliente is null) throw new ArgumentNullException(nameof(cliente));

                _context.Entry(cliente).CurrentValues.SetValues(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Alterar o produto: {ex.Message}");
            }
        }

        public Cliente CadastrarCliente(Cliente cliente)
        {
            try
            {
                if (cliente is null) throw new ArgumentException(nameof(cliente));

                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return cliente;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao cadastrar o Cliente: {ex.Message}"); ;
            }
        }

        public void AtivarDesativarCliente(Guid clienteId, bool status)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);

                if (cliente is null) new ArgumentNullException(nameof(cliente));

                cliente.Ativo = status;
                               
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao desativar o Cliente: {ex.Message}"); 
            }
        }

        public Cliente ObterClientePeloCnpj(string cnpj)
        {
            try
            {
                if (cnpj is null) new ArgumentNullException(nameof(cnpj));

                return _context.Clientes.Where(c => c.CNPJ == cnpj).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter o cliente: {ex.Message}"); ;
            }
        }

        public IQueryable<Cliente> ObterClientes()
        {
            try
            {
                return _context.Clientes.AsQueryable();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Clientes: {ex.Message}");
            }
        }

        public IQueryable<Cliente> ObterClientes(Cliente filtro)
        {
            try
            {
                IQueryable<Cliente> clientes = _context.Clientes;

                if (filtro.Id != Guid.Empty)
                    clientes = clientes.Where(c => c.Id == filtro.Id);

                if(!string.IsNullOrEmpty(filtro.RazaoSocial))
                    clientes = clientes.Where(c => c.RazaoSocial.Contains(filtro.RazaoSocial));
                
                if (!string.IsNullOrEmpty(filtro.CNPJ))
                    clientes = clientes.Where(c => c.CNPJ.Contains(filtro.CNPJ));
                
                if (!string.IsNullOrEmpty(filtro.Rua))
                    clientes = clientes.Where(c => c.Rua.Contains(filtro.Rua));
                
                if (!string.IsNullOrEmpty(filtro.Bairro))
                    clientes = clientes.Where(c => c.Bairro.Contains(filtro.Bairro));
                
                if (!string.IsNullOrEmpty(filtro.Cidade))
                    clientes = clientes.Where(c => c.Cidade.Contains(filtro.Cidade));
                
                if (filtro.Numero != null)
                    clientes = clientes.Where(c => c.Numero == filtro.Numero);
                
                if (!string.IsNullOrEmpty(filtro.Complemento))
                    clientes = clientes.Where(c => c.Complemento.Contains(filtro.Complemento));
                
                if (!string.IsNullOrEmpty(filtro.CEP))
                    clientes = clientes.Where(c => c.CEP.Contains(filtro.CEP));
                
                if (!string.IsNullOrEmpty(filtro.Telefone))
                    clientes = clientes.Where(c => c.Telefone.Contains(filtro.Telefone));
                
                if (filtro.Ativo == true || filtro.Ativo == false)
                    clientes = clientes.Where(c => c.Ativo == filtro.Ativo);

                return clientes;

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Clientes utilizando os filtros: {ex.Message}"); ;
            }
        }

        public async Task<List<Cliente>> ObterClientesAsync()
        {
            var clientes = new List<Cliente>
            {
                new Cliente {Id = Guid.NewGuid(), RazaoSocial = "Teste 1", CNPJ = "11111111111111", Rua = "Teste 1", Bairro = "Teste 1", Cidade = "Teste 1", Numero = 1, Complemento = string.Empty, CEP = "11111111", Telefone = "1111111111", Ativo = true},
                new Cliente {Id = Guid.NewGuid(), RazaoSocial = "Teste 2", CNPJ = "22222222222222", Rua = "Teste 2", Bairro = "Teste 2", Cidade = "Teste 2", Numero = 2, Complemento = string.Empty, CEP = "22222222", Telefone = "2222222222", Ativo = true},
                new Cliente {Id = Guid.NewGuid(), RazaoSocial = "Teste 3", CNPJ = "33333333333333", Rua = "Teste 3", Bairro = "Teste 3", Cidade = "Teste 3", Numero = 3, Complemento = string.Empty, CEP = "33333333", Telefone = "3333333333", Ativo = false},
                new Cliente {Id = Guid.NewGuid(), RazaoSocial = "Teste 4", CNPJ = "44444444444444", Rua = "Teste 4", Bairro = "Teste 4", Cidade = "Teste 4", Numero = 4, Complemento = string.Empty, CEP = "44444444", Telefone = "4444444444", Ativo = false}
    
            };

            return clientes;
        }
    }
}
