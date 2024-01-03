using CamposRepresentacoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CamposRepresentacoes.Data.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DataEmissao)
                .HasColumnName("DataEmissao")
                .IsRequired();
           
            builder.Property(p => p.ValorTotal)
                .HasColumnName("ValorTotal")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .HasMaxLength(1)
                .IsRequired();           

            // Relacionamento com o Cliente
            builder.HasOne(p => p.Cliente)                
                .WithMany()
                .HasForeignKey(p => p.IdCliente)
                .IsRequired();

            // Relacionamento com o Fornecedor
            builder.HasOne(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.IdFornecedor)
                .IsRequired();

            // Relacionamento com a Transportadora
            builder.HasOne(p => p.Transportadora)
                .WithMany()
                .HasForeignKey(p => p.IdTransportadora)
                .IsRequired();

            // Relacionamento com o Item do Pedido
            builder.HasMany(p => p.ItensPedido)
               .WithOne(ip => ip.Pedido)
               .HasForeignKey(ip => ip.IdPedido)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(p => p.Id);
        }
    }
}
