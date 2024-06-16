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


        public IActionResult OnGet(Pedido? pedido)
        {
            Fornecedores = _pedidosService.ObterFornecedores();
            Clientes = _pedidosService.ObterClientes();            
            
            if (pedido.IdCliente == Guid.Empty && pedido.IdFornecedor == Guid.Empty && string.IsNullOrEmpty(pedido.Status)) 
                Pedidos = _pedidosService.ObterPedidos();
            else
                Pedidos = _pedidosService.ObterPedidos(pedido);
            return Page();
                     
        }       

        public IActionResult OnPostExcluir(Guid id)
        {
            _pedidosService.DeletarPedido(id);

            MensagemAlerta.SetMensagem("MensagemExclusao", "Pedido excluido com sucesso!");

            return RedirectToPage(); 
        }
    }
}
