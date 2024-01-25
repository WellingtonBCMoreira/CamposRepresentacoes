using CamposRepresentacoes.Data;
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

                _context.Entry(fornecedor).CurrentValues.SetValues(fornecedor);
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

        public async Task<List<Fornecedor>> ObterFornecedoresAsync()
        {
            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor {Id = Guid.NewGuid(), RazaoSocial = "Teste 1", CNPJ = "11111111111111", Rua = "Teste 1", Bairro = "Teste 1", Cidade = "Teste 1", Numero = 1, Complemento = string.Empty, CEP = "11111111", Telefone = "1111111111", Status = true},
                new Fornecedor {Id = Guid.NewGuid(), RazaoSocial = "Teste 2", CNPJ = "22222222222222", Rua = "Teste 2", Bairro = "Teste 2", Cidade = "Teste 2", Numero = 2, Complemento = string.Empty, CEP = "22222222", Telefone = "2222222222", Status = true},
                new Fornecedor {Id = Guid.NewGuid(), RazaoSocial = "Teste 3", CNPJ = "33333333333333", Rua = "Teste 3", Bairro = "Teste 3", Cidade = "Teste 3", Numero = 3, Complemento = string.Empty, CEP = "33333333", Telefone = "3333333333", Status = false},
                new Fornecedor {Id = Guid.NewGuid(), RazaoSocial = "Teste 4", CNPJ = "44444444444444", Rua = "Teste 4", Bairro = "Teste 4", Cidade = "Teste 4", Numero = 4, Complemento = string.Empty, CEP = "44444444", Telefone = "4444444444", Status = false}

            };

            return fornecedores;
        }

        public Fornecedor ObterFornecedorPorCnpj(string cnpj)
        {
            try
            {
                if (cnpj is null) new ArgumentNullException(nameof(cnpj));

                return _context.Fornecedores.Where(c => c.CNPJ == cnpj).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter o Fornecedor: {ex.Message}"); ;
            }
        }
    }
}
