using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Services
{
    public class PedidoService : IPedidosService
    {
        private readonly IPedidosRepository _pedidosRepository;

        public PedidoService(IPedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        public void AlterarCapaPedido(Pedido pedido)
        {
            _pedidosRepository.AlterarCapaPedido(pedido);
        }

        public Pedido CadastrarCapaPedido(Pedido pedido)
        {
            return _pedidosRepository.CadastrarCapaPedido(pedido);
        }

        public Pedido CriarPedido(Pedido pedido, List<ItensPedido> itensPedido)
        {
            return _pedidosRepository.CriarPedido(pedido, itensPedido);
        }

        public void DeletarPedido(Guid idPedido)
        {
            _pedidosRepository.DeletarPedido(idPedido);
        }

        public ItensPedido InserirItens(ItensPedido itensPedido)
        {
            return _pedidosRepository.InserirItens(itensPedido);
        }

        public IQueryable<Cliente> ObterClientes()
        {
            return _pedidosRepository.ObterClientes();
        }

        public IQueryable<Fornecedor> ObterFornecedores()
        {
            return _pedidosRepository.ObterFornecedores();
        }

        public Pedido ObterPedidoPorId(Guid IdPedido)
        {
            return _pedidosRepository.ObterPedidoPorId(IdPedido);
        }

        public IQueryable<Pedido> ObterPedidos()
        {
            return _pedidosRepository.ObterPedidos();
        }
        public IQueryable<ItensPedido> ObterItensPedido(Guid idPedido)
        {
            return _pedidosRepository.ObterItensPedido(idPedido);
        }

        public IQueryable<Pedido> ObterPedidos(Pedido filtro)
        {
            return _pedidosRepository.ObterPedidos(filtro);
        }

        public Task<List<Pedido>> ObterPedidosTesteAsync()
        {
            return _pedidosRepository.ObterPedidosTesteAsync();
        }

        public IQueryable<Produto> ObterProdutos(string idFornecedor)
        {
            return _pedidosRepository.ObterProdutos(idFornecedor);
        }

        public IQueryable<Transportadora> ObterTransportadoras()
        {
            return _pedidosRepository.ObterTransportadoras();
        }

        public void DeletarItemPedido(string id)
        {
            _pedidosRepository.DeletarItemPedido(id);
        }

        public void ConfirmarPedido(Guid idPedido)
        {
            _pedidosRepository.ConfirmarPedido(idPedido);
        }
    }
}
