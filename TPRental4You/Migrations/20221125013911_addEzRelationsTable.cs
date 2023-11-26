using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class addEzRelationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "funcionarioId",
                table: "Estados",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "FK_Estados_Reservas_ReservaId",
                table: "Estados",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_AspNetUsers_funcionarioId",
                table: "Estados");

            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Reservas_ReservaId",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_funcionarioId",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_ReservaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "funcionarioId",
                table: "Estados");
        }
    }
}
