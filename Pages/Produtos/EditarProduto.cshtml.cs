using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Produtos
{
    [Authorize]
    public class EditarProdutoModel : PageModel
    {
        private readonly IProdutosService _produtosService;

        public EditarProdutoModel(IProdutosService produtosService)
        {
            _produtosService = produtosService;
        }

        [BindProperty]
        public Produto Produto { get; set; } = new();

        public IActionResult OnGet(string id)
        {            
            Produto = _produtosService.ObterProdutoPorId(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            _produtosService.AlterarProduto(Produto);

            MensagemAlerta.SetMensagem("MsgAlteracao", "Produto alterado com sucesso!!!");

            return Page();
        }
    }
}
