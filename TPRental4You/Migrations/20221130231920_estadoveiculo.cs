using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class estadoveiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_empresaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_ApplicationUserId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_AspNetUsers_ApplicationUserId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ApplicationUserId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Reservas",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_ApplicationUserId",
                table: "Reservas",
                newName: "IX_Reservas_ClienteId");

            migrationBuilder.RenameColumn(
                name: "empresaId",
                table: "AspNetUsers",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_empresaId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_EmpresaId");

            migrationBuilder.AddColumn<string>(
                name: "FuncionariorId",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "funcionarioId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados",
                column: "funcionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados",
                column: "funcionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_AspNetUsers_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_AspNetUsers_ClienteId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "FuncionariorId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "funcionarioId",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Reservas",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                newName: "IX_Reservas_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "AspNetUsers",
                newName: "empresaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_empresaId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ApplicationUserId",
                table: "Estados",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_empresaId",
                table: "AspNetUsers",
                column: "empresaId",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_AspNetUsers_ApplicationUserId",
                table: "Estados",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_AspNetUsers_ApplicationUserId",
                table: "Reservas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
