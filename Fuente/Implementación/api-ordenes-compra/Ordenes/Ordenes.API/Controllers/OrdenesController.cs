using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordenes.Dominio.IServices.Command;
using Ordenes.Dominio.IServices.Queries;
using Ordenes.Dominio.Modelo;
using Ordenes.Dominio.Modelo.Command;
using Ordenes.Dominio.Modelo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordenes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenesServiceQuery _ordenesServiceQuery;
        private readonly IOrdenesServiceCommand _ordenesServiceCommand;


        public OrdenesController(IOrdenesServiceQuery ordenesServiceQuery,IOrdenesServiceCommand ordenesServiceCommand)
        {
            this._ordenesServiceQuery = ordenesServiceQuery;
            this._ordenesServiceCommand = ordenesServiceCommand;
        }

        // GET: Ordenes/{#}
        /// <summary>Consultar carrito de compras</summary>
        /// <param name="idUsuario">Identificador del usuario</param>
        /// <returns>Catálogo correspondiente</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>


        [HttpGet("{idUsuario}")]
        public ActionResult<ResponseBase<OrdenCompraQuery>> verOrden(int idUsuario)
        {


            ResponseBase<OrdenCompraQuery> response = new ResponseBase<OrdenCompraQuery>();
            response.code = 500;

            try
            {
                response.data = _ordenesServiceQuery.ConsultarOrdenCompraPorUsuario(idUsuario);
                
                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar la operación";
                }
                else
                {
                    response.code = 200;
                    response.message = "Proceso Completado";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            return StatusCode(response.code, response);

        }





        // POST: Ordenes
        /// <summary>Agregar una orden de compra</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Rsultado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        public ActionResult<ResponseBase<OrdenCompraCmd>> AgregarOrden(RequestBase<OrdenCompraCmd> request)
        {
            ResponseBase<OrdenCompraCmd> response = new ResponseBase<OrdenCompraCmd>();
            response.code = 500;

            try
            {
                response.data = _ordenesServiceCommand.CrearOrdenCompra(request.data);
                

                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Orden creada";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            ////Auditoría

            //var jrq = JsonConvert.SerializeObject(request);
            //var jrp = JsonConvert.SerializeObject(response);
            //var host = "";
            //try
            //{
            //    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //    host = Dns.GetHostEntry(remoteIpAddress).HostName;
            //}
            //catch (Exception e) { }

            //try
            //{
            //    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

            //}
            //catch (Exception e) { };


            return StatusCode(response.code, response);
        }



        // POST: Ordenes
        /// <summary>Agregar producto a una orden</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Rsultado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpPost()]
        [Route("detalle")]
        public ActionResult<ResponseBase<OrdenCompraCmd>> AgregarOrdenDetalle(RequestBase<OrdenCompraDetalleCmd> request)
        {
            ResponseBase<OrdenCompraCmd> response = new ResponseBase<OrdenCompraCmd>();
            response.code = 500;

            try
            {
                response.data = _ordenesServiceCommand.AgregarDetalle(request.data);


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Producto agregado.";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            ////Auditoría

            //var jrq = JsonConvert.SerializeObject(request);
            //var jrp = JsonConvert.SerializeObject(response);
            //var host = "";
            //try
            //{
            //    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //    host = Dns.GetHostEntry(remoteIpAddress).HostName;
            //}
            //catch (Exception e) { }

            //try
            //{
            //    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

            //}
            //catch (Exception e) { };


            return StatusCode(response.code, response);
        }



        // POST: Ordenes
        /// <summary>Eliminar un producto de una orden</summary>
        /// <param name="request">Datos de la solicitud</param>
        /// <returns>Rsultado</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpDelete()]
        [Route("detalle")]
        public ActionResult<ResponseBase<OrdenCompraCmd>> EliminarOrdenDetalle(RequestBase<OrdenCompraDetalleCmd> request)
        {
            ResponseBase<OrdenCompraCmd> response = new ResponseBase<OrdenCompraCmd>();
            response.code = 500;

            try
            {
                response.data = _ordenesServiceCommand.RemoverDetalle(request.data);


                if (response.data == null)
                {
                    response.code = 202;
                    response.message = "No se ha podido completar el proceso";
                }
                else
                {
                    response.code = 200;
                    response.message = "Producto agregado.";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
            }

            response.date = DateTime.Now;

            ////Auditoría

            //var jrq = JsonConvert.SerializeObject(request);
            //var jrp = JsonConvert.SerializeObject(response);
            //var host = "";
            //try
            //{
            //    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //    host = Dns.GetHostEntry(remoteIpAddress).HostName;
            //}
            //catch (Exception e) { }

            //try
            //{
            //    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

            //}
            //catch (Exception e) { };


            return StatusCode(response.code, response);
        }


    }
}
