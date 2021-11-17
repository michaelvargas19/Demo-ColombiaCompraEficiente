using Ordenes.Dominio.Modelo.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.IServices.Command
{
    public interface IOrdenesServiceCommand
    {
        OrdenCompraCmd CrearOrdenCompra(OrdenCompraCmd orden);

        OrdenCompraCmd ActualizarOrdenCompra(OrdenCompraCmd orden);

        OrdenCompraCmd RemoverOrdenCompra(OrdenCompraCmd orden);

        OrdenCompraCmd AgregarDetalle(OrdenCompraDetalleCmd detalle);

        OrdenCompraCmd ActualizarDetalle(OrdenCompraDetalleCmd detalle);
        
        OrdenCompraCmd RemoverDetalle(OrdenCompraDetalleCmd detalle);

    }
}
