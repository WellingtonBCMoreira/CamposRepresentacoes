using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamposRepresentacoes.Pages.Produtos
{
    [Authorize]
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
            var produto = _produtosService.ObterProduto(Produto.Codigo, Produto.IdFornecedor);

            if(produto is null)
            {
                _produtosService.CadastrarProduto(Produto);
                MensagemAlerta.SetMensagem("CadastroRealizado", "Produto cadastrado com sucesso!");            
            }
            else
                MensagemAlerta.SetMensagem("ProdutoExistente", $"Produto de codigo {Produto.Codigo} jah cadastrado anteriormente, utilize a tela para editar!");

            return RedirectToPage();          
            
        }
    }
}
