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
        private readonly IClientesService _clientesService;

        public DetalhesModel(IPedidosService pedidosService, IProdutosService produtosService, IClientesService clientesService)
        {
            _pedidosService = pedidosService;
            _produtosService = produtosService;
            _clientesService = clientesService;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }
        public IQueryable<ItensPedido> ItensPedido { get; set; }
        public List<ProdutosPedido> ProdutosPedido { get; set; }
        public Cliente Cliente { get; set; }

        [BindProperty]
        public string Confirmacao { get; set; }
        
        public IActionResult OnGet(Guid idPedido)
        {
            ProdutosPedido = new List<ProdutosPedido>();
            if (TempData.ContainsKey("Pedido"))
            {
                string pedidoJson = TempData["Pedido"] as string;

                Pedido = JsonConvert.DeserializeObject<Pedido>(pedidoJson);                
            }
            else
            {
                Pedido = _pedidosService.ObterPedidoPorId(idPedido);                
            }

            ItensPedido = _pedidosService.ObterItensPedido(idPedido);

            Cliente = _clientesService.ObterClientePeloId(Convert.ToString( Pedido.IdCliente));

            if (ItensPedido.Count() > 0)
            {
                foreach (var item in ItensPedido)
                {
                    Produto produto = _produtosService.ObterProdutoPorId(id: Convert.ToString(item.IdProduto));

                    var prodPedido = new ProdutosPedido
                    {
                        Codigo = produto.Codigo,
                        Nome = produto.Nome,
                        ValorUnitario = produto.Preco,
                        ValorTotal = Convert.ToDecimal(produto.Preco * item.Quantidade),
                        TotalProduto = item.Quantidade,
                    };
                    ProdutosPedido.Add(prodPedido);
                }
            }

            return Page();
        } 
    }
}
