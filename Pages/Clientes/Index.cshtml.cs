using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
                    Numero = c.Numero,
                    Telefone = c.Telefone,
                    Email = c.Email,                    
                    Status = c.Status,
                    Id = c.Id,
                });

                return new JsonResult(listaClientes);               
            }

            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost(string idsClientesSelecionados)
        {
            if (!string.IsNullOrEmpty(idsClientesSelecionados))
            {
                List<Guid> idProdutos = JsonConvert.DeserializeObject<List<Guid>>(idsClientesSelecionados);
                foreach (var id in idProdutos)
                {
                    _clientesService.AtivarDesativarCliente(id, false);

                }

                MensagemAlerta.SetMensagem("SucessoDesativarClientes", "Os Clientes selecionados foram desativados :(");
            }
            return RedirectToPage();
        }
    }
}
