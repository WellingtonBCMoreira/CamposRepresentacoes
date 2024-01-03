using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async void OnGet()
        {
            Transportadoras = await _transportadorasService.ObterTransportadorasAsync();
        }
    }
}
