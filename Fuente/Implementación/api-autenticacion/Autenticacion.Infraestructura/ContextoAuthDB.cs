using Autenticacion.Infraestructura.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Autenticacion.Infraestructura
{
    public class ContextoAuthDB : IdentityDbContext<Usuario, Rol, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, Token>
    {
        public ContextoAuthDB(DbContextOptions<ContextoAuthDB> options)
            : base(options)
        {
        }
       

        public DbSet<TipoDocumento> TiposDocumento { get; set; }

        public DbSet<AlgoritmoDeSeguridad> AlgoritmosDeSeguridad { get; set; }

        public DbSet<Aplicacion> Aplicaciones { get; set; }

        public DbSet<TipoAutenticacion> TiposAutenticacion { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<TipoPlantillaEmail> TiposPlantillasEmail { get; set; }
        
        public DbSet<PlantillaEmail> PlantillasEmail { get; set; }

        public DbSet<_LogAutenticacionAPI> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Token>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name, p.Transaccion });


            builder.Entity<TipoAutenticacion>().HasData(
                            new TipoAutenticacion() { IdTipo = 1, Autenticacion = "Contraseña", EsDirectorioActivo = false, IdAD = null, IndHabilitado = true }
                         );

            //Insert App
            builder.Entity<Aplicacion>().HasData(
                            new Aplicacion()
                            {
                                IdAplicacion = "Manager",
                                Nombre = "Account Service",
                                EmailContacto = "michavarg9@gmail.com",
                                Estado = true,
                                PermiteJWT = true,
                                EstadoLlave = true,
                                AlgoritmoDeSeguridad = "HmacSha512",
                                LlaveSecreta = "XYyZhvxK6T5xJO1dfiDOPrE5ZOldILWAGx5aStCYPpv5p495p2TRaoOSSU9Ddm6PvlFu6LUL3ttIUy9K5UHvvGqncNpvZW3c7zUfOpdqnZWl53bEVBwUe8dGbvJ9BSRTA4gDP9UT5ZHoiMw07wvzLcpsybShy1eKl1IGb1nmkGwHJl5EHYYrWCon6GCF30wv3v8jT0fmRY9AxbsRHGWc4ECQe9uy4vtUb0iUzD9vuqQWzxRpkwpptxjDqqBv3Qzy",
                                URLNuevoUsuario = "",
                                URLUpdateUsuario = "",
                                //URLConfirmacionEmail = "",
                                MinutosDeVida = 12,
                                FechaExpiracionLlave = DateTime.Now.AddYears(2),
                                IndHabilitado = true
                            }
                         ); ;

            //Insert Role
            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 1, Name = "PowerUser", NormalizedName = "POWERUSER", IdAplicacion = "Manager", Display = "PowerUser", Descripcion = "Usuario con permisos Full", IndHabilitado = true }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 2, Name = "Administrador", NormalizedName = "ADMINISTRADOR", IdAplicacion = "Manager", Display = "Administrador", Descripcion = "Usuario con permisos de Admin", IndHabilitado = true }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 3, Name = "Cliente", NormalizedName = "CLIENTE", IdAplicacion = "Manager", Display = "Cliente", Descripcion = "Cliente del sistema", IndHabilitado = true }
                         );

            builder.Entity<Rol>().HasData(
                            new Rol() { Id = 4, Name = "Proveedor", NormalizedName = "PROVEEDOR", IdAplicacion = "Manager", Display = "Proveedor", Descripcion = "Proveedor del sistema", IndHabilitado = true }
                         );

            //Insert Role Claims--> Identificador de la APP
            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 1, RoleId = 1, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 2, RoleId = 2, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 3, RoleId = 3, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );

            builder.Entity<IdentityRoleClaim<int>>().HasData(
                            new
                            { Id = 4, RoleId = 4, ClaimType = ClaimTypes.System, ClaimValue = "Manager" }
                         );


            //Insert User
            builder.Entity<Usuario>().HasData(
                            new Usuario()
                            {
                                Id = 1,
                                UserName = "Admin",
                                NormalizedUserName = "ADMIN",
                                Email = "admin@admin.org.co",
                                NormalizedEmail = "ADMIN@ADMIN.ORG.CO",
                                EmailConfirmed = true,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                LockoutEnabled = false,
                                AccessFailedCount = 0,
                                PrimerNombre = "User",
                                SegundoNombre = "",
                                PrimerApellido = "Admin",
                                SegundoApellido = "Default",
                                IdTipoAuth = 1,
                                PasswordHash = "AQAAAAEAACcQAAAAEDvsJrU5P2uO7jfhKVQTK2rMCwYlOAoWC3AzIGB+iktmo8A2515Utzul5+KXfWEjqQ==",
                                SecurityStamp = "XVMFBE37LCN4TNGMZSHLPHBV7FIVHBQG",
                                Cargo = "Administrador del sistema",
                                Descripcion = "Administrador del sistema de autenticación",
                                Organizacion = "PUJ",
                                EsExterno = false,
                                IndHabilitado = true
                            });



            //Insert User Claims
            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 1,
                                UserId = 1,
                                ClaimType = ClaimTypes.AuthenticationMethod,
                                ClaimValue = "Contraseña"
                            }
                         );

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 2,
                                UserId = 1,
                                ClaimType = ClaimTypes.StreetAddress,
                                ClaimValue = String.Empty
                            });

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 3,
                                UserId = 1,
                                ClaimType = ClaimTypes.Uri,
                                ClaimValue = String.Empty
                            });

            builder.Entity<IdentityUserClaim<int>>().HasData(
                            new
                            {
                                Id = 4,
                                UserId = 1,
                                ClaimType = ClaimTypes.Locality,
                                ClaimValue = String.Empty
                            });


            //Insert User-Role Relation
            builder.Entity<IdentityUserRole<int>>().HasData(
                            new { UserId = 1, RoleId = 1 }
                         );


            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha256", Valor = "ES256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha384", Valor = "ES384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "EcdsaSha512", Valor = "ES512", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha384", Valor = "HS384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha512", Valor = "HS512", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "None", Valor = "none", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha256", Valor = "RS256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha384", Valor = "RS384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSha512", Valor = "RS512", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha256", Valor = "PS256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha384", Valor = "PS384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaSsaPssSha512", Valor = "PS512", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes128CbcHmacSha256", Valor = "A128CBC-HS256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "HmacSha256", Valor = "HS256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes192CbcHmacSha384", Valor = "A192CBC-HS384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes128KW", Valor = "A128KW", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes256KW", Valor = "A256KW", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaOAEP", Valor = "RSA-OAEP", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha256", Valor = "SHA256", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha384", Valor = "SHA384", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Sha512", Valor = "SHA512", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "RsaPKCS1", Valor = "RSA1_5", IndHabilitado = true });
            builder.Entity<AlgoritmoDeSeguridad>().HasData(new AlgoritmoDeSeguridad { Algoritmo = "Aes256CbcHmacSha512", Valor = "A256CBC-HS512", IndHabilitado = true });



            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 1, Codigo = "AS", Nombre = "Adulto sin identificación", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 2, Codigo = "CD", Nombre = "Carnet diplomático", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 3, Codigo = "CC", Nombre = "Cédula de ciudadanía", Descripcion = "", Orden = 1, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 4, Codigo = "CE", Nombre = "Cédula de extranjería", Descripcion = "", Orden = 2, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 5, Codigo = "CN", Nombre = "Certificado de nacido vivo", Descripcion = "", Orden = 10, indHabilitado = false });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 6, Codigo = "DE", Nombre = "Documento extranjero", Descripcion = "", Orden = 4, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 7, Codigo = "MS", Nombre = "Menor sin identificación", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 8, Codigo = "NIT", Nombre = "NIT", Descripcion = "", Orden = 5, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 9, Codigo = "PA", Nombre = "Pasaporte", Descripcion = "", Orden = 6, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 10, Codigo = "PE", Nombre = "Permiso especial de permanencia", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 11, Codigo = "RC", Nombre = "Registro civil", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 12, Codigo = "SC", Nombre = "Salvoconducto", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 13, Codigo = "SI", Nombre = "Sin identificación", Descripcion = "", Orden = 10, indHabilitado = true });
            builder.Entity<TipoDocumento>().HasData(new TipoDocumento { IdTipo = 14, Codigo = "TI", Nombre = "Tarjeta de identidad", Descripcion = "", Orden = 7, indHabilitado = true });


            builder.Entity<TipoPlantillaEmail>().HasData(new TipoPlantillaEmail { IdTipo = 1,  Nombre = "Solicitud de confirmación", Descripcion = "Usada para notificar el registro de un nuevo usuario", FechaCreacion = DateTime.Now, FechaModificacion = null, IndHabilitado = true });
            builder.Entity<TipoPlantillaEmail>().HasData(new TipoPlantillaEmail { IdTipo = 2, Nombre = "Confirmación de Cuenta", Descripcion = "Usada para notificar la confirmación de cuenta", FechaCreacion = DateTime.Now, FechaModificacion = null, IndHabilitado = true });
            builder.Entity<TipoPlantillaEmail>().HasData(new TipoPlantillaEmail { IdTipo = 3, Nombre = "Recuperar Cuenta", Descripcion = "Usada para enviar el enlace de recuperación de cuenta", FechaCreacion = DateTime.Now, FechaModificacion = null, IndHabilitado = true });
            builder.Entity<TipoPlantillaEmail>().HasData(new TipoPlantillaEmail { IdTipo = 4, Nombre = "Cambio de Contraseña", Descripcion = "Usada para notificar el cambio de contraseña", FechaCreacion = DateTime.Now, FechaModificacion = null, IndHabilitado = true });


            builder.Entity<PlantillaEmail>().HasData(new PlantillaEmail { IdPlantilla = 1, idTipoPlantilla = 1, IdAplicacion = "Manager", Nombre = "Notificación de registro", Descripcion = "Estructura usada para solicitar la confirmación de la cuenta via email", 
                                                                          Plantilla = "<!DOCTYPE html><html><head><meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" ><title>Mozilla</title></head><body>Usuario: $Usuario Enlace: $Enlace</body></html>", 
                                                                          FechaCreacion = DateTime.Now, idUsuarioCrea=1, FechaModificacion = null, idUsuarioModifica=null, IndHabilitado = true });
            builder.Entity<PlantillaEmail>().HasData(new PlantillaEmail { IdPlantilla = 2, idTipoPlantilla = 2, IdAplicacion = "Manager", Nombre = "Notificación de confirmación", Descripcion = "Estructura usada para notificar al usuario la confirmación de email", Plantilla = "", FechaCreacion = DateTime.Now, idUsuarioCrea = 1, FechaModificacion = null, idUsuarioModifica = null, IndHabilitado = true });
            builder.Entity<PlantillaEmail>().HasData(new PlantillaEmail { IdPlantilla = 3, idTipoPlantilla = 3, IdAplicacion = "Manager", Nombre = "Notificación de enlace para recuperación", Descripcion = "Estructura usada para notificar al usuario el enlace para la recuperación de cuenta", Plantilla = "", FechaCreacion = DateTime.Now, idUsuarioCrea = 1, FechaModificacion = null, idUsuarioModifica = null, IndHabilitado = true });
            builder.Entity<PlantillaEmail>().HasData(new PlantillaEmail { IdPlantilla = 4, idTipoPlantilla = 4, IdAplicacion = "Manager", Nombre = "Notificación de cambio de contraseña", Descripcion = "Estructura usada para notificar al usuario el cambio exitoso de contraseña", Plantilla = "", FechaCreacion = DateTime.Now, idUsuarioCrea = 1, FechaModificacion = null, idUsuarioModifica = null, IndHabilitado = true });

        }


        public void Log(_LogAutenticacionAPI log)
        {
            if (!string.IsNullOrEmpty(log.Metodo))
            {
                this.Logs.Add(log);
                this.SaveChanges();
            }
        }
    }
}
