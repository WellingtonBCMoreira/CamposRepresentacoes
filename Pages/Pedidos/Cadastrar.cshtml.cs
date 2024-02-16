using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Pedidos
{
    public class CadastrarModel : PageModel
    {
        private readonly IPedidosService _pedidosService;

        public CadastrarModel(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
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
            try
            {
                _pedidosService.CadastrarCapaPedido(Pedido);

                MensagemAlerta.SetMensagem("MensagemSucesso", "Pedido salvo com sucesso!!!");

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }

        public IActionResult OnGetObterProdutosPorFornecedor(string IdFornecedor)
        {

            var produtos = _pedidosService.ObterProdutos(IdFornecedor);

            var listadeProdutos = produtos.Select(produto => new
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            });

            return new JsonResult(listadeProdutos);
        }

        public IActionResult OnPostAdicionarItemPedido([FromBody] ItensPedido item)
        {
            try
            {
                ItensPedido ??= new List<ItensPedido>();
                ItensPedido.Add(item);

                _pedidosService.InserirItens(item);

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
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

        public void SalvarPedido()
        {
            _pedidosService.CriarPedido(Pedido, ItensPedido);
        }
    }
}
