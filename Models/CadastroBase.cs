namespace CamposRepresentacoes.Models
{
    public class CadastroBase
    {        
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public bool? Status { get; set; }
    }
}
