using Ordenes.Dominio.Modelo.Command;
using Ordenes.Dominio.Modelo.Queries;
using Ordenes.Infraestructura.Entities;
using System.Collections.Generic;

namespace Ordenes.Dominio.Util
{
    public interface IUtils
    {

        //Ordenes de Compra
        OrdenCompraQuery ConvertList_Orden_To_Query(OrdenCompra orden);

        IEnumerable<OrdenCompraDetalleQuery> ConvertList_Detalle_To_Query(IEnumerable<OrdenCompraDetalle> detalles);

        OrdenCompraDetalleQuery Convert_Detalle_To_Query(OrdenCompraDetalle detalle);

        ProductoQuery Convert_Producto_To_Query(Producto producto);


        OrdenCompra Convert_Cmd_To_Orden(OrdenCompraCmd cmd);

        OrdenCompraCmd Convert_Orden_To_Cmd(OrdenCompra orden);

    }
}
