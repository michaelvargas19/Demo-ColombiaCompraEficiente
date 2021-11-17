using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.Modelo;
using Catalogos.Dominio.Modelo.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Catalogos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosServiceQuery _productosServiceQuery;

        public ProductosController(IProductosServiceQuery productosServiceQuery)
        {
            this._productosServiceQuery = productosServiceQuery;
        }



        // GET: Productos/paginacion?skip={#}&take={#}
        /// <summary>Consultar productos paginandos</summary>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos correspondientes</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>


        [HttpGet()]
        public ActionResult<IEnumerable<ProductoQuery>> verPaginacion(int skip, int take)
        {


            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verPaginacion(skip, take);
                //response.data = getProductosEjemplo();

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


        // GET: Productos/ranking/catalogo?codigo={''}&skip={#}&take={#}
        /// <summary>Consultar ranking de productos</summary>
        /// <param name="codigo">Código del catálogo</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos correspondientes</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/catalogo")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingCatalogo(int idCatalogo, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingCatalogo(idCatalogo, skip, take);
                //response.data = getProductosEjemplo();

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


        // GET: Productos/ranking/fulltext?texto={''}&skip={#}&take={#}
        /// <summary>Consultar productos por texto</summary>
        /// <param name="texto">Texto de búsqueda</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos encontrados</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/fulltext")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingFullText(string texto, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingFullText(texto, skip, take);
                //response.data = getProductosEjemplo();

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



        // GET: Productos/ranking/marca?marca={''}&skip={#}&take={#}
        /// <summary>Consultar productos por marca</summary>
        /// <param name="marca">Marca de búsqueda</param>
        /// <param name="skip">Cantidad de datos a omitir</param>
        /// <param name="take">Cantidad de datos a tomar</param>
        /// <returns>Productos encontrados</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("ranking/marca")]
        public ActionResult<IEnumerable<ProductoQuery>> verRankingMarca(string marca, int skip, int take)
        {

            ResponseBase<IEnumerable<ProductoQuery>> response = new ResponseBase<IEnumerable<ProductoQuery>>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verRankingMarca(marca, skip, take);
                //response.data = getProductosEjemplo();

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



        // GET: Productos/{id}
        /// <summary>Consultar producto por Id</summary>
        /// <param name="id">Código del producto</param>
        /// <returns>Detalles del Producto</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="202">Solicitud no procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("codigo/{codigo}")]
        public ActionResult<ProductoQuery> VerProductoPorId(int id)
        {
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verProductoPorCodigo(id);
                //response.data = getProductosEjemplo().FirstOrDefault();

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



        // GET: Productos/sku/{sku}
        /// <summary>Consultar producto por Id</summary>
        /// <param name="sku">Código SKU del producto</param>
        /// <returns>Detalles del Producto</returns>
        /// <response code="200">Solicitud procesada</response>
        /// <response code="400">Problemas con la solicitud</response>
        /// <response code="401">Falta de permisos</response>
        /// <response code="500">Error Interno</response>

        [HttpGet("sku/{sku}")]
        public ActionResult<ProductoQuery> VerProductoPorSKU(string sku)
        {
            ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
            response.code = 500;

            try
            {
                response.data = _productosServiceQuery.verProductoPorSKU(sku);
                //response.data = getProductosEjemplo().FirstOrDefault();

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



        //// POST: Productos
        ///// <summary>Agregar un producto</summary>
        ///// <param name="request">Datos de la solicitud</param>
        ///// <returns>Producto Agregado</returns>
        ///// <response code="200">Solicitud procesada</response>
        ///// <response code="400">Problemas con la solicitud</response>
        ///// <response code="401">Falta de permisos</response>
        ///// <response code="500">Error Interno</response>

        //[HttpPost()]
        //public ActionResult<ProductoQuery> AgregarProducto(RequestBase<ProductoCmd> request)
        //{
        //    ResponseBase<ProductoQuery> response = new ResponseBase<ProductoQuery>();
        //    response.code = 500;

        //    try
        //    {
        //        response.data = _productosServiceCmd.AgregarProducto(request.data);
        //        //response.data = getCatalogosEjemplo().FirstOrDefault();


        //        if (response.data == null)
        //        {
        //            response.code = 202;
        //            response.message = "No se ha podido completar el proceso";
        //        }
        //        else
        //        {
        //            response.code = 200;
        //            response.message = "Catálogo creado";
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        response.message = e.Message;
        //    }

        //    response.date = DateTime.Now;

        //    ////Auditoría

        //    //var jrq = JsonConvert.SerializeObject(request);
        //    //var jrp = JsonConvert.SerializeObject(response);
        //    //var host = "";
        //    //try
        //    //{
        //    //    var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        //    //    host = Dns.GetHostEntry(remoteIpAddress).HostName;
        //    //}
        //    //catch (Exception e) { }

        //    //try
        //    //{
        //    //    this._logServiceCmd.AgregarLog(new LogCmd { Tipo = "Request-Response", Usuario = request.usuario, Request = jrq, Response = jrp, Aplicacion = "Autenticación.API", Metodo = MethodInfo.GetCurrentMethod().Name, Entidad = this.ToString(), EsExcepcion = false, Mensaje = "", Parametros = "" });

        //    //}
        //    //catch (Exception e) { };


        //    return StatusCode(response.code, response);
        //}






    }
}
