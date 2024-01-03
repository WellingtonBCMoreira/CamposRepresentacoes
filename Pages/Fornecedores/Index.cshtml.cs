using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Fornecedores
{
    public class IndexModel : PageModel
    {
        private readonly IFornecedoresService _fornecedoresService;

        public IndexModel(IFornecedoresService fornecedoresService)
        {
            _fornecedoresService = fornecedoresService;
        }

        public List<Fornecedor> Fornecedores { get; set; }

        public void OnGet()
        {
            Fornecedores = _fornecedoresService.ObterFornecedores().ToList();
        }
    }
}
