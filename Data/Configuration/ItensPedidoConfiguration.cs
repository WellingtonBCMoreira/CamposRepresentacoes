using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamposRepresentacoes.Data.Configuration
{
    internal class ItensPedidoConfiguration : IEntityTypeConfiguration<ItensPedido>
    {
        public void Configure(EntityTypeBuilder<ItensPedido> builder)
        {
            builder.ToTable("ItensPedido");

            builder.Property(ip => ip.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(ip => ip.IdPedido)
                .HasColumnName("IdPedido")
                .IsRequired();

            builder.Property(ip => ip.IdProduto)
                .HasColumnName("IdProduto")
                .IsRequired();

            builder.Property(ip => ip.IdFornecedor)
                .HasColumnName("IdFornecedor")
                .IsRequired();

            builder.Property(ip => ip.Quantidade)
                .HasColumnName("Quantidade")
                .IsRequired();

            builder.Property(ip => ip.Preco)
                .HasColumnName("Preco");

            builder.Property(ip => ip.Status)
                .HasColumnName("Status")
                .HasMaxLength(1)
                .IsRequired();

            //Relacionamentos
            builder.HasOne(ip => ip.Pedido)
                .WithMany()
                .HasForeignKey(ip => ip.IdPedido);

            builder.HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ip => ip.Fornecedor)
                .WithMany()
                .HasForeignKey(ip => ip.IdFornecedor);

            builder.HasKey(ip => ip.Id);
        }
    }
}