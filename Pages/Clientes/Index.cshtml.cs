using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        //public async void OnGet()
        //{
        //    //Clientes = _clienteService.ObterClientes().ToList();
        //    Clientes = await _clientesService.ObterClientesAsync();
        //}

        public IActionResult OnGetPesquisar(Cliente filtro)
        {
            
            Clientes = _clientesService.ObterClientes(filtro).ToList();

            ViewData["MostrarTabela"] = Clientes.Any();

            ViewData["Pesquisado"] = true;

            return Page();
        }
    }
}
