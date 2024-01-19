using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Produtos
{
    public class CadastrarProdutosModel : PageModel
    {
        private readonly IProdutosService _produtosService;

        public CadastrarProdutosModel(IProdutosService produtosService)
        {
            _produtosService = produtosService;
        }

        [BindProperty]
        public Produto Produto { get; set; } = new();
        public IQueryable<Fornecedor> Fornecedores { get; set; } 

        public void OnGet()
        {
            // Lista com os fornecedores
            Fornecedores = _produtosService.ObterFornecedores();
        }

        public IActionResult OnPost() 
        {
            _produtosService.CadastrarProduto(Produto);

            MensagemAlerta.SetMensagem("CadastroRealizado", "Produto cadastrado com sucesso!");
                
            return RedirectToPage();          
            
        }
    }
}
