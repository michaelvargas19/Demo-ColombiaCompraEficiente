using Catalogos.Dominio.IServices.Queries;
using Catalogos.Dominio.IUnitOfWorks;
using Catalogos.Dominio.Modelo.Queries;
using Catalogos.Dominio.Util;
using Catalogos.Infraestructura.Entities;
using Catalogos.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogos.Dominio.Services.Queries
{
    /// <summary>
    /// Esta clase implementa los métodos firmados en la interfaz
    /// IProductosServiceQuery haciendo uso de del patrón Unit Of Work
    /// para acceder a los repositorios que que consultan información
    /// de los catálogos
    /// </summary>
    public class ProductosServiceQuery : IProductosServiceQuery
    {

        private readonly IUnitOfWork _ufw;
        private readonly IUtils _utils;

        public ProductosServiceQuery(IUnitOfWork ufw,
                                     IUtils utils)
        {
            this._ufw = ufw;
            this._utils = utils;
        }


        /// <summary>
        /// Permite consultar los productos de forma paginada
        /// </summary>
        /// <param name="skip">Número de registros a omitir</param>
        /// <param name="take">Número de registros a tomar</param>
        /// <returns>Productos encontrados</returns>
        public IEnumerable<ProductoQuery> verPaginacion(int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufw.RepositoryProductoQuery().Find(new ProductoSpecification(skip, take)).AsEnumerable();

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }


        /// <summary>
        /// Permite consultar el producto por código
        /// </summary>
        /// <param name="id">Identificador del código</param>
        /// <returns>Producto encontrado</returns>
        public ProductoQuery verProductoPorCodigo(int id)
        {
            ProductoQuery productoQ = null;

            try
            {
                Producto producto = _ufw.RepositoryProductoQuery().Find(new ProductoCodigoSpecification(id)).FirstOrDefault();

                if (producto != null)
                {
                    productoQ = this._utils.Convert_Producto_To_Query(producto);
                }
                else
                {
                    throw new Exception("No se ha encontrado el Producto");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return productoQ;
        }

        /// <summary>
        /// Permite consultar un producto por el código SKU
        /// </summary>
        /// <param name="sku">Código SKU del producto</param>
        /// <returns>Producto encontrado</returns>
        public ProductoQuery verProductoPorSKU(string sku)
        {
            ProductoQuery productoQ = null;

            try
            {
                Producto producto = _ufw.RepositoryProductoQuery().Find(new ProductoSKUSpecification(sku)).FirstOrDefault();

                if (producto != null)
                {
                    productoQ = this._utils.Convert_Producto_To_Query(producto);

                }
                else
                {
                    throw new Exception("No se ha encontrado el Producto");
                }

            }
            catch (Exception e)
            {
                _AuditoriaCatalogos log = new _AuditoriaCatalogos("INTEGRACIÓN REQUEST EXCEPTION", "", true, "", "", this.ToString(), e.Message, e.StackTrace, "", "");
            }

            return productoQ;
        }


        /// <summary>
        /// Permite consultar los productos de un catálogo deacuerdo a la calificación
        /// </summary>
        /// <param name="idCatalogo">Identificador del ca´tálogo</param>
        /// <param name="skip">Número de registros a omitir</param>
        /// <param name="take">Número de registros a tomar</param>
        /// <returns>Productos encontrados</returns>
        public IEnumerable<ProductoQuery> verRankingCatalogo(int idCatalogo, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }


            try
            {
                IEnumerable<ProductoQuery> productosQ = new List<ProductoQuery>();
                IEnumerable<Producto> productos = _ufw.RepositoryProductoQuery().Find(new ProductoSpecification(idCatalogo, true)).AsEnumerable();
                
                productos = productos.OrderByDescending(p => p.Calificacion).Skip(skip).Take(take);
                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

                return productosQ;

            }
            catch (Exception e)
            {
                _AuditoriaCatalogos log = new _AuditoriaCatalogos("INTEGRACIÓN REQUEST EXCEPTION", "", true, "", "", this.ToString(), e.Message, e.StackTrace, "", "");
                throw e;
            }

        }


        /// <summary>
        /// Permite consultar productos apartir de una cadena de texto
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Productos encontrados</returns>
        public IEnumerable<ProductoQuery> verRankingFullText(string texto, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufw.RepositoryProductoQuery().Find(new ProductoFullTextSpecification(texto, true)).AsEnumerable();
                productos = productos.OrderByDescending(p => p.Prioridad).Skip(skip).Take(take);
                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }


        /// <summary>
        /// Permite consultar productos apartir de una marca
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Productos encontrados</returns>
        public IEnumerable<ProductoQuery> verRankingMarca(string marca, int skip, int take)
        {
            if (take <= 0) { throw new Exception("La variable take debe tener valor positivo"); }

            IEnumerable<ProductoQuery> productosQ = null;

            try
            {
                IEnumerable<Producto> productos = _ufw.RepositoryProductoQuery().Find(new ProductoMarcaSpecification(marca, true)).AsEnumerable();

                productos = productos.OrderByDescending(p => p.Calificacion).Skip(skip).Take(take);

                productosQ = this._utils.ConvertList_Producto_To_Query(productos);

            }
            catch (Exception e)
            {
                throw e;
            }

            return productosQ;
        }
    }
}
