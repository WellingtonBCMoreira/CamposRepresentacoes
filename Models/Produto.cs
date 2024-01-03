using System.ComponentModel.DataAnnotations.Schema;

namespace CamposRepresentacoes.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Guid IdFornecedor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        // Relacionamentos
        public Fornecedor Fornecedor { get; set; }
    }
}
