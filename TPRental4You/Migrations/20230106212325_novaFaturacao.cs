using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class novaFaturacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturacao_Empresas_EmpresaId",
                table: "Faturacao");

            migrationBuilder.DropIndex(
                name: "IX_Faturacao_EmpresaId",
                table: "Faturacao");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Faturacao",
                newName: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturacao_ReservaId",
                table: "Faturacao",
                column: "ReservaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturacao_Reservas_ReservaId",
                table: "Faturacao",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturacao_Reservas_ReservaId",
                table: "Faturacao");

            migrationBuilder.DropIndex(
                name: "IX_Faturacao_ReservaId",
                table: "Faturacao");

            migrationBuilder.RenameColumn(
                name: "ReservaId",
                table: "Faturacao",
                newName: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturacao_EmpresaId",
                table: "Faturacao",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faturacao_Empresas_EmpresaId",
                table: "Faturacao",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
