using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalogos.Infraestructura.Migrations
{
    public partial class sql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "catCatalogos",
                columns: table => new
                {
                    idCatalogo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    CodigoCatalogo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Calificacion = table.Column<double>(nullable: false),
                    idUsuarioCrea = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catCatalogos", x => x.idCatalogo);
                    table.ForeignKey(
                        name: "FK_catCatalogos_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "catMultimediaXCatalogo",
                columns: table => new
                {
                    idMultimedia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCatalogo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    idUsuarioCrea = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catMultimediaXCatalogo", x => x.idMultimedia);
                    table.ForeignKey(
                        name: "FK_catMultimediaXCatalogo_catCatalogos_idCatalogo",
                        column: x => x.idCatalogo,
                        principalTable: "catCatalogos",
                        principalColumn: "idCatalogo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catMultimediaXCatalogo_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "catProductos",
                columns: table => new
                {
                    idProducto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCatalogo = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Prioridad = table.Column<double>(nullable: false),
                    iva = table.Column<double>(nullable: false),
                    PesoKg = table.Column<double>(nullable: false),
                    ValorUnitario = table.Column<double>(nullable: false),
                    NivelInventario = table.Column<int>(nullable: false),
                    NivelAdvertencia = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    EnAlmacen = table.Column<bool>(nullable: false),
                    Calificacion = table.Column<double>(nullable: false),
                    fechaCreacion = table.Column<DateTime>(nullable: false),
                    idUsuarioCrea = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catProductos", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK_catProductos_catCatalogos_idCatalogo",
                        column: x => x.idCatalogo,
                        principalTable: "catCatalogos",
                        principalColumn: "idCatalogo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProductos_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "catDescuentosXProducto",
                columns: table => new
                {
                    idDescuento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProducto = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Porcentaje = table.Column<double>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catDescuentosXProducto", x => x.idDescuento);
                    table.ForeignKey(
                        name: "FK_catDescuentosXProducto_catProductos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "catProductos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catMultimediaXProducto",
                columns: table => new
                {
                    idMultimedia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProducto = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catMultimediaXProducto", x => x.idMultimedia);
                    table.ForeignKey(
                        name: "FK_catMultimediaXProducto_catProductos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "catProductos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catCatalogos_idUsuarioCrea",
                table: "catCatalogos",
                column: "idUsuarioCrea");

            migrationBuilder.CreateIndex(
                name: "IX_catDescuentosXProducto_idProducto",
                table: "catDescuentosXProducto",
                column: "idProducto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catMultimediaXCatalogo_idCatalogo",
                table: "catMultimediaXCatalogo",
                column: "idCatalogo");

            migrationBuilder.CreateIndex(
                name: "IX_catMultimediaXCatalogo_idUsuarioCrea",
                table: "catMultimediaXCatalogo",
                column: "idUsuarioCrea");

            migrationBuilder.CreateIndex(
                name: "IX_catMultimediaXProducto_idProducto",
                table: "catMultimediaXProducto",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_catProductos_idCatalogo",
                table: "catProductos",
                column: "idCatalogo");

            migrationBuilder.CreateIndex(
                name: "IX_catProductos_idUsuarioCrea",
                table: "catProductos",
                column: "idUsuarioCrea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catDescuentosXProducto");

            migrationBuilder.DropTable(
                name: "catMultimediaXCatalogo");

            migrationBuilder.DropTable(
                name: "catMultimediaXProducto");

            migrationBuilder.DropTable(
                name: "catProductos");

            migrationBuilder.DropTable(
                name: "catCatalogos");

        }
    }
}
