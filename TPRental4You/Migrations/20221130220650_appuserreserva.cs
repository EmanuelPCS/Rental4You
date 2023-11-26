using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class appuserreserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_AspNetUsers_clienteId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ReservaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "funcionarioId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "clienteId",
                table: "Reservas",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_clienteId",
                table: "Reservas",
                newName: "IX_Reservas_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ApplicationUserId",
                table: "Estados",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ReservaId",
                table: "Estados",
                column: "ReservaId");

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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_ApplicationUserId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_AspNetUsers_ApplicationUserId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ApplicationUserId",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ReservaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Reservas",
                newName: "clienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_ApplicationUserId",
                table: "Reservas",
                newName: "IX_Reservas_clienteId");

            migrationBuilder.AddColumn<string>(
                name: "funcionarioId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados",
                column: "funcionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_ReservaId",
                table: "Estados",
                column: "ReservaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados",
                column: "funcionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_AspNetUsers_clienteId",
                table: "Reservas",
                column: "clienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
