using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CamposRepresentacoes.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        private readonly IClientesService _clientesService;

        public IndexModel(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        public List<Cliente> Clientes { get; set; } = new();

        [BindProperty(Name = "cliente", SupportsGet = true)]
        public Cliente Cliente { get; set; } = new();

        public IActionResult Onget()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Clientes = _clientesService.ObterClientes(Cliente).ToList();

                var listaClientes = Clientes.Select(c => new
                {
                    RazaoSocial = c.RazaoSocial,
                    CNPJ = c.CNPJ,
                    Cidade = c.Cidade,
                    Telefone = c.Telefone,
                    Email = c.Email,                    
                    Status = c.Status                    
                });

                return new JsonResult(listaClientes);               
            }

            return Page();
        }

        public IActionResult OnGetPesquisar(Cliente filtro)
        {
            
            Clientes = _clientesService.ObterClientes(filtro).ToList();

            ViewData["MostrarTabela"] = Clientes.Any();

            ViewData["Pesquisado"] = true;

            return Page();
        }
    }
}
