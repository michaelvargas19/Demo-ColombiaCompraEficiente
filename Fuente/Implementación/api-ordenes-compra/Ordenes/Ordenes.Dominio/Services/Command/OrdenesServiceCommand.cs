using Ordenes.Dominio.IServices.Command;
using Ordenes.Dominio.IUnitOfWorks;
using Ordenes.Dominio.Modelo.Command;
using Ordenes.Dominio.Util;
using Ordenes.Infraestructura.Entities;
using Ordenes.Infraestructura.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ordenes.Dominio.Services.Command
{
    public class OrdenesServiceCommand : IOrdenesServiceCommand
    {

        private readonly IUnitOfWork _ufw;
        private readonly IUtils _utils;

        public OrdenesServiceCommand(IUnitOfWork ufw,
                                     IUtils utils)
        {
            this._ufw = ufw;
            this._utils = utils;
        }


        /// <summary>
        /// Permite crear una orden de compra para un usuario
        /// </summary>
        /// <param name="cmd">Orden de compra </param>
        /// <returns>Orden de compra actualizada</returns>
        public OrdenCompraCmd CrearOrdenCompra(OrdenCompraCmd cmd)
        {
            
            try
            {
                OrdenCompra ordenAux = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(cmd.idUsuarioCrea)).FirstOrDefault();

                if(ordenAux != null)
                {
                    cmd = _utils.Convert_Orden_To_Cmd(ordenAux);

                    return cmd;
                }

                OrdenCompra orden = _utils.Convert_Cmd_To_Orden(cmd);
                orden.idEstado = 1;
                orden.indHabilitado = true;
                orden.fechaCreacion = DateTime.Now;

                //_ufw.BeginTransaction();
                _ufw.RepositoryOrdenCompraCommand().Add(orden);

                _ufw.SaveChanges();
                //_ufw.Commit();

                orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(cmd.idUsuarioCrea)).FirstOrDefault();

                if (orden == null)
                {
                    throw new Exception("No se ha podido crear el carrito de compras");
                }

                cmd = _utils.Convert_Orden_To_Cmd(orden);

                return cmd;

            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// Permite remover la orden de compra
        /// </summary>
        /// <param name="orden"></param>
        /// <returns>Orden de compra removida</returns>
        public OrdenCompraCmd RemoverOrdenCompra(OrdenCompraCmd orden)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permite actualizar la orden de compra de un usuario
        /// </summary>
        /// <param name="orden"></param>
        /// <returns>Orden de compra actualizada</returns>
        public OrdenCompraCmd ActualizarOrdenCompra(OrdenCompraCmd orden)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Permite agregar un item a una orden de compra
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns>Orden de compra actualizada</returns>
        public OrdenCompraCmd AgregarDetalle(OrdenCompraDetalleCmd detalle)
        {
            try
            {
                OrdenCompra orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(detalle.idUsuario)).FirstOrDefault();

                if ( (orden == null) || (orden.idOrden != detalle.idOrden))
                {
                    throw new Exception("La solicitud es inválida.");
                }

                Producto producto = _ufw.RepositoryProductoQuery().Find(new ProductoSpecification(detalle.idProducto)).FirstOrDefault();

                if ( (producto==null) )
                {
                    throw new Exception("La solicitud es inválida.");
                }

                if ((producto.NivelInventario < detalle.cantidad))
                {
                    throw new Exception("No hay disponibilidad del número de unidades solicitadas.");
                }

                OrdenCompraDetalle ordenDetalle = new OrdenCompraDetalle();

                ordenDetalle.idOrden = orden.idOrden;
                ordenDetalle.idProducto = producto.idProducto;
                ordenDetalle.cantidad = detalle.cantidad;
                ordenDetalle.fechaCreacion = DateTime.Now;
                ordenDetalle.idUsuarioCrea = detalle.idUsuario;
                ordenDetalle.indHabilitado = true;



                //_ufw.BeginTransaction();
                _ufw.RepositoryOrdenCompraDetalleCommand().Add(ordenDetalle);

                _ufw.SaveChanges();
                //_ufw.Commit();

                orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(detalle.idUsuario)).FirstOrDefault();

                if (orden == null)
                {
                    throw new Exception("No se ha podido crear el carrito de compras");
                }

                OrdenCompraCmd ordenCMD = _utils.Convert_Orden_To_Cmd(orden);

                return ordenCMD;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Permite actualizar el item de una orden de compra
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns>Orden de compra actualizada</returns>
        public OrdenCompraCmd ActualizarDetalle(OrdenCompraDetalleCmd detalle)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Permite remover item de una orden de compra
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns>Orden de compra actualizada</returns>
        public OrdenCompraCmd RemoverDetalle(OrdenCompraDetalleCmd detalle)
        {
            try
            {
                OrdenCompra orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(detalle.idUsuario)).FirstOrDefault();

                if ((orden == null) || (orden.idOrden != detalle.idOrden))
                {
                    throw new Exception("La solicitud es inválida.");
                }

                Producto producto = _ufw.RepositoryProductoQuery().Find(new ProductoSpecification(detalle.idProducto)).FirstOrDefault();

                if ((producto == null))
                {
                    throw new Exception("La solicitud es inválida.");
                }

                if ( (orden.Detalle.Any(p=> p.idProducto == producto.idProducto && producto.Estado == true) ) )
                {

                    OrdenCompraDetalle ordenDetalle = _ufw.RepositoryOrdenCompraDetalleQuery().Find(new OrdenCompraDetalleSpecification(orden.idOrden, producto.idProducto)).FirstOrDefault();

                    if ((ordenDetalle == null))
                    {
                        throw new Exception("La solicitud es inválida.");
                    }

                    ordenDetalle.indHabilitado = false;

                    //_ufw.BeginTransaction();
                    _ufw.RepositoryOrdenCompraDetalleCommand().Update(ordenDetalle);

                    _ufw.SaveChanges();
                    //_ufw.Commit();

                }
                else
                {
                    throw new Exception("No hay disponibilidad del número de unidades solicitadas.");
                }

                               

                orden = _ufw.RepositoryOrdenCompraQuery().Find(new OrdenCompraSpecification(detalle.idUsuario)).FirstOrDefault();

                if (orden == null)
                {
                    throw new Exception("No se ha podido crear el carrito de compras");
                }

                OrdenCompraCmd ordenCMD = _utils.Convert_Orden_To_Cmd(orden);

                return ordenCMD;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
