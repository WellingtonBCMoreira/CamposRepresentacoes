namespace CamposRepresentacoes.Models
{
    public class ItensPedido
    {
        public Guid Id { get; set; }
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public Guid IdFornecedor { get; set; }
        public int Quantidade { get; set; }
        public decimal? Preco { get; set; }
        public string Status { get; set; }

        // Relacionamentos
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}