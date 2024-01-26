using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Fornecedores
{
    public class CadastrarFornecedorModel : PageModel
    {
        private readonly IFornecedoresService _fornecedoresService;

        public CadastrarFornecedorModel(IFornecedoresService fornecedoresService)
        {
            _fornecedoresService = fornecedoresService;
        }

        [BindProperty]
        public Fornecedor Fornecedor { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            _fornecedoresService.CadastrarFornecedor(Fornecedor);

            MensagemAlerta.SetMensagem("CadastroRealizado", "Fornecedor cadastrado com sucesso :)");

            return Page();
        }
    }
}
