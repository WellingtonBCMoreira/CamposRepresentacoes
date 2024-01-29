using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Transportadoras
{
    public class EditarTransportadoraModel : PageModel
    {
        private readonly ITransportadorasService _transportadorasService;

        public EditarTransportadoraModel(ITransportadorasService transportadorasService)
        {
            _transportadorasService = transportadorasService;
        }

        [BindProperty]
        public Transportadora Transportadora { get; set; }

        public IActionResult OnGet(string id)
        {
            Transportadora = _transportadorasService.ObterTransportadoraPeloId(id);

            return Page();
        }

        public IActionResult OnPost() 
        {
            _transportadorasService.AlterarTransportadora(Transportadora);

            MensagemAlerta.SetMensagem("MsgAlteracao", "Transportadora alterada com sucesso ;)");

            return Page();
        }
    }
}
