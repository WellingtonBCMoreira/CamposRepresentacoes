using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Pedidos
{
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

            var produtos = _produtosService.ObterProdutoPorFornecedor(pedido.IdFornecedor);

            var listaDeProdutos = produtos.Select(p => new
            {
                ProdutoId = p.Id,
                Codigo = p.Codigo,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
            });

            return new JsonResult(new { success = true, pedido = pedido, produtos = listaDeProdutos });

            //TempData["Pedido"] = pedidoJson;

            //return RedirectToPage("/Pedidos/Detalhes");
        }

        //public IActionResult OnGetObterProdutosPorFornecedor(string IdFornecedor)
        //{

        //    var produtos = _pedidosService.ObterProdutos(IdFornecedor);

        //    var listadeProdutos = produtos.Select(produto => new
        //    {                
        //        Nome = produto.Nome,
        //        Descricao = produto.Descricao,
        //        Preco = produto.Preco
        //    });

        //    return new JsonResult(listadeProdutos);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostAdicionarItemPedido([FromBody] ItensPedido item)
        {
            var itemPedido = _pedidosService.InserirItens(item);

            MensagemAlerta.SetMensagem("ItemAdicionado", "Item adicionado com sucesso :)");

            return Page();
        }

        public IActionResult OnPostRemoverItemPedido(string item)
        {
            try
            {
                _pedidosService.DeletarItemPedido(item);

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }

        public IActionResult OnPostCancelarPedido(string id)
        {
            _pedidosService.DeletarItemPedido(id);
            return Page();
        }

        private decimal CalcularValorTotalItem(string idProduto, int quantidade)
        {
            var produto = _pedidosService.ObterProdutos(idProduto).FirstOrDefault();
            return produto.Preco * quantidade;
        }

        public void AtualizarValorTotalPedido()
        {
            Pedido.ValorTotal = Pedido.ItensPedido.Sum(item => item.Quantidade);
        }


    }
}
