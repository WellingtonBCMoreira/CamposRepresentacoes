using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamposRepresentacoes.Data.Configuration
{
    public class TransportadoraConfiguration : IEntityTypeConfiguration<Transportadora>
    {
        public void Configure(EntityTypeBuilder<Transportadora> builder)
        {
            builder.ToTable("Transportadoras");

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(t => t.RazaoSocial)
                .HasColumnName("RazaoSocial")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(15);

            builder.Property(t => t.CNPJ)
                .HasColumnName("CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(t => t.Rua)
                .HasColumnName("Rua")
                .HasMaxLength(50);

            builder.Property(t => t.Bairro)
                .HasColumnName("Bairro")
                .HasMaxLength(50);

            builder.Property(t => t.Cidade)
                .HasColumnName("Cidade")
                .HasMaxLength(50);

            builder.Property(t => t.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            builder.Property(t => t.Complemento)
                .HasColumnName("Complemento")
                .HasMaxLength(255);

            builder.Property(t => t.CEP)
                .HasColumnName("CEP")
                .HasMaxLength(8);

            builder.Property(t => t.Telefone)
                .HasColumnName("Telefone")
                .HasMaxLength(14);

            builder.Property(t => t.Celular)
                .HasColumnName("Celular")
                .HasMaxLength(14);

            builder.Property(t => t.Status)
                .HasColumnName("Ativo")
                .IsRequired();

            builder.Property(t => t.Email)
                .HasColumnName("Email")
                .IsRequired();

            builder.HasKey(t => t.Id);
        }
    }
}
