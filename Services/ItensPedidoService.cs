using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Services
{
    public class ItensPedidoService : IItensPedidosService
    {
        private readonly IItensPedidosRepository _itensPedidoRepository;

        public ItensPedidoService(IItensPedidosRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public void AlterarItemPedido(ItensPedido itens)
        {
            _itensPedidoRepository.AlterarItemPedido(itens);
        }

        public ItensPedido CadastrarItensPedido(ItensPedido itens)
        {
            return _itensPedidoRepository.CadastrarItensPedido(itens);
        }

        public void DeletarItemPedido(Guid idPedido, Guid idItem)
        {
            _itensPedidoRepository.DeletarItemPedido(idPedido,idItem);
        }

        public IQueryable<ItensPedido> ObterDetalheDoPedido(ItensPedido idPedido)
        {
            return _itensPedidoRepository.ObterDetalheDoPedido(idPedido);
        }
    }
}
