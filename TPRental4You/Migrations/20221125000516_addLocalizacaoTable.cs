using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class addLocalizacaoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalizacaoId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoAdicional = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_LocalizacaoId",
                table: "Veiculos",
                column: "LocalizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Localizacoes_LocalizacaoId",
                table: "Veiculos",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Localizacoes_LocalizacaoId",
                table: "Veiculos");

            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_LocalizacaoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "Veiculos");
        }
    }
}
