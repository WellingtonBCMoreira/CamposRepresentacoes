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
        public IQueryable<ItensPedido> ItensPedido { get; set; }
        public List<ProdutosPedido> ProdutosPedido { get; set; }

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

            if (ItensPedido.Count() > 0)
            {
                foreach (var item in ItensPedido)
                {
                    Produto produto = _produtosService.ObterProdutoPorId(id: Convert.ToString(item.IdProduto));

                    var prodPedido = new ProdutosPedido
                    {
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        ValorUnitario = produto.Preco,
                        ValorTotal = Convert.ToDecimal(produto.Preco * item.Quantidade),
                        TotalProduto = item.Quantidade,
                    };
                    ProdutosPedido.Add(prodPedido);
                }
            }

            return Page();
        } 
        
        public IActionResult OnPostGerarPdf(Guid idPedido)
        {
            byte[] pdfBytes = GerarPdfDoPedido(idPedido); // Método fictício para gerar o PDF

            return File(pdfBytes, "application/pdf", "Pedido.pdf");

        }

        private byte[] GerarPdfDoPedido(Guid idPedido)
        {
            throw new NotImplementedException();
        }
    }
}
