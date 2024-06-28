using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Services
{
    public class TransportadoraService : ITransportadorasService
    {
        private readonly ITransportadorasRepository _transportadorasRepository;

        public TransportadoraService(ITransportadorasRepository transportadorasRepository)
        {
            _transportadorasRepository = transportadorasRepository;
        }

        public void AlterarTransportadora(Transportadora transportadora)
        {
            _transportadorasRepository.AlterarTransportadora(transportadora);
        }

        public void AtivarDesativarTransportadora(Guid transportadoraId, bool status)
        {
            _transportadorasRepository.AtivarDesativarTransportadora(transportadoraId, status);
        }

        public Transportadora ObterTransportadoraPeloId(string id)
        {
            return _transportadorasRepository.ObterTransportadoraPeloId(id);
        }

        public IQueryable<Transportadora> ObterTransportadoras(Transportadora filtro)
        {
            return _transportadorasRepository.ObterTransportadoras(filtro);
        }       

        public IQueryable<Transportadora> ObterTranspostadoras()
        {
            return _transportadorasRepository.ObterTransportadoras();
        }

        public Transportadora CadastrarTransportadora(Transportadora transportadora)
        {
            return _transportadorasRepository.CadastrarTransportadora(transportadora);
        }
    }
}
