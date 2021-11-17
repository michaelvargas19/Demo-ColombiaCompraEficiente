using Ordenes.Dominio.Modelo.Command;
using Ordenes.Dominio.Modelo.Queries;
using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ordenes.Dominio.Util
{
    public class Utils : IUtils
    {
        public OrdenCompraQuery ConvertList_Orden_To_Query(OrdenCompra orden)
        {
            if (orden == null)
            {
                return null;
            }

            OrdenCompraQuery q = new OrdenCompraQuery();
            q.idOrden = orden.idOrden;
            q.fechaCreacion= orden.fechaCreacion;
            q.idEstado = orden.idEstado;
            q.valorTotal = orden.valorTotal;
            q.indHabilitado = orden.indHabilitado;

            if (orden.Estado != null) 
            {
                q.Estado = new OrdenCompraEstadoQuery();
                q.Estado.idEstado = orden.Estado.idEstado;
                q.Estado.Nombre = orden.Estado.Nombre;
            }

            if (orden.Detalle != null)
            {
                q.Detalle = ConvertList_Detalle_To_Query(orden.Detalle.Where(d=> d.indHabilitado==true));
                
            }


            return q;

        }


        public IEnumerable<OrdenCompraDetalleQuery> ConvertList_Detalle_To_Query(IEnumerable<OrdenCompraDetalle> detalles)
        {
            List<OrdenCompraDetalleQuery> ordenes = new List<OrdenCompraDetalleQuery>();

            foreach (OrdenCompraDetalle d in detalles)
            {
                ordenes.Add(this.Convert_Detalle_To_Query(d));
            }
            return ordenes;
        }

        public OrdenCompraDetalleQuery Convert_Detalle_To_Query(OrdenCompraDetalle detalle)
        {
            if(detalle == null)
            {
                return null;
            }


            OrdenCompraDetalleQuery q = new OrdenCompraDetalleQuery();
            q.idDetalle = detalle.idDetalle;
            q.idOrden = detalle.idOrden;
            q.idProducto = detalle.idProducto;
            q.cantidad = detalle.cantidad;
            q.fechaCreacion = detalle.fechaCreacion;
            q.idUsuarioCrea = detalle.idUsuarioCrea;

            if(detalle.Producto != null)
            {
                q.Producto = this.Convert_Producto_To_Query(detalle.Producto);
            }

            return q;
        }


        public ProductoQuery Convert_Producto_To_Query(Producto producto)
        {
            ProductoQuery p = new ProductoQuery();

            p.idProducto = producto.idProducto;
            p.Nombre = producto.Nombre;
            p.sku = producto.SKU;
            p.iva = producto.iva;
            p.PesoKg = producto.PesoKg;
            p.ValorUnitario = producto.ValorUnitario;
            p.NivelInventario = producto.NivelInventario;
            p.Estado = producto.Estado;

            return p;
        }




        public OrdenCompra Convert_Cmd_To_Orden(OrdenCompraCmd cmd)
        {
            OrdenCompra orden = new OrdenCompra();
            orden.fechaCreacion = DateTime.Now;
            orden.idUsuarioCrea = cmd.idUsuarioCrea;
            orden.idEstado = cmd.idEstado;
            orden.valorTotal = cmd.valorTotal;
            

            return orden;
        }


        public OrdenCompraCmd Convert_Orden_To_Cmd(OrdenCompra orden)
        {
            OrdenCompraCmd cmd = new OrdenCompraCmd();
            cmd.idOrden = orden.idOrden;
            cmd.fechaCreacion = orden.fechaCreacion;
            cmd.idUsuarioCrea = orden.idUsuarioCrea;
            cmd.idEstado = orden.idEstado;
            cmd.valorTotal = orden.valorTotal;

            if (orden.Estado != null)
            {
                cmd.Estado = new OrdenCompraEstadoCmd();
                cmd.Estado.idEstado = orden.Estado.idEstado;
                cmd.Estado.Nombre = orden.Estado.Nombre;
            }

            return cmd;
        }

    }
}
