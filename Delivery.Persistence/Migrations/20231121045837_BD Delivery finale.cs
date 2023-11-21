using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BDDeliveryfinale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaracteristicaComidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicaComidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comidas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    MenuDelDia = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comidas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Calle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    NombreTarjeta = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    fechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CVV = table.Column<string>(type: "char(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateBirth = table.Column<DateTime>(type: "Date", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comida_CaracteristicasMenu",
                columns: table => new
                {
                    IdComida = table.Column<int>(type: "int", nullable: false),
                    IdCaracteristicaComida = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comida_CaracteristicasMenu", x => new { x.IdComida, x.IdCaracteristicaComida });
                    table.ForeignKey(
                        name: "FK_Comida_CaracteristicasMenu_CaracteristicaComidas_IdCaracteristicaComida",
                        column: x => x.IdCaracteristicaComida,
                        principalTable: "CaracteristicaComidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comida_CaracteristicasMenu_Comidas_IdComida",
                        column: x => x.IdComida,
                        principalTable: "Comidas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: true),
                    Fecha_Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true),
                    IdDireccion = table.Column<int>(type: "int", nullable: false),
                    IdMetodoPago = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(9)", nullable: false),
                    IdRepartidor = table.Column<int>(type: "int", nullable: true),
                    Repartidor = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Pedidos_Direcciones_IdDireccion",
                        column: x => x.IdDireccion,
                        principalTable: "Direcciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_MetodoPagos_IdMetodoPago",
                        column: x => x.IdMetodoPago,
                        principalTable: "MetodoPagos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comida_CaracteristicasPedido",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "text", nullable: true),
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comida_CaracteristicasPedido", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comida_CaracteristicasPedido_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comida_CaracteristicasMenu_IdCaracteristicaComida",
                table: "Comida_CaracteristicasMenu",
                column: "IdCaracteristicaComida");

            migrationBuilder.CreateIndex(
                name: "IX_Comida_CaracteristicasPedido_IdPedido",
                table: "Comida_CaracteristicasPedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdDireccion",
                table: "Pedidos",
                column: "IdDireccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdMetodoPago",
                table: "Pedidos",
                column: "IdMetodoPago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comida_CaracteristicasMenu");

            migrationBuilder.DropTable(
                name: "Comida_CaracteristicasPedido");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CaracteristicaComidas");

            migrationBuilder.DropTable(
                name: "Comidas");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "MetodoPagos");
        }
    }
}
