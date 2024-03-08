using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Pedidos
{
    public class DetalhesModel : PageModel
    {
        private readonly IPedidosService _pedidosService;

        public DetalhesModel(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
        }
        [BindProperty]
        public Pedido Pedido { get; set; }
        public List<ItensPedido> ItensPedido { get; set; }
        public ItensPedido ItemPedido { get; set; }

        [BindProperty]
        public string Confirmacao { get; set; }

        public IQueryable<Fornecedor> Fornecedores { get; set; }
        public IQueryable<Cliente> Clientes { get; set; }
        public IQueryable<Transportadora> Transportadoras { get; set; }
        public IQueryable<Produto> Produtos { get; set; }
        public IQueryable<Pedido> Pedidos { get; set; }
        public string ItensPedidoJson { get; set; }
        public IActionResult OnGet(Guid id)
        {
            if (TempData.ContainsKey("Pedido"))
            {
                string pedidoJson = TempData["Pedido"] as string;

                Pedido = JsonConvert.DeserializeObject<Pedido>(pedidoJson);

                Produtos = _pedidosService.ObterProdutos(Convert.ToString(Pedido.IdFornecedor));
            }
            else
            {
                Pedido = _pedidosService.ObterPedidoPorId(id);

                Produtos = _pedidosService.ObterProdutos(Convert.ToString(Pedido.IdFornecedor));
            }
            return Page();
        }
        [HttpPost]
        public IActionResult AdicionarItemPedido(Guid pedidoId, Guid produtoId)
        {
            var item = _pedidosService.ObterProdutos(Convert.ToString(produtoId));
            _pedidosService.InserirItens(ItemPedido);
            return Page();
        }

        //[HttpPost]
        //public IActionResult AtualizarQuantidadeItemPedido(Guid itemPedidoId, int quantidade)
        //{
        //    _pedidosService.AtualizarQuantidadeItemPedido(itemPedidoId, quantidade);
        //    return Ok();
        //}

        //[HttpPost]
        //public IActionResult RemoverItemPedido(Guid itemPedidoId)
        //{
        //    _pedidosService.RemoverItemPedido(itemPedidoId);
        //    return Ok();
        //}

        //[HttpPost]
        //public IActionResult SalvarItensPedido(List<ItensPedido> itensPedido)
        //{
        //    _pedidosService.SalvarItensPedido(itensPedido);
        //    return Ok();
        //}
    }
}
