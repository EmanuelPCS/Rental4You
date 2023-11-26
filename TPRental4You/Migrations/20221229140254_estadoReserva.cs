using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class estadoReserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmada",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "EstadoReserva",
                table: "Reservas",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoReserva",
                table: "Reservas");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmada",
                table: "Reservas",
                type: "bit",
                nullable: true);
        }
    }
}
