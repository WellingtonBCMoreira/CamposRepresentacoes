using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Transportadoras
{
    public class IndexModel : PageModel
    {
        private readonly ITransportadorasService _transportadorasService;

        public IndexModel(ITransportadorasService transportadorasService)
        {
            _transportadorasService = transportadorasService;
        }

        public List<Transportadora> Transportadoras { get; set; }

        [BindProperty(Name = "transportadora", SupportsGet = true)]
        public Transportadora Transportadora { get; set; }

        public IActionResult OnGet()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Transportadoras = _transportadorasService.ObterTransportadoras(Transportadora).ToList();

                var listaTransportadoras = Transportadoras.Select(c => new
                {
                    RazaoSocial = c.RazaoSocial,
                    CNPJ = c.CNPJ,
                    Cidade = c.Cidade,
                    Numero = c.Numero,
                    Telefone = c.Telefone,
                    Email = c.Email,
                    Status = c.Status,
                    Id = c.Id,
                });

                return new JsonResult(listaTransportadoras);
            }

            return Page(); ;
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost(string idsTransportadorasSelecionadas)
        {
            if (!string.IsNullOrEmpty(idsTransportadorasSelecionadas))
            {
                List<Guid> ids = JsonConvert.DeserializeObject<List<Guid>>(idsTransportadorasSelecionadas);
                foreach (var id in ids)
                {
                    _transportadorasService.AtivarDesativarTransportadora(id, false);
                }

                MensagemAlerta.SetMensagem("SucessoDesativarTransportadoras", "As Transportadoras selecionadas foram desativadas"); 
            }
            return RedirectToPage();
        }
    }
}
