using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ComidaNM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaracteristicaComidas_Comidas_IdComida",
                table: "CaracteristicaComidas");

            migrationBuilder.DropIndex(
                name: "IX_CaracteristicaComidas_IdComida",
                table: "CaracteristicaComidas");

            migrationBuilder.DropColumn(
                name: "IdComida",
                table: "CaracteristicaComidas");

            migrationBuilder.CreateTable(
                name: "Comida_Caracteristicas",
                columns: table => new
                {
                    IdComida = table.Column<int>(type: "int", nullable: false),
                    IdCaracteristicaComida = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comida_Caracteristicas", x => new { x.IdComida, x.IdCaracteristicaComida });
                    table.ForeignKey(
                        name: "FK_Comida_Caracteristicas_CaracteristicaComidas_IdCaracteristicaComida",
                        column: x => x.IdCaracteristicaComida,
                        principalTable: "CaracteristicaComidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comida_Caracteristicas_Comidas_IdComida",
                        column: x => x.IdComida,
                        principalTable: "Comidas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comida_Caracteristicas_IdCaracteristicaComida",
                table: "Comida_Caracteristicas",
                column: "IdCaracteristicaComida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comida_Caracteristicas");

            migrationBuilder.AddColumn<int>(
                name: "IdComida",
                table: "CaracteristicaComidas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicaComidas_IdComida",
                table: "CaracteristicaComidas",
                column: "IdComida");

            migrationBuilder.AddForeignKey(
                name: "FK_CaracteristicaComidas_Comidas_IdComida",
                table: "CaracteristicaComidas",
                column: "IdComida",
                principalTable: "Comidas",
                principalColumn: "ID");
        }
    }
}
