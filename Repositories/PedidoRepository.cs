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

        public Pedido CadastrarCapaPedido(Pedido pPedido)
        {
            try
            {
                if (pPedido is null) new ArgumentNullException(nameof(pPedido));

                var cliente = _context.Clientes.FirstOrDefault(x => x.Id == pPedido.IdCliente);
                var fornecedor = _context.Fornecedores.FirstOrDefault(x => x.Id == pPedido.IdFornecedor);
                var transportadora = _context.Transportadoras.FirstOrDefault(x => x.Id == pPedido.IdTransportadora);

                pPedido.Id = Guid.NewGuid();
                pPedido.DataEmissao = DateTime.Now;
                pPedido.RazaoSocialCliente = cliente.RazaoSocial;
                pPedido.RazaoSocialFornecedor = fornecedor.RazaoSocial;
                pPedido.RazaoSocialTransportadora = transportadora.RazaoSocial;
                pPedido.ValorTotal = 0;
                pPedido.FormaPagamento = pPedido.FormaPagamento;
                pPedido.QuantidadeItens = 0;
                pPedido.Status = "Aberto";

                _context.Pedidos.Add(pPedido);
                _context.SaveChanges();

                return pPedido;

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
                var itens = _context.ItensPedido.Where(ip => ip.IdPedido == idPedido).ToList();

                if (pedido is null) new ArgumentNullException(nameof(pedido));

                pedido.Status = "Cancelado";
                _context.ItensPedido.RemoveRange(itens);
                               
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar deletar o Pedido: {ex.Message}");
            }            
        }

        public ItensPedido InserirItens(ItensPedido itensPedido)
        {
            try
            {
                if (itensPedido is null) new ArgumentNullException(nameof(itensPedido));

                _context.ItensPedido.Add(itensPedido);
                _context.SaveChanges();

                AtualizarDadosPedido(itensPedido);

                return itensPedido;
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
                IQueryable<Pedido> pedidos = _context.Pedidos.Where(p => p.Status != "Cancelado");                

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar obter os Pedidos: {ex.Message}");
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
                return _context.Clientes.Where(c => c.Status == true);
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
                return _context.Fornecedores.Where(f => f.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter Fornecedores: {ex.Message}");
            }
        }

        IQueryable<Produto> IPedidosRepository.ObterProdutos(string idFornecedor)
        {
            try
            {
                Guid id = Guid.Parse(idFornecedor);

                return _context.Produtos                    
                    .Where(p => p.Status == true && p.IdFornecedor == id);
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
               return _context.Transportadoras.Where(t => t.Status == true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao obter as Transpostadoras!");
            }
        }

        public void DeletarItemPedido(string id)
        {
            try
            {
                if (id is null) new ArgumentNullException(nameof(id));

                Guid idItem = Guid.Parse(id);

                _context.Remove(idItem);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<ItensPedido> ObterItensPedido(Guid idPedido)
        {
            try
            {
                return _context.ItensPedido.Where(i => i.IdPedido == idPedido);
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao listar os itens do Pedido!");
            }
        }

        private void AtualizarDadosPedido(ItensPedido itensPedido)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == itensPedido.IdPedido);
            if (pedido != null)
            {
                pedido.QuantidadeItens += itensPedido.Quantidade;
                pedido.ValorTotal += itensPedido.Preco;
                _context.SaveChanges();
            }
        }
    }
}
