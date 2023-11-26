using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class faturacaoAvaliacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Faturacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "Avaliacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faturacao_EmpresaId",
                table: "Faturacao",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_ReservaId",
                table: "Avaliacoes",
                column: "ReservaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Reservas_ReservaId",
                table: "Avaliacoes",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturacao_Empresas_EmpresaId",
                table: "Faturacao",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Reservas_ReservaId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturacao_Empresas_EmpresaId",
                table: "Faturacao");

            migrationBuilder.DropIndex(
                name: "IX_Faturacao_EmpresaId",
                table: "Faturacao");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_ReservaId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Faturacao");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "Avaliacoes");
        }
    }
}
