using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Autenticacion.Infraestructura.Email.DTO;
using Autenticacion.Infraestructura.Entities.Auth;
using Autenticacion.Infraestructura.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Autenticacion.Dominio.Services.Command
{
    public class UsuariosServiceCmd : IUsuariosServiceCmd
    {
        private readonly string IdApp;
        private readonly string Issuer;
        private int CodeTimeLife;
        private int CodeLength;
        private readonly IUnitOfWork _ufw;
        
        public UsuariosServiceCmd(IConfiguration configuration,
                               IUnitOfWork ufwAplicacion)
        {
            
            this._ufw = ufwAplicacion;
            this.IdApp = configuration["IdentifierAPP:Id"];
            this.Issuer = configuration["IdentifierAPP:Issuer"];
            this.CodeTimeLife = Int32.Parse(configuration["IdentifierAPP:CodeTimeLife"]);
            this.CodeLength = Int32.Parse(configuration["IdentifierAPP:CodeLength"]);

        }

        public UsuarioQuery registrarUsuario(UsuarioCmd usuario, string host)
        {
            UsuarioQuery usrQ = null;
            var jrp = "";
            var jrq = "";


            try
            {

                if (!usuario.EsValido())
                {
                    throw new Exception("La información no es válida");
                }

                AplicacionQuery app =  _ufw.ServicioQueryAplicacion().consultarAplicacion(usuario.IdAplicacion, true, true);
                
                if ( (app == null) )
                {
                    throw new Exception("Hay problemas con la solicitud. Por favor intente más tarde.");
                }


                if( !app.Roles.Any(r => r.Id == usuario.IdRole))
                {
                    throw new Exception("Hay problemas con la solicitud. Por favor intente más tarde.");
                }


                if ( (_ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(true, usuario.Usuario)).ToList().Count > 0 ) )
                {
                    throw new Exception("Ya existe un usuario registrado este usuario");
                }

                if ((_ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(false, usuario.Usuario)).ToList().Count > 0))
                {
                    throw new Exception("Ya existe un usuario registrado este usuario pero se encuentra inhabilitado. Comuniquese con la línea de soporte para solucionar la habilitación.");
                }

                if ((_ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(usuario.Email, true)).ToList().Count > 0))
                {
                    throw new Exception("Ya existe un usuario registrado con este email");
                }

                if ((_ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(usuario.Email, false)).ToList().Count > 0))
                {
                    throw new Exception("Ya existe un usuario registrado este email pero se encuentra inhabilitado. Comuniquese con la línea de soporte para solucionar la habilitación.");
                }

                if ( (_ufw.RepositoryQueryTipoAuth().Find(new TipoAuthSpecification(usuario.IdTipoAuth)).ToList().Count == 0 ) )
                {
                    throw new Exception("El Tipo de Autenticación es inválido");
                }

                Usuario usrE = new Usuario();
                
                usrE.UserName = usuario.Usuario;
                usrE.Email = usuario.Email;
                usrE.IdTipoAuth = usuario.IdTipoAuth;

                usrE.PrimerNombre = usuario.PrimerNombre;
                usrE.SegundoNombre = usuario.SegundoNombre;
                usrE.PrimerApellido = usuario.PrimerApellido;
                usrE.SegundoApellido = usuario.SegundoApellido;

                usrE.Identificacion = usuario.Identificacion;
                usrE.IndicativoFijo = usuario.IndicativoFijo;
                usrE.Telefono= usuario.Telefono;
                usrE.IndicativoMovil = usuario.IndicativoMovil;
                usrE.PhoneNumber = usuario.Celular;
                
                usrE.Organizacion = usuario.Organizacion;
                usrE.Cargo = usuario.Cargo;
                usrE.Descripcion = usuario.Descripcion;
                usrE.EsExterno = usuario.EsExterno;

                usrE.IndHabilitado = true;

                //_ufw.BeginTransaction();
                IdentityResult result = _ufw.UserIdentityRepository().CreateUser(usrE, usuario.Contrasena);
                
                if (result.Succeeded)
                {
                    usrE = _ufw.RepositoryQueryUsuario().Find(new UsuarioSpecification(true, usuario.Usuario)).FirstOrDefault();

                    _ufw.RoleIdentityRepository().CreateRelationUserRoleAsync(usrE.UserName, usuario.IdRole);


                    if (!result.Succeeded)
                    {
                        _ufw.Rollback();
                        throw new Exception("Ha ocurrido un error, E14. Por favor comuníquese a la línea de soporte o intente más tarde. Gracias.");
                    }
                                       

                    

                    usrQ = new UsuarioQuery();
                    usrQ.IdUsuario = usrE.Id;
                    usrQ.Usuario = usrE.UserName;
                    usrQ.Nombres = usrE.PrimerNombre + " " + usrE.SegundoNombre;
                    usrQ.Apellidos = usrE.PrimerApellido + " " + usrE.SegundoApellido;
                    usrQ.Identificacion = usrE.Identificacion;
                    usrQ.TelefonoMovil = usrE.PhoneNumber;
                    usrQ.Email = usrE.Email;
                    usrQ.IdTipoAuth = usrE.IdTipoAuth;
                    usrQ.TipoAutenticacion = (usrE.TipoAutenticacion != null) ? usrE.TipoAutenticacion.Autenticacion : "";
                    usrQ.Organizacion = usrE.Organizacion;
                    usrQ.Cargo = usrE.Cargo;
                    usrQ.Description = usrE.Descripcion;
                    usrQ.EsExterno = usrE.EsExterno;


                    Token token = new Token();

                    //code -> 6 digits
                    string code = UtilsOTPServiceCmd.tokenNumerico(this.CodeLength);

                    token.Transaccion = this._ufw.GenerarCodigoTransaccion();
                    token.IdAplicacion = app.IdAplicacion;
                    token.UserId = usrQ.IdUsuario;
                    token.LoginProvider = this.IdApp;
                    token.Name = ClaimTypes.Sid;
                    token.FechaCreacion = DateTime.Now;
                    token.FechaExpiracion = token.FechaCreacion.AddMinutes(this.CodeTimeLife);
                    token.MinutosDeVida = CodeTimeLife;
                    token.LongitudCodigo = CodeLength;
                    token.IndHabilitado = true;
                    token.Value = code;

                    CodigoCmd codigo = new CodigoCmd();

                    List<Claim> claimsJWT = new List<Claim>();
                    claimsJWT.Add(new Claim(ClaimTypes.Sid, codigo.GetHashCode().ToString()));

                    Aplicacion appConfig = this._ufw.ContextoAuthDB().Aplicaciones.Where(a => a.IdAplicacion == app.IdAplicacion).FirstOrDefault();
                    string algorithm = this._ufw.RepositoryQueryAlgoritmo().Find(new AlgoritmoSpecification(appConfig.AlgoritmoDeSeguridad)).Select(a => a.Valor).FirstOrDefault();
                    string tokenJWT = TokenGenerator.GenerateTokenJWT(appConfig.LlaveSecreta, algorithm, claimsJWT, CodeTimeLife, this.Issuer, appConfig.IdAplicacion);


                    codigo = UtilsTokenServiceCmd.obtenerConfRecuperar(token, codigo);
                    codigo.Mensaje = "Código enviado al usuario.";
                    codigo.AbreviacionApp = usuario.IdAplicacion;
                    codigo.TokenJWT = tokenJWT;
                    codigo.CodigoGenerado = true;

                    token.FirmaJWT = TokenValidator.extraerFirmaJWT(tokenJWT);


                    //Notificar 
                    try
                    {
                        PlantillaEmail plantilla = this._ufw.RepositoryQueryPlantillaEmail().Find(new PlantillaEmailSpecification(1, app.IdAplicacion)).FirstOrDefault();
                        
                        if( plantilla != null)
                        {
                            MailRequest mail = new MailRequest();
                            mail.Destinatarios = new Dictionary<string, string>();
                            mail.Destinatarios.Add("Michael Vargas", "paraisoverdeboyaca@gmail.com");
                            mail.Asunto = "Confirmación de Cuenta";
                            mail.Body = plantilla.Plantilla;

                            _ufw.ConsumidorEmail().EnviarEmail(mail);
                        }
                        else {
                            throw new Exception("No se ha encontrado una plantilla para la notificación. Por favor comuníquese con el contacto de soporte.");
                        }

                        
                    }
                    catch (Exception e)
                    {
                        //Auditoría
                        jrq = JsonConvert.SerializeObject(usuario);
                        jrp = JsonConvert.SerializeObject(token);


                        try
                        {
                            this._ufw.LogServiceCmd().AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = jrq, Request = jrq, Response = jrp, Host = host, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = true, Mensaje = e.Message, Parametros = token.Transaccion });

                        }
                        catch (Exception ex) { }

                    }

                    _ufw.SaveChanges();
                    //_ufw.Commit();

                }
                else
                {
                    throw new Exception(result.Errors.ToArray().ToString());
                }
                



                


                //usrE.Roles = new List<Rol   >();

                //foreach (Rol r in usrE.Roles)
                //{
                //    RolQuery rolQ = new RolQuery();
                //    rolQ.Id = r.Id;
                //    rolQ.Nombre = r.Name;
                //    rolQ.Descripcion = r.Descripcion;
                //    usrQ.Roles.Add(rolQ);
                //}
                
                
            }
            catch (Exception e)
            {
                _ufw.Rollback();
                throw e;
            }


            return usrQ;

        }

        public UsuarioQuery actualizarUsuario(UsuarioCmd usuario)
        {
            throw new NotImplementedException();
        }

        public ResultadoCmd asignarRol(AsignarRolUserCmd rol)
        {
            try
            {
                ResultadoCmd result = new ResultadoCmd();
                result.Proceso = "Asignar Rol";
                result.Exitoso = false;

                IdentityResult rest = this._ufw.UserIdentityRepository().AsigRoleUser(rol.Usuario, rol.IdRol);

                if (rest.Succeeded)
                {
                    result.Exitoso = true;
                    result.Mensaje = "Rol asignado";
                }
                else
                {
                    result.Mensaje = "Ha habido un problema en la asignación";
                }

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ResultadoCmd RemoverAsignarRol(AsignarRolUserCmd rol)
        {
            try
            {
                ResultadoCmd result = new ResultadoCmd();
                result.Proceso = "Remover Rol-Usuario";
                result.Exitoso = false;

                IdentityResult rest = this._ufw.UserIdentityRepository().RemoveAsigRoleUser(rol.Usuario, rol.IdRol);

                if (rest.Succeeded)
                {
                    result.Exitoso = true;
                    result.Mensaje = "Rol removido";
                }
                else
                {
                    result.Mensaje = "Ha habido un problema";
                }

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
