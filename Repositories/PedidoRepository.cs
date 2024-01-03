using CamposRepresentacoes.Data;
using CamposRepresentacoes.Interfaces.Repositories;
using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CamposRepresentacoes.Repositories
{
    public class PedidoRepository : IPedidosRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AlterarCapaPedido(Pedido pedido)
        {
            try
            {
                if(pedido is null) new ArgumentNullException(nameof(pedido));

                _context.Entry(pedido).CurrentValues.SetValues(pedido);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public Pedido CadastrarCapaPedido(Pedido pedido)
        {
            try
            {
                if (pedido is null) new ArgumentNullException(nameof(pedido));

                _context.Pedidos.Add(pedido);
                _context.SaveChanges();

                return pedido;

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao cadastar a Capa do Pedido: {ex.Message}");
            }
        }

        public Pedido CriarPedido(Pedido pedido, List<ItensPedido> itensPedido)
        {
            ValidarPedidoComRelacionamentos(pedido, itensPedido);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (pedido == null)
                        throw new ArgumentNullException(nameof(pedido));

                    if (itensPedido == null || itensPedido.Count == 0)
                        throw new ArgumentNullException(nameof(itensPedido), "A lista de itens do pedido não pode ser nula ou vazia.");

                    // Adiciona a capa do pedido
                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();

                    // Associa os itens ao pedido
                    foreach (var item in itensPedido)
                    {
                        item.IdPedido = pedido.Id;
                        _context.ItensPedido.Add(item);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return pedido;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao criar o pedido: {ex.Message}");
                }
            }
        }

        private void ValidarPedidoComRelacionamentos(Pedido pedido, List<ItensPedido> itensPedido)
        {
            if (pedido.IdCliente == Guid.Empty)
            {
                throw new ArgumentException("O pedido deve ter um cliente associado.");
            }

            if (pedido.IdFornecedor == Guid.Empty)
            {
                throw new ArgumentException("O pedido deve ter um fornecedor associado.");
            }

            if (pedido.IdTransportadora == Guid.Empty)
            {
                throw new ArgumentException("O pedido deve ter uma transportadora associada.");
            }

            if (itensPedido == null || itensPedido.Count == 0)
            {
                throw new ArgumentException("O pedido deve ter pelo menos um item associado.");
            }
        }

        public void DeletarPedido(Guid idPedido)
        {
            try
            {
                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == idPedido);

                if (pedido is null) new ArgumentNullException(nameof(pedido));

                _context.Pedidos.Remove(pedido);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar deletar o Pedido: {ex.Message}");
            }            
        }

        public void InserirItens(ItensPedido itensPedido)
        {
            try
            {
                if (itensPedido is null) new ArgumentNullException(nameof(itensPedido));

                _context.ItensPedido.Add(itensPedido);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao inserir o item {itensPedido.Produto.Nome}, erro: {ex.Message}");
            }
        }

        public Pedido ObterPedidoPorId(Guid IdPedido)
        {
            try
            {
                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == IdPedido);
                
                if(pedido is null) new ArgumentNullException(nameof(pedido));

                return pedido;
            }
            catch (Exception ex )
            {

                throw new Exception($"Erro ao tentar obter o Pedido: {ex.Message}");
            };
        }

        public IQueryable<Pedido> ObterPedidos()
        {
            try
            {
                return _context.Pedidos.Where(p => p.Status != "Cancelado");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar obter os Pedidos");
            }
        }

        public IQueryable<Pedido> ObterPedidos(Pedido filtro)
        {
            try
            {
                IQueryable<Pedido> pedidos = _context.Pedidos;

                if (filtro.Id != Guid.Empty)
                    pedidos = pedidos.Where(p => p.Id == filtro.Id);
                else
                {
                    if (filtro.IdCliente != Guid.Empty)
                        pedidos = pedidos.Where(p => p.IdCliente == filtro.IdCliente);
                    if (filtro.IdFornecedor != Guid.Empty)
                        pedidos = pedidos.Where(p => p.IdFornecedor == filtro.IdFornecedor);
                    if (filtro.IdTransportadora != Guid.Empty)
                        pedidos = pedidos.Where(p => p.IdTransportadora == filtro.IdTransportadora);                    
                    if (filtro.ValorTotal != null)
                        pedidos = pedidos.Where(p => p.ValorTotal == filtro.ValorTotal);
                    if (!string.IsNullOrEmpty(filtro.Status))
                        pedidos = pedidos.Where(p => p.Status == filtro.Status);
                }
                
                return pedidos;                    
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar listar os Pedidos com base no filtro!");
            }
        }

        public Task<List<Pedido>> ObterPedidosTesteAsync()
        {
            throw new NotImplementedException();
        }

        IQueryable<Cliente> IPedidosRepository.ObterClientes()
        {
            try
            {
                return _context.Clientes.Where(c => c.Ativo == true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter os Clientes: {ex.Message}");
            }
        }

        IQueryable<Fornecedor> IPedidosRepository.ObterFornecedores()
        {
            try
            {
                return _context.Fornecedores.Where(f => f.Ativo == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Fornecedores: {ex.Message}");
            }
        }

        IQueryable<Produto> IPedidosRepository.ObterProdutos(Guid? idFornecedor)
        {
            try
            {
                return _context.Produtos                    
                    .Where(p => p.Status == true && p.IdFornecedor == idFornecedor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Produtos: {ex.Message}");
            }
        }

        IQueryable<Transportadora> IPedidosRepository.ObterTransportadoras()
        {
            try
            {
               return _context.Transportadoras.Where(t => t.Ativo == true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter as Transpostadoras");
            }
        }
    }
}
