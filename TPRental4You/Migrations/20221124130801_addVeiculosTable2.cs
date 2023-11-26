using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class addVeiculosTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Veiculos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Veiculos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "IdadeMinima",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requisitos",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Reservado",
                table: "Veiculos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "IdadeMinima",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Requisitos",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Reservado",
                table: "Veiculos");
        }
    }
}
