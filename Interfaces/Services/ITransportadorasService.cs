using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface ITransportadorasService
    {
        Transportadora CadastrarTransportadora(Transportadora transportadora);
        void AlterarTransportadora(Transportadora transportadora);
        void AtivarDesativarTransportadora(Guid transportadoraId, bool status);
        Transportadora ObterTransportadoraPeloId(string id);
        IQueryable<Transportadora> ObterTranspostadoras();
        IQueryable<Transportadora> ObterTransportadoras(Transportadora filtro);
        Task<List<Transportadora>> ObterTransportadorasAsync();
    }
}
