using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Transportadoras
{
    public class CadastrarTransportadoraModel : PageModel
    {
        private readonly ITransportadorasService _transportadorasService;
        
        public CadastrarTransportadoraModel (ITransportadorasService transportadorasService) 
        {
            _transportadorasService = transportadorasService;
        }

        [BindProperty]
        public Transportadora Transportadora { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _transportadorasService.CadastrarTransportadora(Transportadora);

            MensagemAlerta.SetMensagem("CadastroRealizado", "Fornecer cadastro com socesso :)");

            return Page();


        }
    }
}
