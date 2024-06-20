using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Services
{
    public interface IItensPedidosService
    {
        ItensPedido CadastrarItensPedido(ItensPedido itens);
        void AlterarItemPedido(ItensPedido itens);
        void DeletarItemPedido(Guid idPedido, Guid idItem);
        IQueryable<ItensPedido> ObterDetalheDoPedido(ItensPedido idPedido);
    }
}
