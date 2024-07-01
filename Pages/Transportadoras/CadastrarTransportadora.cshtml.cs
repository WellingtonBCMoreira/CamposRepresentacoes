using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Transportadoras
{
    [Authorize]
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

            MensagemAlerta.SetMensagem("CadastroRealizado", "Transportadora cadastrada com socesso :)");

            return Page();


        }
    }
}
