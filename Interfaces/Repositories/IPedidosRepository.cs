using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Interfaces.Repositories
{
    public interface IPedidosRepository
    {
        Pedido CadastrarCapaPedido(Pedido pedido);
        void InserirItens(ItensPedido itensPedido);
        Pedido CriarPedido(Pedido pedido, List<ItensPedido> itensPedido);
        void AlterarCapaPedido(Pedido pedido);
        void DeletarPedido(Guid idPedido);
        Pedido ObterPedidoPorId(Guid IdPedido);
        IQueryable<Pedido> ObterPedidos();
        IQueryable<Pedido> ObterPedidos(Pedido filtro);
        Task<List<Pedido>> ObterPedidosTesteAsync();
        IQueryable<Fornecedor> ObterFornecedores();
        IQueryable<Cliente> ObterClientes();
        IQueryable<Transportadora> ObterTransportadoras();
        IQueryable<Produto> ObterProdutos(string idFornecedor);
        void DeletarItemPedido(string id);
    }
}
