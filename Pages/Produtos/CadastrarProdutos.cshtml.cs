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
            if (!ModelState.IsValid)
            {
                // Se o modelo n�o for v�lido, retorne para a p�gina com erros de valida��o
                return Page();
            }

            try
            {
                Produto.Id = Guid.NewGuid();
                Produto.Status = true;
                Produto.DataCadastro = DateTime.Now;

                _produtosService.CadastrarProduto(Produto);                

                // Redirecionar para a p�gina de lista de produtos ap�s o cadastro bem-sucedido
                return RedirectToPage("/Produtos/CadastrarProdutos");
            }
            catch (Exception ex)
            {
                // Tratar qualquer exce��o aqui
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao cadastrar o produto.");
                return Page();
            }
            
        }
    }
}
