using CamposRepresentacoes.Interfaces.Services;
using CamposRepresentacoes.Models;
using CamposRepresentacoes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Text;

namespace CamposRepresentacoes.Pages.Pedidos
{
    [Authorize]
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
        private readonly PDFService _pdfService;


        public EnvioEmailModel(IEmailService emailService, IRazorViewEngine razorViewEngine, 
            ITempDataProvider tempDataProvider, IServiceProvider serviceProvider,
            IPedidosService pedidosService, IProdutosService produtosService, IClientesService clientesService, 
            ITransportadorasService transportadorasService, PDFService pdfService)
        {
            _emailService = emailService;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _pedidosService = pedidosService;
            _produtosService = produtosService;
            _clientesService = clientesService;
            _transportadorasService = transportadorasService;
            _pdfService = pdfService;
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
                    Produto produto = _produtosService.ObterProdutoPorId(Convert.ToString(item.IdProduto));

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

            var emailContent = await RenderViewToStringAsync("/Pages/Shared/_PedidoEmailPartial.cshtml", viewModel);

            string bodyEmail = BodyEmail();
            string nomeCliente = Cliente.RazaoSocial.Split(' ')[0];

            var pdfBytes = _pdfService.ConvertToPdf(emailContent);

            await _emailService.SendEmailAsync(email, "Seu Pedido", bodyEmail, nomeCliente, pdfBytes);

            TempData["EmailEnviado"] = "Email enviado com sucesso!";

            //MensagemAlerta.SetMensagem("EmailEnviado", "Email enviado com sucesso!");

            return RedirectToPage("/Pedidos/Detalhes", new { idPedido });
        }

        private string BodyEmail()
        {
            var htmlBody = new StringBuilder();
            htmlBody.AppendLine("<html>");
            htmlBody.AppendLine("<body>");
            htmlBody.AppendLine($"<p style='font-weight: bold;'>Prezado(a) {Cliente.RazaoSocial},</p>");
            htmlBody.AppendLine("<p>O seu pedido está em anexo.</p>");
            htmlBody.AppendLine("<p>Atenciosamente,</p>");
            htmlBody.AppendLine("<p style='font-weight: bold;'>Att, Julio Campos</p>");
            htmlBody.AppendLine("</body>");
            htmlBody.AppendLine("</html>");
            
            return htmlBody.ToString();
        }

        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();

            // Ensure the ActionContext has a router associated with it
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor)
            {
                // Add this line to fix the routing issue
                RouteData = new RouteData()
            };

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView("", viewName, false);

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
