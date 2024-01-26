using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Clientes
{
    public class CadastrarClienteModel : PageModel
    {
        private readonly IClientesService _clientesService;

        public CadastrarClienteModel(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }
        [BindProperty]
        public Cliente Cliente { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        { 
            _clientesService.CadastrarCliente(Cliente);

            MensagemAlerta.SetMensagem("CadastroRealizado", "Cliente cadastrado com sucesso :)");

            return Page();
        }
    }
}
