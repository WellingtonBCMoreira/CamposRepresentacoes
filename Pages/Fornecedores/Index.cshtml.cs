using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
        
        [BindProperty(Name = "fornecedor", SupportsGet = true)] 
        public Fornecedor Fornecedor { get; set; }

        public IActionResult OnGet()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Fornecedores = _fornecedoresService.ObterFornecedores().ToList();

                var listaFornecedores = Fornecedores.Select(f => new
                {
                    RazaoSocial = f.RazaoSocial,
                    CNPJ = f.CNPJ,
                    Rua = f.Rua,
                    Bairro = f.Bairro,
                    Cidade = f.Cidade,
                    Numero = f.Numero,
                    Complemento = f.Complemento,
                    CEP = f.CEP,
                    Telefone = f.Telefone,
                    Acao = f.Status
                });

                return new JsonResult(listaFornecedores);
            }

            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost(string idsFornecedoresSelecionados)
        {
            if(!string.IsNullOrEmpty(idsFornecedoresSelecionados))
            {
                List<Guid> ids = JsonConvert.DeserializeObject<List<Guid>>(idsFornecedoresSelecionados);
                foreach(var id in ids)
                {
                    _fornecedoresService.AtivarDesativarFornecedor(id, false);
                }

                MensagemAlerta.SetMensagem("SucessoDesativarFornecedores", "Os fornecedores selecionados foram desativados");
            }
            return RedirectToPage();
        }
    }
}
