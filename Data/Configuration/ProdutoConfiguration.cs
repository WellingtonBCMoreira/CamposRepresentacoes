using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CamposRepresentacoes.Models;

namespace CamposRepresentacoes.Data.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("Descricao")
                .HasMaxLength(1000);

            builder.Property(p => p.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasColumnName("DataCadastro");

            // Relacionamento com o Fornecedor
            builder.HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.IdFornecedor)
                .IsRequired();

            builder.HasKey(p => p.Id);
        }
    }
}
