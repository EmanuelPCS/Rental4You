using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class relationEstadoReserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "FuncionariorId",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "funcionarioId",
                table: "Estados",
                newName: "FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados",
                newName: "IX_Estados_FuncionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_AspNetUsers_FuncionarioId",
                table: "Estados",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_FuncionarioId",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "FuncionarioId",
                table: "Estados",
                newName: "funcionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_FuncionarioId",
                table: "Estados",
                newName: "IX_Estados_funcionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "funcionarioId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FuncionariorId",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados",
                column: "funcionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
