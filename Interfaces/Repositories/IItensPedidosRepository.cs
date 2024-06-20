using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface IItensPedidosRepository
    {
        ItensPedido CadastrarItensPedido(ItensPedido itens);
        void AlterarItemPedido(ItensPedido itens);
        void DeletarItemPedido(Guid idPedido, Guid idItem);
        IQueryable<ItensPedido> ObterDetalheDoPedido(ItensPedido idPedido);
    }
}
