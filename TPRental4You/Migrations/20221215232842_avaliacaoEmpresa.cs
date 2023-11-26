using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class avaliacaoEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Avaliacao",
                table: "Empresas",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "Empresas");
        }
    }
}
