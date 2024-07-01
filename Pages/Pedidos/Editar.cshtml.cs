using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Pedidos
{
    [Authorize]
    public class EditarModel : PageModel
    {
        private readonly IPedidosService _pedidoService;
        private readonly IProdutosService _produtosService;
        private readonly IClientesService _clientesService;

        public EditarModel(IPedidosService pedidosService, IProdutosService produtosService, IClientesService clientesService)
        {
            _pedidoService = pedidosService;
            _produtosService = produtosService;
            _clientesService = clientesService;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }
        public IQueryable<ItensPedido> ItensPedido { get; set; }
        
        [BindProperty]
        public string Confirmacao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Cliente Cliente { get; set; }
        public Transportadora Transportadora { get; set; }
        public List<ProdutosPedido> ProdutosPedido { get; set; } 
        public IQueryable<Produto> Produtos { get; set; }     
        
        public void OnGet(Guid idPedido)
        {
            ProdutosPedido = new List<ProdutosPedido>();
            if (TempData.ContainsKey("Pedido"))
            {
                string pedidoJson = TempData["Pedido"] as string;

                Pedido = JsonConvert.DeserializeObject<Pedido>(pedidoJson);
            }
            else
            {
                Pedido = _pedidoService.ObterPedidoPorId(idPedido);
            }

            ItensPedido = _pedidoService.ObterItensPedido(idPedido);
            Cliente = _clientesService.ObterClientePeloId(Convert.ToString(Pedido.IdCliente));

            if (ItensPedido.Count() > 0)
            {
                foreach (var item in ItensPedido)
                {
                    Produto produto = _produtosService.ObterProdutoPorId(id : Convert.ToString(item.IdProduto));

                    var prodPedido = new ProdutosPedido
                    {
                        IdProduto = produto.Id,
                        Codigo = produto.Codigo,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        ValorUnitario = produto.Preco,
                        ValorTotal = item.Preco,
                        TotalProduto = item.Quantidade
                    };
                    ProdutosPedido.Add(prodPedido);
                }
            }

            var prdFornecedor = _produtosService.ObterProdutoPorFornecedor(idFornecedor: Pedido.IdFornecedor);

            Produtos = prdFornecedor.Where(p => !ItensPedido.Any(ip => ip.IdProduto == p.Id));
                       
        }

        public IActionResult OnPostAdicionar(Guid produtoId, Guid pedidoId, int quantidade, string desconto)
        {
            var produto = _produtosService.ObterProdutoPorId(id: Convert.ToString(produtoId));
            var pedido = _pedidoService.ObterPedidoPorId(pedidoId);

            decimal percentualDesconto, precoComDesconto;

            if (!string.IsNullOrEmpty(desconto) && decimal.TryParse(desconto, out percentualDesconto))
            {                
                percentualDesconto = percentualDesconto / 100;
                             
                precoComDesconto = produto.Preco * quantidade * (1 - percentualDesconto);
            }
            else
            {                
                precoComDesconto = produto.Preco * quantidade;
            }

            var itemPedido = new ItensPedido
            {
                IdProduto = produto.Id,
                IdPedido = pedido.Id,
                IdFornecedor = pedido.IdFornecedor,
                Preco = precoComDesconto,
                Quantidade = quantidade
            };

            _pedidoService.InserirItens(itemPedido);

            return RedirectToPage(new {idPedido = pedidoId});
        }

        public IActionResult OnPostSalvarItemPedido(ItensPedido itensPedido)
        {
            _pedidoService.InserirItens(itensPedido);
            return Page();
        }

        public IActionResult OnPostDeletarItem(Guid idPedido ,Guid idProduto)
        {
            _pedidoService.DeletarItemPedido(idPedido, idProduto);

            MensagemAlerta.SetMensagem("MensagemExclusao", "Item excluido com sucesso!");

            return RedirectToPage("/Pedidos/Editar", new { idPedido });
        }

        public IActionResult OnPostConfirmar(Guid idPedido, string observacao)
        { 
            _pedidoService.ConfirmarPedido(idPedido, observacao);

            return RedirectToPage("/Pedidos/Detalhes", new { idPedido });
        }        
    }
}
