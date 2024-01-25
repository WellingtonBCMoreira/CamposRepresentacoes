using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamposRepresentacoes.Data.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(c => c.RazaoSocial)
                .HasColumnName("RazaoSocial")
                .HasMaxLength(255)
                .IsRequired();                     

            builder.Property(c => c.CNPJ)
                .HasColumnName("CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.Rua)
                .HasColumnName("Rua")
                .HasMaxLength(50);

            builder.Property(c => c.Bairro)
                .HasColumnName("Bairro")
                .HasMaxLength(50);

            builder.Property(c => c.Cidade)
                .HasColumnName("Cidade")
                .HasMaxLength(50);

            builder.Property(c => c.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            builder.Property(c => c.Complemento)
                .HasColumnName("Complemento")
                .HasMaxLength(255);

            builder.Property(c => c.CEP)
                .HasColumnName("CEP")
                .HasMaxLength(8);

            builder.Property(c => c.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(14);

            builder.Property(c => c.Status)
                .HasColumnName("Ativo")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnName("Email")
                .IsRequired();

            builder.HasKey(c => c.Id);
        }
    }
}
