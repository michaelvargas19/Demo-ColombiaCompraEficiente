using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Autenticacion.Dominio.Services.Command
{
    public class SesionesServiceCmd : ISesionesServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private readonly IUnitOfWork _ufw;
        private readonly IRolesServiceQuery _rolesService;
        
        public SesionesServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion,
                               IRolesServiceQuery rolesService)
        {
            
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["JwtConfig:issuer"];
            this._ufw = ufwAplicacion;
            this._rolesService = rolesService;
        }

        public LoginResponse IniciarSesion(CredencialesLogin request, string host)
        {
            LoginResponse response = new LoginResponse();
            response.Autenticado = false;
            response.Bloqueado = false;
            
            
            try
            {


                Aplicacion app = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(request.IdAplicacion, true, true)).FirstOrDefault();

                IEnumerable<Claim> claims = null;
                Claim authClaim = new Claim("", String.Empty);

                if ((app != null) && (app.Estado == true) && (app.PermiteJWT == true) && (app.FechaExpiracionLlave.Value.CompareTo(DateTime.Now) >= 0) && (app.LlaveSecreta != null))
                {

                    request.Usuario = request.Usuario.Contains("@") ? request.Usuario.Split("@")[0] : request.Usuario;
                    Usuario usuario = _ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(true, request.Usuario)).FirstOrDefault();

                    SignInResult result = SignInResult.Failed;

                    if ((usuario != null))
                    {
                        claims = _ufw.RepositorySessionesQueries().getClaims(usuario.UserName, app.IdAplicacion);
                        

                        authClaim = claims.Where(c => c.Type == ClaimTypes.AuthenticationMethod).FirstOrDefault();

                        if (authClaim == null)
                        {
                            throw new Exception("El usuario no tiene definido el tipo de Autenticación.");
                        }


                        //if (usuario.EmailConfirmed)
                        //{
                        //    throw new Exception("La cuenta no ha sigo confirmada.");
                        //}



                        //Account Service
                        if (authClaim.Value.CompareTo(EnumTipoAutenticacion.Contraseña.ToString()) == 0)
                        {
                            result = this._ufw.RepositorySessionesCmd().IniciarSesion(request.IdAplicacion, request.Usuario, request.Contrasena);
                        }

                    }
                    else
                    {
                        throw new Exception("La dupla Usuario/Contraseña es incorrecta.");
                    }


                    if (result.Succeeded)
                    {
                        //if ( (usuario.EmailConfirmed==false) )
                        //{
                        //    throw new Exception("Es necesario confirmar la cuenta de email para iniciar sesión. Revisa la bandeja del email registrado.");
                        //}

                        //if ( result.IsLockedOut )
                        //{
                        //    throw new Exception("Su cuenta se encuentra bloqueada. Comuníquese con el contacto de soporte.");
                        //}


                        string algorithm = _ufw.RepositoryQueryAlgoritmo().Find(new AlgoritmoSpecification(app.AlgoritmoDeSeguridad) ).FirstOrDefault().Valor;                        
                        string jwt = TokenGenerator.GenerateTokenJWT(app.LlaveSecreta, algorithm, claims, app.MinutosDeVida.Value, this.Issuer, app.IdAplicacion);

                        response.TokenJWT = new TokenJWT();
                        response.TokenJWT.IdAplicacion = app.IdAplicacion;
                        response.TokenJWT.Token = jwt;
                        
                        response.Mensaje = "Usuario Autenticado";
                        response.Autenticado = true;

                        var tipoAuth = _ufw.RepositoryQueryTipoAuth().Find(new TipoAuthSpecification(usuario.IdTipoAuth)).FirstOrDefault();

                        response.DatosUsuario = new UsuarioQuery();
                        response.DatosUsuario.IdUsuario = usuario.Id;
                        response.DatosUsuario.Usuario = usuario.UserName;
                        response.DatosUsuario.Nombres = usuario.PrimerNombre + " " + usuario.SegundoNombre; ;
                        response.DatosUsuario.Apellidos = usuario.PrimerApellido + " " + usuario.SegundoApellido;
                        response.DatosUsuario.Identificacion = usuario.Identificacion;
                        response.DatosUsuario.TelefonoMovil = usuario.PhoneNumber;
                        response.DatosUsuario.Email = usuario.Email;
                        response.DatosUsuario.IdTipoAuth = usuario.IdTipoAuth;
                        response.DatosUsuario.TipoAutenticacion = (tipoAuth != null)? tipoAuth.Autenticacion : "";
                        response.DatosUsuario.Organizacion = usuario.Organizacion;
                        response.DatosUsuario.Cargo = usuario.Cargo;
                        response.DatosUsuario.Description = usuario.Descripcion;
                        response.DatosUsuario.EsExterno = usuario.EsExterno;

                        

                        response.DatosUsuario.Roles = _rolesService.verRolesPorUsuario_Aplicacion(usuario.UserName, app.IdAplicacion).Roles;
                        
                        //this._ufw.UserIdentityRepository().ResetAccessFailedCount(usuario);

                    }
                    else
                    {
                        //Account Service
                        if (authClaim.Value.CompareTo(EnumTipoAutenticacion.Contraseña.ToString()) == 0)
                        {
                            //this._ufw.UserIdentityRepository().AccessFailed(usuario);
                        }

                        response.Mensaje = "Inicio de sesión inválido";
                    }

                }
                else
                {
                    response.Mensaje = "Inicio de sesión inválido";
                }
                
            }
            catch (Exception e)
            {
                _ufw.InsertarLog(new _LogAutenticacionAPI("Error", request.Usuario, host, "Autenticación API", MethodInfo.GetCurrentMethod().Name, this.ToString(), true, e.Message, e.StackTrace));
                throw e;
            }
            
            return response;

        }


        public TokenJWT validarTokenJWT(TokenJWT token)
        {
            TokenJWT respuesta = new TokenJWT();

            try
            {
                
                Aplicacion aplicacion = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(token.IdAplicacion, true, true)).FirstOrDefault();

                respuesta.IdAplicacion = token.IdAplicacion;
                respuesta.TokenValido = false;
                respuesta.Token = token.Token;

                if (aplicacion != null)
                {
                    respuesta.TokenValido = TokenValidator.ValidarTokenJWT(token.Token, aplicacion.LlaveSecreta, this.Issuer, aplicacion.IdAplicacion, TimeSpan.Zero);
                }

                respuesta.Token = (respuesta.TokenValido) ? "Token Válido" : "Token Inválido";
                
            }
            catch (Exception e)
            {
                throw e;
            }

            return respuesta;

        }

        public TokenJWT renovarToken(TokenJWT token)
        {
            
            TokenJWT newToken = null;
            
            if (validarTokenJWT(token).TokenValido)
            {
                Aplicacion aplicacion = _ufw.RepositoryQueryAplicacion().Find(new AplicacionSpecification(token.IdAplicacion, true, true)).FirstOrDefault();
                newToken = new TokenJWT();
                newToken.IdAplicacion = aplicacion.IdAplicacion;
                newToken.Token = TokenGenerator.renovarTokenJWT(token.Token, aplicacion.LlaveSecreta, aplicacion.MinutosDeVida.Value, this.Issuer, aplicacion.IdAplicacion);
                newToken.TokenValido = true;
            }
            
            return newToken;

        }

        public TokenJWT cambiarContrasena(TokenJWT token)
        {
            throw new NotImplementedException();
        }
    }
}
