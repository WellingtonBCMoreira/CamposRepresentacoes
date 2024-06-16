﻿// <auto-generated />
using System;
using CamposRepresentacoes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CamposRepresentacoes.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240616162833_AdicionarRazaoSocialTransportadora")]
    partial class AdicionarRazaoSocialTransportadora
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("CamposRepresentacoes.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Bairro");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT")
                        .HasColumnName("CEP");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("CNPJ");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Cidade");

                    b.Property<string>("Complemento")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Complemento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Numero");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocial");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Rua");

                    b.Property<bool?>("Status")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Ativo");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Bairro");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT")
                        .HasColumnName("CEP");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("CNPJ");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Cidade");

                    b.Property<string>("Complemento")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Complemento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Numero");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocial");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Rua");

                    b.Property<bool?>("Status")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Ativo");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.ItensPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<Guid>("IdFornecedor")
                        .HasColumnType("TEXT")
                        .HasColumnName("IdFornecedor");

                    b.Property<Guid>("IdPedido")
                        .HasColumnType("TEXT")
                        .HasColumnName("IdPedido");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("TEXT")
                        .HasColumnName("IdProduto");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("TEXT")
                        .HasColumnName("Preco");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Quantidade");

                    b.HasKey("Id");

                    b.HasIndex("IdFornecedor");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProduto");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItensPedido", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("TEXT")
                        .HasColumnName("DataEmissao");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("FormaPagamento");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdFornecedor")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdTransportadora")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantidadeItens")
                        .HasColumnType("int")
                        .HasColumnName("QuantidadeItens");

                    b.Property<string>("RazaoSocialCliente")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocialCliente");

                    b.Property<string>("RazaoSocialFornecedor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocialFornecedor");

                    b.Property<string>("RazaoSocialTransportadora")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocialTransportadora");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("TEXT")
                        .HasColumnName("Status");

                    b.Property<decimal?>("ValorTotal")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdFornecedor");

                    b.HasIndex("IdTransportadora");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Codigo");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("TEXT")
                        .HasColumnName("DataCadastro");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT")
                        .HasColumnName("Descricao");

                    b.Property<Guid>("IdFornecedor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Nome");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Preco");

                    b.Property<bool?>("Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("INTEGER")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("IdFornecedor");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Transportadora", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Bairro");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT")
                        .HasColumnName("CEP");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("CNPJ");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Cidade");

                    b.Property<string>("Complemento")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Complemento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Numero");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("RazaoSocial");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("Rua");

                    b.Property<bool?>("Status")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Ativo");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("TEXT")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Transportadoras", (string)null);
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.ItensPedido", b =>
                {
                    b.HasOne("CamposRepresentacoes.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CamposRepresentacoes.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CamposRepresentacoes.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CamposRepresentacoes.Models.Pedido", null)
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId");

                    b.Navigation("Fornecedor");

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Pedido", b =>
                {
                    b.HasOne("CamposRepresentacoes.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CamposRepresentacoes.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CamposRepresentacoes.Models.Transportadora", "Transportadora")
                        .WithMany()
                        .HasForeignKey("IdTransportadora")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Fornecedor");

                    b.Navigation("Transportadora");
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Produto", b =>
                {
                    b.HasOne("CamposRepresentacoes.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("CamposRepresentacoes.Models.Pedido", b =>
                {
                    b.Navigation("ItensPedido");
                });
#pragma warning restore 612, 618
        }
    }
}
