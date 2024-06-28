using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamposRepresentacoes.Data.Configuration
{
    public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedores");

            builder.Property(f => f.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(f => f.RazaoSocial)
                .HasColumnName("RazaoSocial")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(f => f.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(15);

            builder.Property(f => f.CNPJ)
                .HasColumnName("CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(f => f.Rua)
                .HasColumnName("Rua")
                .HasMaxLength(50);

            builder.Property(f => f.Bairro)
                .HasColumnName("Bairro")
                .HasMaxLength(50);

            builder.Property(f => f.Cidade)
                .HasColumnName("Cidade")
                .HasMaxLength(50);

            builder.Property(f => f.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            builder.Property(f => f.Complemento)
                .HasColumnName("Complemento")
                .HasMaxLength(255);

            builder.Property(f => f.CEP)
                .HasColumnName("CEP")
                .HasMaxLength(8);

            builder.Property(f => f.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(14);

            builder.Property(f => f.Celular)
                .HasColumnName("Celular")
                .HasMaxLength(14);

            builder.Property(f => f.Status)
                .HasColumnName("Ativo")
                .IsRequired();

            builder.Property(f => f.Email)
                .HasColumnName("Email")
                .IsRequired();

            builder.HasKey(f => f.Id);
        }
    }
}
