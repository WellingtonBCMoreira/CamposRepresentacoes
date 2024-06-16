using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Pedidos
{
    public class EditarModel : PageModel
    {
        private readonly IPedidosService _pedidoService;
        private readonly IProdutosService _produtosService;

        public EditarModel(IPedidosService pedidosService, IProdutosService produtosService)
        {
            _pedidoService = pedidosService;
            _produtosService = produtosService;
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

            if (ItensPedido.Count() > 0)
            {
                foreach (var item in ItensPedido)
                {
                    Produto produto = _produtosService.ObterProdutoPorId(id : Convert.ToString(item.IdProduto));

                    var prodPedido = new ProdutosPedido
                    {
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        ValorUnitario = produto.Preco,
                        ValorTotal = Convert.ToDecimal(produto.Preco * item.Quantidade)
                    };
                    ProdutosPedido.Add(prodPedido);
                }
            }

            var prdFornecedor = _produtosService.ObterProdutoPorFornecedor(idFornecedor: Pedido.IdFornecedor);

            Produtos = prdFornecedor.Where(p => !ItensPedido.Any(ip => ip.IdProduto == p.Id));
                       
        }

        public IActionResult OnPostAdicionar(Guid produtoId, Guid pedidoId, int quantidade)
        {
            var produto = _produtosService.ObterProdutoPorId(id: Convert.ToString(produtoId));
            var pedido = _pedidoService.ObterPedidoPorId(pedidoId);
            
            var itemPedido = new ItensPedido
            {
                IdProduto = produto.Id,
                IdPedido = pedido.Id,
                IdFornecedor = pedido.IdFornecedor,
                Preco = produto.Preco * quantidade,
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
    }
}
