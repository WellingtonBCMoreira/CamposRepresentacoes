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
                // Se o modelo não for válido, retorne para a página com erros de validação
                return Page();
            }

            try
            {
                Produto.Id = Guid.NewGuid();
                Produto.Status = true;
                Produto.DataCadastro = DateTime.Now;

                _produtosService.CadastrarProduto(Produto);                

                // Redirecionar para a página de lista de produtos após o cadastro bem-sucedido
                return RedirectToPage("/Produtos/CadastrarProdutos");
            }
            catch (Exception ex)
            {
                // Tratar qualquer exceção aqui
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao cadastrar o produto.");
                return Page();
            }
            
        }
    }
}
