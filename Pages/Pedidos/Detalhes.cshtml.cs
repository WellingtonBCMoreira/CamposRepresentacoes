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
        private readonly IProdutosService _produtosService;

        public DetalhesModel(IPedidosService pedidosService, IProdutosService produtosService)
        {
            _pedidosService = pedidosService;
            _produtosService = produtosService;
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

                Produtos = _produtosService.ObterProdutoPorFornecedor(Pedido.IdFornecedor);
            }
            else
            {
                Pedido = _pedidosService.ObterPedidoPorId(id);

                Produtos = _produtosService.ObterProdutoPorFornecedor(Pedido.IdFornecedor);
            }
            return Page();
        }  
    }
}
