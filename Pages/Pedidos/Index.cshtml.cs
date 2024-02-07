using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly IPedidosService _pedidosService;

        public IndexModel(IPedidosService pedidosService)
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

        public void OnGet()
        {
            Fornecedores = _pedidosService.ObterFornecedores();
            Clientes = _pedidosService.ObterClientes();
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

                return Page();
            }
            catch (Exception ex)
            {               
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }

        public IActionResult OnGetObterProdutosPorFornecedor(string IdFornecedor)
        {
            // Lógica para buscar produtos por fornecedor (substituir com sua lógica real)
            var produtos = _pedidosService.ObterProdutos(IdFornecedor);

            var listadeProdutos = produtos.Select(produto => new
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
                // Adicione outros campos conforme necessário
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

        public IActionResult OnPostCancelarPedido()
        {

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
            _pedidosService.CriarPedido(Pedido,ItensPedido);
        }
    }
}
