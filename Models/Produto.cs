using System.ComponentModel.DataAnnotations.Schema;

namespace CamposRepresentacoes.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Guid IdFornecedor { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        [NotMapped]
        public string Preco2 { get; set; }
        public bool? Status { get; set; }
        [NotMapped]
        public string RazaoSocialFornecedor { get; set; }

        public DateTime? DataCadastro { get; set; }

        // Relacionamentos
        [NotMapped]
        public Fornecedor Fornecedor { get; set; }
    }
}
