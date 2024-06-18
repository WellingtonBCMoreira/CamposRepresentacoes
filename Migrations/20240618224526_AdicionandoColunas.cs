using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposRepresentacoes.Migrations
{
    public partial class AdicionandoColunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataFinalizacao",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ItensPedido",
                type: "TEXT",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFinalizacao",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ItensPedido");
        }
    }
}
