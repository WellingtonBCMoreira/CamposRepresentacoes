using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Services
{
    public class FornecedorService : IFornecedoresService
    {
        private readonly IFornecedoresRepository _fornecedoresRepository;

        public FornecedorService(IFornecedoresRepository fornecedoresRepository)
        {
            _fornecedoresRepository = fornecedoresRepository;
        }

        public void AlterarFornecedor(Fornecedor fornecedor)
        {
            _fornecedoresRepository.AlterarFornecedor(fornecedor);
        }

        public void AtivarDesativarFornecedor(Guid fornecedorId, bool status)
        {
            _fornecedoresRepository.AtivarDesativarFornecedor(fornecedorId, status);
        }

        public Fornecedor CadastrarFornecedor(Fornecedor fornecedor)
        {
            return _fornecedoresRepository.CadastrarFornecedor(fornecedor);
        }

        public IQueryable<Fornecedor> ObterFornecedores()
        {
            return _fornecedoresRepository.ObterFornecedores();
        }

        public IQueryable<Fornecedor> ObterFornecedores(Fornecedor filtro)
        {
            return _fornecedoresRepository.ObterFornecedores(filtro);
        }

        public Task<List<Fornecedor>> ObterFornecedoresAsync()
        {
            return _fornecedoresRepository.ObterFornecedoresAsync();
        }

        public Fornecedor ObterFornecedorPorId(string cnpj)
        {
            return _fornecedoresRepository.ObterFornecedorPorId(cnpj);
        }
    }
}
