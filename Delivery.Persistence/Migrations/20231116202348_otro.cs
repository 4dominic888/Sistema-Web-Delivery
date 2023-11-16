using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class otro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_MetodoPagos_MetodoPagoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_MetodoPagoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "MetodoPagoId",
                table: "Pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdMetodoPago",
                table: "Pedidos",
                column: "IdMetodoPago");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_MetodoPagos_IdMetodoPago",
                table: "Pedidos",
                column: "IdMetodoPago",
                principalTable: "MetodoPagos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_MetodoPagos_IdMetodoPago",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdMetodoPago",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "MetodoPagoId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_MetodoPagoId",
                table: "Pedidos",
                column: "MetodoPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_MetodoPagos_MetodoPagoId",
                table: "Pedidos",
                column: "MetodoPagoId",
                principalTable: "MetodoPagos",
                principalColumn: "Id");
        }
    }
}
