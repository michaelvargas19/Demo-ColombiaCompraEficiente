using Autenticacion.Dominio.IServices.Command;
using Autenticacion.Dominio.IServices.Queries;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Modelo.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServiceCmd _usuariosService;
        private readonly IAplicacionesServiceQuery _aplicacionesServiceQuery;
        private readonly ILogServiceCmd _logServiceCmd;

        public UsuariosController(IUsuariosServiceCmd usuariosService,
                                  IAplicacionesServiceQuery aplicacionesServiceQuery,
                                      ILogServiceCmd logServiceCmd)
        {
            this._usuariosService = usuariosService;
            this._logServiceCmd = logServiceCmd;
            this._aplicacionesServiceQuery = aplicacionesServiceQuery;

        }


        // POST: Configuración        
        /// <summary>Crear un nuevo usuario</summary>
        /// <param name="request">Datos de configuración</param>
        /// <returns>Configuración</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Proceso no Completado</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        public ActionResult<ResponseBase<UsuarioQuery>> CrearUsuario(RequestBase<UsuarioCmd> request)
        {
            var host = "";

            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }


            ResponseBase<UsuarioQuery> response = new ResponseBase<UsuarioQuery>();
            response.code = 500;

            try
            {
                response.data = _usuariosService.registrarUsuario(request.data, host);


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Usuario creado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            //Auditoría

            var jrq = JsonConvert.SerializeObject(request);
            var jrp = JsonConvert.SerializeObject(response);
            
            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            try
            {
                this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

            }
            catch (Exception e) { };


            return StatusCode(response.code, response);
        }




        // POST: Ver Configuracion - Nuevo Usuario
        /// <summary>Configuración para crear un nuevo usuario</summary>
        /// <param name="token">Credenciales de acceso genéricas</param>
        /// <returns>usuario creado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Proceso no Completado</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        [Route("configuracion")]
        public ActionResult<ResponseBase<ConfiguracionNewUserQuery>>  GetConfiguracionNuevoUsuario(TokenJWT token)
        {
            ResponseBase<ConfiguracionNewUserQuery> response = new ResponseBase<ConfiguracionNewUserQuery>();
            response.code = 500;

            try
            {
                response.data = _aplicacionesServiceQuery.GetConfiguracionNuevoUsuario(token);


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Usuario creado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;

                //Auditoría

                
                var jrp = JsonConvert.SerializeObject(response);
                var host = "";
                try
                {
                    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    host = Dns.GetHostEntry(remoteIpAddress).HostName;
                }
                catch (Exception exe) { }

                try
                {
                    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = "", Request = "", Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

                }
                catch (Exception ex) { };


            }

            response.date = DateTime.Now;

            


            return StatusCode(response.code, response);
        }



        #region Roles

        

                // POST: Roles/asignar/usuario
                /// <summary>Asignar un rol a un usuario</summary>
                /// <param name="request">Datos de configuración</param>
                /// <returns>Resultado del proceso</returns>
                /// <response code="200">Solicitud procesada</response>
                /// <response code="202">Proceso no Completado</response>
                /// <response code="400">Problemas con la solicitud</response>
                /// <response code="401">Falta de permisos</response>
                /// <response code="500">Error Interno</response>

                [HttpPost("asignar/usuario")]
                public ActionResult<ResponseBase<ResultadoCmd>> AsignarRol(RequestBase<AsignarRolUserCmd> request)
                {
                    ResponseBase<ResultadoCmd> response = new ResponseBase<ResultadoCmd>();
                    response.code = 500;

                    try
                    {
                        response.data = _usuariosService.asignarRol(request.data);


                        if (response.data == null)
                        {
                            response.code = 202;
                            response.message = "No se ha podido completar el proceso";
                        }
                        else
                        {
                            response.code = 200;
                            response.message = "Rol asignado";
                        }

                    }
                    catch (Exception e)
                    {
                        response.message = e.Message;
                    }

                    response.date = DateTime.Now;

                    //Auditoría

                    var jrq = JsonConvert.SerializeObject(request);
                    var jrp = JsonConvert.SerializeObject(response);
                    var host = "";
                    try
                    {
                        var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        host = Dns.GetHostEntry(remoteIpAddress).HostName;
                    }
                    catch (Exception e) { }

                    try
                    {
                        this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

                    }
                    catch (Exception e) { };


                    return StatusCode(response.code, response);
                }



                // DELETE: Roles/asignar/usuario
                /// <summary>Asignar un rol a un usuario</summary>
                /// <param name="request">Datos de configuración</param>
                /// <returns>Resultado del proceso</returns>
                /// <response code="200">Solicitud procesada</response>
                /// <response code="202">Proceso no Completado</response>
                /// <response code="400">Problemas con la solicitud</response>
                /// <response code="401">Falta de permisos</response>
                /// <response code="500">Error Interno</response>

                [HttpDelete("asignar/usuario")]
                public ActionResult<ResponseBase<ResultadoCmd>> RemoverAsignarRol(RequestBase<AsignarRolUserCmd> request)
                {
                    ResponseBase<ResultadoCmd> response = new ResponseBase<ResultadoCmd>();
                    response.code = 500;

                    try
                    {
                        response.data = _usuariosService.RemoverAsignarRol(request.data);


                        if (response.data == null)
                        {
                            response.code = 202;
                            response.message = "No se ha podido completar el proceso";
                        }
                        else
                        {
                            response.code = 200;
                            response.message = "Rol removido";
                        }

                    }
                    catch (Exception e)
                    {
                        response.message = e.Message;
                    }

                    response.date = DateTime.Now;

                    //Auditoría

                    var jrq = JsonConvert.SerializeObject(request);
                    var jrp = JsonConvert.SerializeObject(response);
                    var host = "";
                    try
                    {
                        var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        host = Dns.GetHostEntry(remoteIpAddress).HostName;
                    }
                    catch (Exception e) { }

                    try
                    {
                        this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

                    }
                    catch (Exception e) { };


                    return StatusCode(response.code, response);
                }

        #endregion


    }
}
