namespace CamposRepresentacoes.Models
{
    public class ProdutosPedido
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int TotalProduto { get; set; }
    }
}
