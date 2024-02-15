using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposRepresentacoes.Migrations
{
    public partial class AdicionandoColunasTabelaPedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeItens",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocialCliente",
                table: "Pedidos",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocialFornecedor",
                table: "Pedidos",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeItens",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "RazaoSocialCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "RazaoSocialFornecedor",
                table: "Pedidos");
        }
    }
}
