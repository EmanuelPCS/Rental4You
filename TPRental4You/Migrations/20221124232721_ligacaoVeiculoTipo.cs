using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class ligacaoVeiculoTipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiposDeVeiculoId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_TiposDeVeiculoId",
                table: "Veiculos",
                column: "TiposDeVeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_TiposDeVeiculos_TiposDeVeiculoId",
                table: "Veiculos",
                column: "TiposDeVeiculoId",
                principalTable: "TiposDeVeiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_TiposDeVeiculos_TiposDeVeiculoId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_TiposDeVeiculoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "TiposDeVeiculoId",
                table: "Veiculos");
        }
    }
}
