namespace CamposRepresentacoes.Models
{
    public class PedidoViewModel
    {
        public Pedido Pedido { get; set; }
        public List<ProdutosPedido> ProdutosPedido { get; set; }
        public Cliente Cliente { get; set; }
        public Transportadora Transportadora { get; set; }
    }
}
