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

        public IActionResult OnPostConfirmar()
        {
            try
            {
                Pedido.ItensPedido = new List<ItensPedido>();

                Pedido.Id = Guid.NewGuid();
                Pedido.DataEmissao = DateTime.Now;
                Pedido.Status = "Aberto";

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
            return new JsonResult(produtos);
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
    }
}
