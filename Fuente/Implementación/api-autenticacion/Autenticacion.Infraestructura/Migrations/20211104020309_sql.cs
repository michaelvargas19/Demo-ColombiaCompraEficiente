using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Autenticacion.Infraestructura.Migrations
{
    public partial class sql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_LogAutenticacionAPI",
                columns: table => new
                {
                    IdLog = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsExcepcion = table.Column<bool>(type: "bit", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parametros = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LogAutenticacionAPI", x => x.IdLog);
                });

            migrationBuilder.CreateTable(
                name: "AspNetAlgoritmosDeSeguridad",
                columns: table => new
                {
                    Algoritmo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAlgoritmosDeSeguridad", x => x.Algoritmo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetTiposAutenticacion",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autenticacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsDirectorioActivo = table.Column<bool>(type: "bit", nullable: false),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false),
                    IdAD = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTiposAutenticacion", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "usuTipoDocumento",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    indHabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuTipoDocumento", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "usuTipoPlantillaEmail",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuTipoPlantillaEmail", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetAplicacion",
                columns: table => new
                {
                    IdAplicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    PermiteJWT = table.Column<bool>(type: "bit", nullable: false),
                    AlgoritmoDeSeguridad = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    URLNuevoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URLUpdateUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlaveSecreta = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MinutosDeVida = table.Column<double>(type: "float", nullable: true),
                    FechaExpiracionLlave = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoLlave = table.Column<bool>(type: "bit", nullable: false),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAplicacion", x => x.IdAplicacion);
                    table.ForeignKey(
                        name: "FK_AspNetAplicacion_AspNetAlgoritmosDeSeguridad_AlgoritmoDeSeguridad",
                        column: x => x.AlgoritmoDeSeguridad,
                        principalTable: "AspNetAlgoritmosDeSeguridad",
                        principalColumn: "Algoritmo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idTipoDocumento = table.Column<int>(type: "int", nullable: true),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoAuth = table.Column<int>(type: "int", nullable: false),
                    Organizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsExterno = table.Column<bool>(type: "bit", nullable: false),
                    IndicativoFijo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndicativoMovil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetTiposAutenticacion_IdTipoAuth",
                        column: x => x.IdTipoAuth,
                        principalTable: "AspNetTiposAutenticacion",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_usuTipoDocumento_idTipoDocumento",
                        column: x => x.idTipoDocumento,
                        principalTable: "usuTipoDocumento",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAplicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetAplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalTable: "AspNetAplicacion",
                        principalColumn: "IdAplicacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Transaccion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdAplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongitudCodigo = table.Column<int>(type: "int", nullable: false),
                    MinutosDeVida = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaValidacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    FirmaJWT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name, x.Transaccion });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuPlantillaEmail",
                columns: table => new
                {
                    IdPlantilla = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTipoPlantilla = table.Column<int>(type: "int", nullable: false),
                    IdAplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Plantilla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuarioCrea = table.Column<int>(type: "int", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idUsuarioModifica = table.Column<int>(type: "int", nullable: true),
                    IndHabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuPlantillaEmail", x => x.IdPlantilla);
                    table.ForeignKey(
                        name: "FK_usuPlantillaEmail_AspNetUsers_idUsuarioCrea",
                        column: x => x.idUsuarioCrea,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuPlantillaEmail_AspNetUsers_idUsuarioModifica",
                        column: x => x.idUsuarioModifica,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetAlgoritmosDeSeguridad",
                columns: new[] { "Algoritmo", "IndHabilitado", "Valor" },
                values: new object[,]
                {
                    { "EcdsaSha256", true, "ES256" },
                    { "Aes256CbcHmacSha512", true, "A256CBC-HS512" },
                    { "RsaPKCS1", true, "RSA1_5" },
                    { "Sha384", true, "SHA384" },
                    { "Sha256", true, "SHA256" },
                    { "RsaOAEP", true, "RSA-OAEP" },
                    { "Aes256KW", true, "A256KW" },
                    { "Aes128KW", true, "A128KW" },
                    { "Aes192CbcHmacSha384", true, "A192CBC-HS384" },
                    { "HmacSha256", true, "HS256" },
                    { "Aes128CbcHmacSha256", true, "A128CBC-HS256" },
                    { "Sha512", true, "SHA512" },
                    { "RsaSsaPssSha384", true, "PS384" },
                    { "RsaSsaPssSha512", true, "PS512" },
                    { "EcdsaSha384", true, "ES384" },
                    { "HmacSha384", true, "HS384" },
                    { "HmacSha512", true, "HS512" },
                    { "EcdsaSha512", true, "ES512" },
                    { "RsaSha256", true, "RS256" },
                    { "RsaSha384", true, "RS384" },
                    { "RsaSha512", true, "RS512" },
                    { "RsaSsaPssSha256", true, "PS256" },
                    { "None", true, "none" }
                });

            migrationBuilder.InsertData(
                table: "AspNetTiposAutenticacion",
                columns: new[] { "IdTipo", "Autenticacion", "EsDirectorioActivo", "IdAD", "IndHabilitado" },
                values: new object[] { 1, "Contraseña", false, null, true });

            migrationBuilder.InsertData(
                table: "usuTipoDocumento",
                columns: new[] { "IdTipo", "Codigo", "Descripcion", "Nombre", "Orden", "indHabilitado" },
                values: new object[,]
                {
                    { 14, "TI", "", "Tarjeta de identidad", 7, true },
                    { 9, "PA", "", "Pasaporte", 6, true },
                    { 13, "SI", "", "Sin identificación", 10, true },
                    { 12, "SC", "", "Salvoconducto", 10, true },
                    { 11, "RC", "", "Registro civil", 10, true },
                    { 10, "PE", "", "Permiso especial de permanencia", 10, true },
                    { 8, "NIT", "", "NIT", 5, true },
                    { 3, "CC", "", "Cédula de ciudadanía", 1, true },
                    { 6, "DE", "", "Documento extranjero", 4, true },
                    { 5, "CN", "", "Certificado de nacido vivo", 10, false },
                    { 4, "CE", "", "Cédula de extranjería", 2, true },
                    { 2, "CD", "", "Carnet diplomático", 10, true },
                    { 1, "AS", "", "Adulto sin identificación", 10, true },
                    { 7, "MS", "", "Menor sin identificación", 10, true }
                });

            migrationBuilder.InsertData(
                table: "usuTipoPlantillaEmail",
                columns: new[] { "IdTipo", "Descripcion", "FechaCreacion", "FechaModificacion", "IndHabilitado", "Nombre" },
                values: new object[,]
                {
                    { 3, "Usada para enviar el enlace de recuperación de cuenta", new DateTime(2021, 11, 3, 21, 3, 8, 794, DateTimeKind.Local).AddTicks(7773), null, true, "Recuperar Cuenta" },
                    { 1, "Usada para notificar el registro de un nuevo usuario", new DateTime(2021, 11, 3, 21, 3, 8, 794, DateTimeKind.Local).AddTicks(6860), null, true, "Solicitud de confirmación" },
                    { 2, "Usada para notificar la confirmación de cuenta", new DateTime(2021, 11, 3, 21, 3, 8, 794, DateTimeKind.Local).AddTicks(7729), null, true, "Confirmación de Cuenta" },
                    { 4, "Usada para notificar el cambio de contraseña", new DateTime(2021, 11, 3, 21, 3, 8, 794, DateTimeKind.Local).AddTicks(7793), null, true, "Cambio de Contraseña" }
                });

            migrationBuilder.InsertData(
                table: "AspNetAplicacion",
                columns: new[] { "IdAplicacion", "AlgoritmoDeSeguridad", "EmailContacto", "Estado", "EstadoLlave", "FechaExpiracionLlave", "IndHabilitado", "LlaveSecreta", "MinutosDeVida", "Nombre", "PermiteJWT", "URLNuevoUsuario", "URLUpdateUsuario" },
                values: new object[] { "Manager", "HmacSha512", "michavarg9@gmail.com", true, true, new DateTime(2023, 11, 3, 21, 3, 8, 791, DateTimeKind.Local).AddTicks(7133), true, "XYyZhvxK6T5xJO1dfiDOPrE5ZOldILWAGx5aStCYPpv5p495p2TRaoOSSU9Ddm6PvlFu6LUL3ttIUy9K5UHvvGqncNpvZW3c7zUfOpdqnZWl53bEVBwUe8dGbvJ9BSRTA4gDP9UT5ZHoiMw07wvzLcpsybShy1eKl1IGb1nmkGwHJl5EHYYrWCon6GCF30wv3v8jT0fmRY9AxbsRHGWc4ECQe9uy4vtUb0iUzD9vuqQWzxRpkwpptxjDqqBv3Qzy", 12.0, "Account Service", true, "", "" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Cargo", "ConcurrencyStamp", "Descripcion", "Email", "EmailConfirmed", "EsExterno", "IdTipoAuth", "Identificacion", "IndHabilitado", "IndicativoFijo", "IndicativoMovil", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Organizacion", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PrimerApellido", "PrimerNombre", "SecurityStamp", "SegundoApellido", "SegundoNombre", "Telefono", "TwoFactorEnabled", "UserName", "idTipoDocumento" },
                values: new object[] { 1, 0, "Administrador del sistema", "9dfe1c2a-0f95-48e4-ad8f-114c4c9c99c7", "Administrador del sistema de autenticación", "admin@admin.org.co", true, false, 1, null, true, null, null, false, null, "ADMIN@ADMIN.ORG.CO", "ADMIN", "PUJ", "AQAAAAEAACcQAAAAEDvsJrU5P2uO7jfhKVQTK2rMCwYlOAoWC3AzIGB+iktmo8A2515Utzul5+KXfWEjqQ==", null, false, "Admin", "User", "XVMFBE37LCN4TNGMZSHLPHBV7FIVHBQG", "Default", "", null, false, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Descripcion", "Display", "IdAplicacion", "IndHabilitado", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "4489e762-6a2e-4bfd-9574-b34058a1869e", "Usuario con permisos Full", "PowerUser", "Manager", true, "PowerUser", "POWERUSER" },
                    { 2, "1042f7b1-87e4-41a4-97f9-b5c76ada82c5", "Usuario con permisos de Admin", "Administrador", "Manager", true, "Administrador", "ADMINISTRADOR" },
                    { 3, "40968329-d9de-43bb-b746-06c7c501781c", "Cliente del sistema", "Cliente", "Manager", true, "Cliente", "CLIENTE" },
                    { 4, "fb5da651-2faf-4320-83ba-c59d72d36f92", "Proveedor del sistema", "Proveedor", "Manager", true, "Proveedor", "PROVEEDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "Contraseña", 1 },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress", "", 1 },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri", "", 1 },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "usuPlantillaEmail",
                columns: new[] { "IdPlantilla", "Descripcion", "FechaCreacion", "FechaModificacion", "IdAplicacion", "IndHabilitado", "Nombre", "Plantilla", "idTipoPlantilla", "idUsuarioCrea", "idUsuarioModifica" },
                values: new object[,]
                {
                    { 1, "Estructura usada para solicitar la confirmación de la cuenta via email", new DateTime(2021, 11, 3, 21, 3, 8, 794, DateTimeKind.Local).AddTicks(9215), null, "Manager", true, "Notificación de registro", "<!DOCTYPE html><html><head><meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" ><title>Mozilla</title></head><body>Usuario: $Usuario Enlace: $Enlace</body></html>", 1, 1, null },
                    { 2, "Estructura usada para notificar al usuario la confirmación de email", new DateTime(2021, 11, 3, 21, 3, 8, 795, DateTimeKind.Local).AddTicks(559), null, "Manager", true, "Notificación de confirmación", "", 2, 1, null },
                    { 3, "Estructura usada para notificar al usuario el enlace para la recuperación de cuenta", new DateTime(2021, 11, 3, 21, 3, 8, 795, DateTimeKind.Local).AddTicks(637), null, "Manager", true, "Notificación de enlace para recuperación", "", 3, 1, null },
                    { 4, "Estructura usada para notificar al usuario el cambio exitoso de contraseña", new DateTime(2021, 11, 3, 21, 3, 8, 795, DateTimeKind.Local).AddTicks(660), null, "Manager", true, "Notificación de cambio de contraseña", "", 4, 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 1 },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 2 },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 3 },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 4 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetAplicacion_AlgoritmoDeSeguridad",
                table: "AspNetAplicacion",
                column: "AlgoritmoDeSeguridad");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IdAplicacion",
                table: "AspNetRoles",
                column: "IdAplicacion");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTipoAuth",
                table: "AspNetUsers",
                column: "IdTipoAuth");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_idTipoDocumento",
                table: "AspNetUsers",
                column: "idTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_usuPlantillaEmail_idUsuarioCrea",
                table: "usuPlantillaEmail",
                column: "idUsuarioCrea");

            migrationBuilder.CreateIndex(
                name: "IX_usuPlantillaEmail_idUsuarioModifica",
                table: "usuPlantillaEmail",
                column: "idUsuarioModifica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_LogAutenticacionAPI");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "usuPlantillaEmail");

            migrationBuilder.DropTable(
                name: "usuTipoPlantillaEmail");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetAplicacion");

            migrationBuilder.DropTable(
                name: "AspNetTiposAutenticacion");

            migrationBuilder.DropTable(
                name: "usuTipoDocumento");

            migrationBuilder.DropTable(
                name: "AspNetAlgoritmosDeSeguridad");
        }
    }
}
