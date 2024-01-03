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
            CarregarDados();
        }
        private void CarregarDados()
        {
            Fornecedores = _pedidosService.ObterFornecedores();
            Clientes = _pedidosService.ObterClientes();
            Transportadoras = _pedidosService.ObterTransportadoras();               

            if (Pedido is not null)
            {
                Produtos = Pedido.IdFornecedor != Guid.Empty
                ? _pedidosService.ObterProdutos(Pedido.IdFornecedor)
                : Enumerable.Empty<Produto>().AsQueryable();
            }
            
        }      

        public IActionResult OnPostConfirmar()
        {
            try
            {
                Pedido.Id = Guid.NewGuid();
                Pedido.DataEmissao = DateTime.Now;
                Pedido.Status = "Aberto";

                _pedidosService.CadastrarCapaPedido(Pedido);
                
                // Limpar dados do pedido
                Pedido = new Pedido();

                return new JsonResult(new { success = true , confirmacao = "Capa do pedido criada com sucesso!" });
            }
            catch (Exception ex)
            {               
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }        
    }
}
