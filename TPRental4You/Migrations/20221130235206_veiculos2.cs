using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class veiculos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Veiculos");

            migrationBuilder.RenameColumn(
                name: "Requisitos",
                table: "Veiculos",
                newName: "Mudancas");

            migrationBuilder.RenameColumn(
                name: "IdadeMinima",
                table: "Veiculos",
                newName: "Malas");

            migrationBuilder.AddColumn<int>(
                name: "Lugares",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lugares",
                table: "Veiculos");

            migrationBuilder.RenameColumn(
                name: "Mudancas",
                table: "Veiculos",
                newName: "Requisitos");

            migrationBuilder.RenameColumn(
                name: "Malas",
                table: "Veiculos",
                newName: "IdadeMinima");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
