using Autenticacion.Dominio.IUnitOfWorks;
using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;
using Autenticacion.Dominio.Services.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IUnitOfWork _ufw;

        public CuentaController(IUnitOfWork ufwAplicacion)
        {

            this._ufw = ufwAplicacion;

        }

        // GET: Cuenta/recuperar
        /// <summary>Obtener token JWT y enviar código al usuario</summary>
        /// <param name="source">Identificador de la aplicación</param>
        /// <param name="root">Usuario solicitante</param>
        /// <remarks>
        /// `Descripción:`  
        /// ```
        ///     Inicia el proceso de recuperación de una cuenta, con los siguientes pasos:
        ///         * Genera un código de 6 digitos
        ///         * Envía el código al email del usuario
        ///         * Genera un token JWT único para para el proceso
        ///         * Retorna el token JWT junto a los detalles del código generado:
        ///             - Longitud
        ///             - Fecha de generación y expiración
        ///  ```
        ///  </remarks>
        /// <returns>Detalles del código generado y token para cosumir el servicio POST 'api/cuenta/recuperar'</returns>
        /// <response code="201">Código generado</response>
        /// <response code="202">Generación no válida</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>

        [HttpGet]
        [Route("recuperar")]
        public CodigoCmd GetRecuperar([FromQuery] string source, string root)
        {
            var host = "";

            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }


            if ((source.Length == 0) || (root.Length == 0))
            {
                Response.StatusCode = 400;
                return null;
            }

            CodigoCmd response = new CodigoCmd();
            response.CodigoGenerado = false;
            response.FechaGeneracion = DateTime.Now;
            response.FechaExpiracion = DateTime.Now;
            try
            {

                response = this._ufw.CuentaServiceCmd().codigoRecuperarCuenta(source, root, host);
                Response.StatusCode = 201;
            }
            catch (Exception e)
            {
                response.Mensaje = e.Message;
                Response.StatusCode = 202;
            }


            return response;
        }


        // POST: Cuenta/recuperar
        /// <summary>Recuperar cuenta con código de confirmación</summary>
        /// <param name="request">Datos para recuperar la cuenta</param>
        /// <remarks>
        /// `Descripción:`  
        /// ```
        ///     Continua con el proceso de recuperación de una cuenta, con los siguientes pasos:
        ///         * Recibe la informaciónn del usuario, token JWT y código de verificación
        ///         * Valída que el token JWT sea el mismo que se expidio para la solicitud
        ///         * Valída el código de verificación
        ///         * Cambia la contraseña del usuario según los datos de la solicitud
        ///         * Retorna el resultado de la operación
        ///  ```
        ///  </remarks>
        /// <returns>Token JWT con datos de la sesión</returns>
        /// <response code="201">SOlicitud procesada</response>
        /// <response code="202">Proceso Inválido</response>
        /// <response code="203">Autorización inválida</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        [HttpPost]
        [Route("recuperar")]

        public ResultadoResponse PostRecuperar([FromBody] RecuperarCuentaCmd request)
        {
            var host = "";

            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }


            ResultadoResponse response = new ResultadoResponse();
            response.Proceso = "Recuperar Cuenta";
            response.Exitoso = false;

            TokenJWT token = new TokenJWT();
            token.IdAplicacion = request.AbreviacionApp;
            token.Token = request.TokenJWT;

            if ( ! this._ufw.SesionesServiceCmd().validarTokenJWT(token).TokenValido  )
            {
                Response.StatusCode = 203;
                response.Mensaje = "Autorización inválida.";
            }

            try
            {

                response = this._ufw.CuentaServiceCmd().PostRecuperar(request, host);

                if (response.Exitoso)
                {
                    Response.StatusCode = 201;
                }
                else
                {
                    Response.StatusCode = 202;
                }
                

            }
            catch (Exception e)
            {
                response.Mensaje = "Comuníquese con el administrador.";
                Response.StatusCode = 202;
            }

            return response;

        }



        // PUT: Cuenta/
        /// <summary>Actualizar contraseña de un usuario</summary>
        /// <param name="cuenta">Debe incluir usuario, contraseña, confirmación y Token JWT válido</param>
        /// <remarks>
        /// `Descripción:`  
        /// ```
        ///     Este servicio permite actualizar la contraseña de un ususario, apartir de un token JWT de sesión válido y datos de entrada.
        ///     Los pasos que se realizan son:
        ///         * Recibe la informaciónn del usuario y token JWT de la sesión
        ///         * Valída que el token JWT está vigente
        ///         * Valída la informaión del usuario y aplicación
        ///         * Actualiza la contraseña del usuario según los datos de la solicitud
        ///         * Retorna el resultado de la operación
        ///  ```
        ///  </remarks>
        /// <returns>Nuevo Token JWT</returns>
        /// <response code="201">Contraseña actualizada</response>
        /// <response code="202">Los datos de la solicitud son incorrectos</response>
        /// <response code="203">Token JWT inválido</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="404">Los datos de aplicación o usuario son inválidos</response>
        [HttpPut]
        public ResultadoResponse Put([FromBody] CuentaCmd cuenta)
        {
            var host = "";

            try
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                host = Dns.GetHostEntry(remoteIpAddress).HostName;
            }
            catch (Exception e) { }

            ResultadoResponse resultado = new ResultadoResponse();
            resultado.Proceso = "Actualizar Contraseña";
            resultado.Exitoso = false;

            if (!cuenta. datosParaActualizacion())
            {
                Response.StatusCode = 202;
                resultado.Mensaje = "La solicitud es incorrecta";
                return resultado;
            }

            TokenJWT token = new TokenJWT();
            token.IdAplicacion = cuenta.AbreviacionAPP;
            token.Token = cuenta.TokenJWT;

            if ( ! this._ufw.SesionesServiceCmd().validarTokenJWT(token).TokenValido )
            {
                Response.StatusCode = 203;
                resultado.Mensaje = "Token JWT inválido";
                return resultado;
            }

            resultado = this._ufw.CuentaServiceCmd().PutContrasena(cuenta, host);
            
            if (resultado.Exitoso)
            {
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 202;
            }

            if (!resultado.Exitoso)
            {
                Response.StatusCode = 202;
                resultado.Mensaje = "La aplicación o los datos del usuario son inválidos";
            }

            return resultado;
        }



    }
}
