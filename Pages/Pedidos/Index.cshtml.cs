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

        [BindProperty(Name = "pedido", SupportsGet = true)]
        public Pedido Pedido { get; set; }
        public IQueryable<Fornecedor> Fornecedores { get; set; }
        public IQueryable<Cliente> Clientes { get; set; }        
        public IQueryable<Pedido> Pedidos { get; set; }


        public void OnGet()
        {
            Fornecedores = _pedidosService.ObterFornecedores();
            Clientes = _pedidosService.ObterClientes();
            Pedidos = _pedidosService.ObterPedidos();
                     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostExcluir(Guid id)
        {
            _pedidosService.DeletarPedido(id);

            MensagemAlerta.SetMensagem("MensagemExclusao", "Pedido excluído com sucesso!");

            return RedirectToPage(); 
        }
    }
}
