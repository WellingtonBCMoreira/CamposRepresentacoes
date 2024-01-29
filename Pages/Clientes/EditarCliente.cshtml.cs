using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Clientes
{
    public class EditarClienteModel : PageModel
    {
        private readonly IClientesService _clientesService;

        public EditarClienteModel(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Cliente = _clientesService.ObterClientePeloId(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            _clientesService.AlterarCliente(Cliente);

            MensagemAlerta.SetMensagem("MsgAlteracao", "Cliente alterado com sucesso ;)");
            
            return Page();
        }
    }
}
