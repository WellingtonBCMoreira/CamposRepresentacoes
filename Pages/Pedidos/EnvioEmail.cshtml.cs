using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CamposRepresentacoes.Pages.Pedidos
{
    public class EnvioEmailModel : PageModel
    {
        private readonly IEmailService _emailService;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPedidosService _pedidosService;
        private readonly IProdutosService _produtosService;
        private readonly IClientesService _clientesService;
        private readonly ITransportadorasService _transportadorasService;

        public EnvioEmailModel(IEmailService emailService, IRazorViewEngine razorViewEngine, 
            ITempDataProvider tempDataProvider, IServiceProvider serviceProvider,
            IPedidosService pedidosService, IProdutosService produtosService, IClientesService clientesService, ITransportadorasService transportadorasService)
        {
            _emailService = emailService;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _pedidosService = pedidosService;
            _produtosService = produtosService;
            _clientesService = clientesService;
            _transportadorasService = transportadorasService;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }
        public IQueryable<ItensPedido> ItensPedido { get; set; }
        public List<ProdutosPedido> ProdutosPedido { get; set; }
        public Cliente Cliente { get; set; }
        public Transportadora Transportadora { get; set; }

        public async Task<IActionResult> OnGet(Guid idPedido, string email)
        {
            ProdutosPedido = new List<ProdutosPedido>();
            if (TempData.ContainsKey("Pedido"))
            {
                string pedidoJson = TempData["Pedido"] as string;

                Pedido = JsonConvert.DeserializeObject<Pedido>(pedidoJson);
            }
            else
            {
                Pedido = _pedidosService.ObterPedidoPorId(idPedido);
            }

            ItensPedido = _pedidosService.ObterItensPedido(idPedido);

            Cliente = _clientesService.ObterClientePeloId(Convert.ToString(Pedido.IdCliente));
            Transportadora = _transportadorasService.ObterTransportadoraPeloId(Convert.ToString(Pedido.IdTransportadora));

            if (ItensPedido.Count() > 0)
            {
                foreach (var item in ItensPedido)
                {
                    Produto produto = _produtosService.ObterProdutoPorId(id: Convert.ToString(item.IdProduto));

                    var prodPedido = new ProdutosPedido
                    {
                        Codigo = produto.Codigo,
                        Nome = produto.Nome,
                        ValorUnitario = produto.Preco,
                        ValorTotal = Convert.ToDecimal(produto.Preco * item.Quantidade),
                        TotalProduto = item.Quantidade,
                    };
                    ProdutosPedido.Add(prodPedido);
                }
            }
            var viewModel = new PedidoViewModel
            {
                Pedido = Pedido,
                ProdutosPedido = ProdutosPedido,
                Cliente = Cliente,
                Transportadora = Transportadora
            };

            var emailContent = await RenderViewToStringAsync("Pedidos/EnvioEmail", viewModel);
            await _emailService.SendEmailAsync("cliente@example.com", "Seu Pedido", emailContent);

            return Page();
        }
        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new ArgumentNullException($"View {viewName} not found.");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var tempData = new TempDataDictionary(httpContext, _tempDataProvider);
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, tempData, sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

    }
}
