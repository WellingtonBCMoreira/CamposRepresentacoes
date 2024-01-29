using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface ITransportadorasRepository
    {
        Transportadora CadastrarTransportadora(Transportadora transportadora);
        void AlterarTransportadora(Transportadora transportadora);
        void AtivarDesativarTransportadora(Guid transportadoraId, bool status);
        Transportadora ObterTransportadoraPeloId(string id);
        IQueryable<Transportadora> ObterTransportadoras();
        IQueryable<Transportadora> ObterTransportadoras(Transportadora filtro);
        Task<List<Transportadora>> ObterTransportadorasAsync();
    }
}
