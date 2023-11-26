using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class addEstadoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Veiculos");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "AspNetUsers",
                newName: "empresaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_empresaId");

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Km = table.Column<float>(type: "real", nullable: false),
                    DanosDoVeiculo = table.Column<bool>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_empresaId",
                table: "AspNetUsers",
                column: "empresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_empresaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.RenameColumn(
                name: "empresaId",
                table: "AspNetUsers",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_empresaId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_EmpresaId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Veiculos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
