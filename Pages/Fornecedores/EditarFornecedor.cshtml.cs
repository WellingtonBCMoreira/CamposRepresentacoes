using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Fornecedores
{
    [Authorize]
    public class EditarFornecedorModel : PageModel
    {
        private readonly IFornecedoresService _fornecedoresService;

        public EditarFornecedorModel(IFornecedoresService fornecedoresService)
        {
            _fornecedoresService = fornecedoresService;
        }

        [BindProperty]
        public Fornecedor Fornecedor { get; set; }

        public IActionResult OnGet(string id)
        {
            Fornecedor = _fornecedoresService.ObterFornecedorPorId(id);

            return Page();
        }

        public IActionResult OnPost ()
        {
            _fornecedoresService.AlterarFornecedor(Fornecedor);

            MensagemAlerta.SetMensagem("MsgAlteracao", "Fornecedor alterado com sucesso ;)");

            return Page();
        }
    }
}
