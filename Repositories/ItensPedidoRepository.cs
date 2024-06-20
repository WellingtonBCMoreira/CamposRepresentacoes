using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Repositories
{
    public class ItensPedidoRepository : IItensPedidosRepository
    {
        private readonly ApplicationDbContext _context;

        public ItensPedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AlterarItemPedido(ItensPedido itens)
        {
            try
            {
                if (itens is null) new ArgumentNullException(nameof(itens));

                _context.Entry(itens).CurrentValues.SetValues(itens);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar alterar o item. Erro: {ex.Message}");
            }
        }

        public ItensPedido CadastrarItensPedido(ItensPedido itens)
        {
            try
            {
                if (itens is null) new ArgumentNullException(nameof(itens));
                _context.ItensPedido.Add(itens);
                _context.SaveChanges();
                
                return itens;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar adicionar item no pedido. Erro: {ex.Message}");
            }
        }

        public void DeletarItemPedido(Guid idPedido, Guid idItem)
        {
            try
            {
                if (idItem == Guid.Empty) new ArgumentNullException(nameof(idItem));

                var item = _context.ItensPedido.FirstOrDefault(i => i.Id == idItem);

                _context.ItensPedido.Remove(item);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar deletar o item. Erro: {ex.Message}");
            }
        }

        public IQueryable<ItensPedido> ObterDetalheDoPedido(ItensPedido idPedido)
        {
            try
            {
                if (idPedido is null) new ArgumentNullException(nameof(idPedido));

                var itens = _context.ItensPedido.Where(i => i.IdPedido == idPedido.IdPedido);

                return itens;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os itens do Pedido. Erro: {ex.Message}");
            }
        }
    }
}
