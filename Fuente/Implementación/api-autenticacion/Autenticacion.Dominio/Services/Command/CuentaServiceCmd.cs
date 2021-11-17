using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Infraestructura.Email.DTO;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.IRepositories.Command;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class CuentaServiceCmd : ICuentaServiceCmd
    {
        private readonly IUnitOfWork _ufw;
        //private readonly CorreoService emailService;
        private string IdApp;
        private string Issuer;
        private int CodeTimeLife;
        private int CodeLength;


        public CuentaServiceCmd(IConfiguration configuration, IUnitOfWork ufw)
        {
            this._ufw = ufw;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["IdentifierAPP:Issuer"];
            this.CodeTimeLife = Int32.Parse(configuration["IdentifierAPP:CodeTimeLife"]);
            this.CodeLength = Int32.Parse(configuration["IdentifierAPP:CodeLength"]);
        }

        public CodigoCmd codigoRecuperarCuenta(string AbreviacionApp, string usuario, string host)
        {
            CodigoCmd codigo = new CodigoCmd();
            var jrp = "";
            var jrq = "{ \"AbreviacionApp\" : \"" + AbreviacionApp + "\", \"" + usuario + "\":\"" + usuario + "\" }";

            string nickname = usuario.Contains("@") ? usuario.Split("@")[0] : usuario;
            
            AplicacionQuery app = _ufw.ServicioQueryAplicacion().consultarAplicacion(AbreviacionApp, true, true);

            Usuario usuarioApp = this._ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(nickname, usuario)).FirstOrDefault();
            
            if ( (app == null) || (usuarioApp == null) )
            {
                throw new Exception("Ha habido un problema con la solicitud.");
            }


            Claim authClaim = new Claim("", String.Empty);

            //User claims
            IList<Claim> claims = this._ufw.UserIdentityRepository().obtegerClaims(nickname);
            authClaim = claims.Where(c => c.Type == ClaimTypes.AuthenticationMethod).Single();


                //Validate Account Manager like autentication Method
                if (authClaim.Value.CompareTo(EnumTipoAutenticacion.Contraseña.ToString()) != 0)
                {
                    throw new Exception("El usuario se autentica con un directorio externo.");
                }

            
                UsuarioQuery usuarioQ = this._ufw.RolesServiceQuery().verRolesPorUsuario_Aplicacion(usuarioApp.UserName, AbreviacionApp);

                bool hasRole = false;

                foreach (RolQuery rol in app.Roles)
                {
                    if ( usuarioQ.Roles.Any(r=> r.Id == rol.Id ) )
                    {
                        hasRole = true;
                        break;
                    }
                }

                if (!hasRole)
                {
                    throw new Exception("Ha habido un problema con la solicitud.");
                }


                Token token = this._ufw.RepositoryQueryToken().Find(new TokenSpecification(usuarioApp.Id, AbreviacionApp, ClaimTypes.Sid, true)).FirstOrDefault();
            

                if (token != null)
                {
                    if (token.Validado)
                    {
                        throw new Exception("El token ya se ha usado.");
                    }

                    if (token.tokenVigente())
                    {

                        codigo = UtilsTokenServiceCmd.obtenerConfRecuperar(token, codigo);
                        codigo.Mensaje = "Ya se ha generado un Token para el usuario. Por favor revisar el correo electrónico. De lo contrario, intente más tarde.";
                        codigo.CodigoGenerado = true;

                        return codigo;
                    }
                    else
                    {

                        try
                        {
                            token.IndHabilitado = false;
                            

                            this._ufw.BeginTransaction();
                            
                            this._ufw.RepositoryCommandToken().Update(token);

                            this._ufw.SaveChanges();
                            this._ufw.Commit();

                            jrp = JsonConvert.SerializeObject(token);
                            //Auditoría
                            this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "Token actualizado", Parametros = token.Transaccion });

                        }
                        catch(Exception e)
                        {
                            this._ufw.Rollback();


                            jrp = JsonConvert.SerializeObject(token);                        
                            //Auditoría
                            this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = true, Mensaje = e.Message, Parametros = token.Transaccion });

                            throw new Exception("Ha habido un problema con la solicitud. E11");
                        }
                        
                }
                }


                try
                {
                    token = new Token();

                    //code -> 6 digits
                    string code = UtilsOTPServiceCmd.tokenNumerico(this.CodeLength);
                    
                    token.Transaccion = this._ufw.GenerarCodigoTransaccion();
                    token.IdAplicacion = AbreviacionApp;
                    token.UserId = usuarioApp.Id;
                    token.LoginProvider = this.IdApp;
                    token.Name = ClaimTypes.Sid;
                    token.FechaCreacion = DateTime.Now;
                    token.FechaExpiracion = token.FechaCreacion.AddMinutes(this.CodeTimeLife);
                    token.MinutosDeVida = CodeTimeLife;
                    token.LongitudCodigo = CodeLength;
                    token.IndHabilitado = true;
                    token.Value = code;

                

                    List<Claim> claimsJWT = new List<Claim>();
                    claimsJWT.Add(new Claim(ClaimTypes.Sid, codigo.GetHashCode().ToString()));

                    Aplicacion appConfig = this._ufw.ContextoAuthDB().Aplicaciones.Where(a=> a.IdAplicacion==app.IdAplicacion ).FirstOrDefault();
                    string algorithm = this._ufw.RepositoryQueryAlgoritmo().Find(new AlgoritmoSpecification(appConfig.AlgoritmoDeSeguridad)).Select(a => a.Valor).FirstOrDefault();
                    string tokenJWT = TokenGenerator.GenerateTokenJWT(appConfig.LlaveSecreta, algorithm, claimsJWT, CodeTimeLife, this.Issuer, appConfig.IdAplicacion);

                        
                    codigo = UtilsTokenServiceCmd.obtenerConfRecuperar(token, codigo);
                    codigo.Mensaje = "Código enviado al usuario.";
                    codigo.AbreviacionApp = app.IdAplicacion;
                    codigo.TokenJWT = tokenJWT;
                    codigo.CodigoGenerado = true;

                    token.FirmaJWT = TokenValidator.extraerFirmaJWT(tokenJWT);

                try
                {
                    this._ufw.BeginTransaction();

                    this._ufw.RepositoryCommandToken().Add(token);

                    this._ufw.SaveChanges();
                    this._ufw.Commit();


                    //Notificar 
                    try
                    {
                        MailRequest mail = new MailRequest();
                        mail.Destinatarios = new Dictionary<string, string>();
                        mail.Destinatarios.Add("Michael Vargas", "michavarg9@gmail.com");
                        mail.Asunto = "Verificación de identidad";
                        mail.Body = token.Value + " -- " + token.FirmaJWT;

                        _ufw.ConsumidorEmail().EnviarEmail(mail);
                    }
                    catch (Exception e)
                    {
                        //Auditoría
                        jrp = JsonConvert.SerializeObject(token);
                        
                        try
                        {
                            this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = true, Mensaje = e.Message, Parametros = token.Transaccion });

                        }catch (Exception ex) { }

                    }


                    //Auditoría
                    jrp = JsonConvert.SerializeObject(token);
                    this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "Token generado", Parametros = token.Transaccion });


                }
                catch (Exception e)
                {
                    this._ufw.Rollback();

                    throw new Exception("Ha habido un problema con la solicitud. E10");
                }


                    return codigo;

                }
                catch (Exception e)
                {

                    token.IdAplicacion = AbreviacionApp;
                    token.UserId = 0;
                    codigo.Transaccion= token.Transaccion;
                    codigo.Mensaje = e.Message;
                    codigo.CodigoGenerado = false;
                    return codigo;
                }

            
        }

        public ResultadoResponse PostRecuperar(RecuperarCuentaCmd recuperar, string host)
        {
            string usuario = recuperar.Usuario;
            var jrq = JsonConvert.SerializeObject(recuperar);
            var jrp = "";

            ResultadoResponse response = new ResultadoResponse();
            response.Exitoso = false;
            response.Proceso = "Recuperar Cuenta";

            string nickname = usuario.Contains("@") ? usuario.Split("@")[0] : usuario;
            
            Usuario usuarioApp = this._ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(nickname, usuario)).FirstOrDefault();
            AplicacionQuery app = _ufw.ServicioQueryAplicacion().consultarAplicacion(recuperar.AbreviacionApp, true, true);

            if ((app == null) || (usuarioApp == null))
            {
                throw new Exception("Ha habido un problema con la solicitud.");
            }

            
            Token token = this._ufw.RepositoryQueryToken().Find(new TokenSpecification(usuarioApp.Id, recuperar.AbreviacionApp, TokenValidator.extraerFirmaJWT(recuperar.TokenJWT), ClaimTypes.Sid, recuperar.Transaccion, true, true )).FirstOrDefault();
            
            if (token != null)
            {
                if (token.Validado)
                {
                    throw new Exception("El token ya se ha usado.");
                }

                if (token.tokenVigente())
                {
                    if (TokenValidator.validarFirmaJWT(recuperar.TokenJWT, token.FirmaJWT))
                    {
                        if (recuperar.CodigoVerificacion.CompareTo(token.Value) == 0)
                        {
                            IdentityResult result = this._ufw.UserManagerIdentity().RemovePasswordAsync(usuarioApp).Result;
                            result = this._ufw.UserManagerIdentity().AddPasswordAsync(usuarioApp, recuperar.NuevaContrasena).Result;

                            if (result.Succeeded)
                            {
                                response.Exitoso = true;
                                response.Mensaje = "Contraseña Actualizada";
                                token.Validado = true;
                                token.FechaValidacion = DateTime.Now;

                                this._ufw.BeginTransaction();

                                this._ufw.RepositoryCommandToken().Update(token);

                                this._ufw.SaveChanges();
                                this._ufw.Commit();


                                //Notificar 

                                try
                                {
                                    MailRequest mail = new MailRequest();
                                    mail.Destinatarios = new Dictionary<string, string>();
                                    mail.Destinatarios.Add("Michael Vargas", "michavarg9@gmail.com");
                                    mail.Asunto = "Verificación de identidad";
                                    mail.Body = token.Value + " -- " + token.FirmaJWT;

                                    _ufw.ConsumidorEmail().EnviarEmail(mail);
                                }
                                catch (Exception e)
                                {
                                    //Auditoría
                                    jrp = JsonConvert.SerializeObject(token);

                                    try
                                    {
                                        this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = true, Mensaje = e.Message, Parametros = token.Transaccion });

                                    }
                                    catch (Exception ex) { }

                                }



                                jrq = JsonConvert.SerializeObject(response);

                                this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "Contraseña actualizada", Parametros = token.Transaccion });
                            }
                            else
                            {
                                response.Exitoso = false;
                                response.Mensaje = "Problemas en el cambio de contraseña. Comuníquese con el administrador.";

                            }

                        }
                        else
                        {
                            response.Exitoso = false;
                            response.Mensaje = "Código Erroneo";

                        }
                    }
                    else
                    {
                        response.Mensaje = "El toquen JWT no corresponde a la solicitud";
                    }

                }
                else
                {
                    response.Mensaje = "El código de verificación ha expirado";
                    token.IndHabilitado = false;    

                    this._ufw.BeginTransaction();

                    this._ufw.RepositoryCommandToken().Update(token);

                    this._ufw.SaveChanges();
                    this._ufw.Commit();


                    jrq = JsonConvert.SerializeObject(response);
                    this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = usuario, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "Token actualizado", Parametros = token.Transaccion });

                }
            }
            else
            {
                response.Mensaje = "No existe código de verificación";
            }

            

            return response;
        }

        public ResultadoResponse PutContrasena(CuentaCmd cuenta, string host)
        {
            throw new NotImplementedException();
        }
    }
}
