using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class avaliacoesEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Reservas_ReservaId",
                table: "Avaliacoes");

            migrationBuilder.RenameColumn(
                name: "ReservaId",
                table: "Avaliacoes",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_ReservaId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Empresas_EmpresaId",
                table: "Avaliacoes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Empresas_EmpresaId",
                table: "Avaliacoes");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Avaliacoes",
                newName: "ReservaId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_EmpresaId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_ReservaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Reservas_ReservaId",
                table: "Avaliacoes",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
