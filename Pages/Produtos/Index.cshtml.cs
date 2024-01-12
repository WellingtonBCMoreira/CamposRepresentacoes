using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Pages.Fornecedores;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Text.Json.Serialization;

namespace CamposRepresentacoes.Pages.Produtos
{
    public class IndexModel : PageModel
    {
        private readonly IProdutosService _produtosService;

        public IndexModel(IProdutosService produtosService)
        {
            _produtosService = produtosService;
        }
        public List<Produto> Produtos { get; set; } = new(); 
        public IQueryable<Fornecedor> Fornecedores { get; set; }

        [BindProperty(Name = "produto", SupportsGet = true)]
        public Produto Produto { get; set; }

        [BindProperty]
        public IFormFile ArquivoUpload { get; set; }
        [BindProperty]
        public Guid FornecedorId { get; set; }

        public IActionResult OnGet()
        { 
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Produtos = _produtosService.ObterProdutos(Produto).ToList();
                
                var listaProdutos = Produtos.Select(p => new
                {
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    DataCadastro = p.DataCadastro,
                    Status = p.Status,
                    Id = p.Id,
                });

                return new JsonResult(listaProdutos);
                //ViewData["Produtos"] = listaProdutos;
            }

            Fornecedores = _produtosService.ObterFornecedores();
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ArquivoUpload != null && ArquivoUpload.Length > 0)
            {
                if (Path.GetExtension(ArquivoUpload.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    var dadosImportados = new List<object>();
                    var dadosImportadosComErro = new List<object>();

                    using (var stream = new MemoryStream())
                    {
                        ArquivoUpload.CopyTo(stream);

                        if (FornecedorId != Guid.Empty)
                        {
                            // Utilize o ExcelDataReader para ler o arquivo Excel
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });

                                var dataTable = dataSet.Tables[0];

                                foreach (DataRow row in dataTable.Rows)
                                {
                                    var produto = new Produto
                                    {
                                        Id = Guid.NewGuid(),
                                        IdFornecedor = FornecedorId,
                                        Nome = row["Nome"].ToString() != "" ? row["Nome"].ToString().Trim() : string.Empty,
                                        Descricao = row["Descrição"].ToString() != "" ? row["Descrição"].ToString().Trim() : string.Empty,
                                        Preco = row["Preço"].ToString() != "" ? decimal.Parse(s: row["Preço"].ToString().Trim()): 0,
                                        Status = true,
                                        DataCadastro = DateTime.Now,
                                    };

                                    var isValid = ValidarProduto(produto);

                                    var produtoItem = new
                                    {
                                        Status = isValid ? "Importado" : "Não importado",
                                        Nome = produto.Nome,
                                        Descricao = produto.Descricao,
                                        Preco = produto.Preco
                                    };

                                    if (isValid)
                                    {
                                        _produtosService.CadastrarProduto(produto);
                                        dadosImportados.Add(produtoItem);
                                    }
                                    else
                                    {
                                        dadosImportadosComErro.Add(produtoItem);
                                    }
                                }
                            }
                        }
                    }
                    ViewData["ProdutosComErro"] = dadosImportadosComErro;
                    ViewData["ProdutosImportados"] = dadosImportados;
                }
            }
            return Page();
        }
        
        [ValidateAntiForgeryToken]
        public IActionResult OnPostDesativarProduto(string idsProdutosSelecionados)
        {
            if (!string.IsNullOrEmpty(idsProdutosSelecionados))
            {                
                List<Guid> idProdutos = JsonConvert.DeserializeObject<List<Guid>>(idsProdutosSelecionados);
                foreach (var id in idProdutos)
                {
                    _produtosService.DeletarProdudo(id);

                }

                MensagemAlerta.SetMensagem("SucessoDesativarProdutos", "Os produtos selecionados foram desativados!!!");
            }
            return RedirectToPage();
        }

        private bool ValidarProduto(Produto produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
                return false;
            if (string.IsNullOrEmpty(produto.Descricao))
                return false;
            if (produto.Preco <= 0)
                return false;

            return true;
        }
    }
}
