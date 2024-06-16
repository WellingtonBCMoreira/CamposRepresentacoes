using System.ComponentModel.DataAnnotations.Schema;

namespace CamposRepresentacoes.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdFornecedor { get; set; }
        public Guid IdTransportadora { get; set; }
        public DateTime DataEmissao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ValorTotal { get; set; }
        public string Status { get; set; }
        public string FormaPagamento { get; set; }                
        public string RazaoSocialCliente { get; set; }
        public string RazaoSocialFornecedor { get; set; }
        public string RazaoSocialTransportadora { get; set; }
        public int QuantidadeItens { get; set; }

        // Relacionamentos
        public Cliente Cliente { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Transportadora Transportadora { get; set; }
        public List<ItensPedido> ItensPedido { get; set; }
    }
    
}
