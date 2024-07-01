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
    public class CadastrarModel : PageModel
    {
        private readonly IPedidosService _pedidosService;
        private readonly IProdutosService _produtosService;

        public CadastrarModel(IPedidosService pedidosService, IProdutosService produtosService)
        {
            _pedidosService = pedidosService;
            _produtosService = produtosService;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }
        public List<ItensPedido> ItensPedido { get; set; }

        [BindProperty]
        public string Confirmacao { get; set; }

        public IQueryable<Fornecedor> Fornecedores { get; set; }
        public IQueryable<Cliente> Clientes { get; set; }
        public IQueryable<Transportadora> Transportadoras { get; set; }
        public IQueryable<Produto> Produtos { get; set; }
        public IQueryable<Pedido> Pedidos { get; set; }

        public void OnGet()
        {
            Clientes = _pedidosService.ObterClientes();
            Fornecedores = _pedidosService.ObterFornecedores();
            Transportadoras = _pedidosService.ObterTransportadoras();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostSalvarPedido()
        {
            var pedido = _pedidosService.CadastrarCapaPedido(Pedido);

            if (pedido == null)
            {
                return new JsonResult(new { success = false });
            }

            string pedidoJson = JsonConvert.SerializeObject(pedido);

            TempData["Pedido"] = pedidoJson;

            return RedirectToPage("/Pedidos/Editar", new { idPedido = pedido.Id });
                                    
        }
    }
}
