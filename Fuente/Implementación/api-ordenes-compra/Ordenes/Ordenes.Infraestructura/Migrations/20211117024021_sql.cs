using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordenes.Infraestructura.Migrations
{
    public partial class sql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_AuditoriaOrdenes",
                columns: table => new
                {
                    idLog = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(nullable: false),
                    Referencia = table.Column<string>(nullable: false),
                    EsError = table.Column<bool>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    Usuario = table.Column<string>(nullable: false),
                    Request = table.Column<string>(nullable: false),
                    Response = table.Column<string>(nullable: false),
                    Metodo = table.Column<string>(nullable: false),
                    Entidad = table.Column<string>(nullable: false),
                    Mensaje = table.Column<string>(nullable: false),
                    Parametros = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditoriaOrdenes", x => x.idLog);
                });

            

            migrationBuilder.CreateTable(
                name: "ordOrdenCompraEstado",
                columns: table => new
                {
                    idEstado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordOrdenCompraEstado", x => x.idEstado);
                });

            migrationBuilder.CreateTable(
                name: "ordPagos",
                columns: table => new
                {
                    idPago = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<int>(nullable: false),
                    fechaPago = table.Column<DateTime>(nullable: false),
                    idUsuario = table.Column<int>(nullable: false),
                    indHabilitado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordPagos", x => x.idPago);
                    table.ForeignKey(
                        name: "FK_ordPagos_AspNetUsers_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            

            migrationBuilder.CreateTable(
                name: "ordOrdenCompra",
                columns: table => new
                {
                    idOrden = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaCreacion = table.Column<DateTime>(nullable: false),
                    idUsuarioCrea = table.Column<int>(nullable: false),
                    idEstado = table.Column<int>(nullable: false),
                    idPago = table.Column<int>(nullable: true),
                    valorTotal = table.Column<double>(nullable: false),
                    indHabilitado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordOrdenCompra", x => x.idOrden);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompra_ordOrdenCompraEstado_idEstado",
                        column: x => x.idEstado,
                        principalTable: "ordOrdenCompraEstado",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompra_ordPagos_idPago",
                        column: x => x.idPago,
                        principalTable: "ordPagos",
                        principalColumn: "idPago",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompra_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ordOrdenCompraDetalle",
                columns: table => new
                {
                    idDetalle = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOrden = table.Column<int>(nullable: false),
                    idProducto = table.Column<int>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    fechaCreacion = table.Column<DateTime>(nullable: false),
                    idUsuarioCrea = table.Column<int>(nullable: false),
                    indHabilitado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordOrdenCompraDetalle", x => x.idDetalle);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompraDetalle_ordOrdenCompra_idOrden",
                        column: x => x.idOrden,
                        principalTable: "ordOrdenCompra",
                        principalColumn: "idOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompraDetalle_catProductos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "catProductos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordOrdenCompraDetalle_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompra_idEstado",
                table: "ordOrdenCompra",
                column: "idEstado");

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompra_idPago",
                table: "ordOrdenCompra",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompra_idUsuarioCrea",
                table: "ordOrdenCompra",
                column: "idUsuarioCrea");

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompraDetalle_idOrden",
                table: "ordOrdenCompraDetalle",
                column: "idOrden");

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompraDetalle_idProducto",
                table: "ordOrdenCompraDetalle",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ordOrdenCompraDetalle_idUsuarioCrea",
                table: "ordOrdenCompraDetalle",
                column: "idUsuarioCrea");

            migrationBuilder.CreateIndex(
                name: "IX_ordPagos_idUsuario",
                table: "ordPagos",
                column: "idUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_AuditoriaOrdenes");

            migrationBuilder.DropTable(
                name: "catDescuentosXProducto");

            migrationBuilder.DropTable(
                name: "ordOrdenCompraDetalle");

            migrationBuilder.DropTable(
                name: "ordOrdenCompra");

            migrationBuilder.DropTable(
                name: "catProductos");

            migrationBuilder.DropTable(
                name: "ordOrdenCompraEstado");

            migrationBuilder.DropTable(
                name: "ordPagos");

        }
    }
}
