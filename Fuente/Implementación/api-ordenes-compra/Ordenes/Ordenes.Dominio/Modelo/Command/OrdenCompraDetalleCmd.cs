using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo.Command
{
    public class OrdenCompraDetalleCmd
    {
        public int idOrden { get; set; }

        public int idProducto { get; set; }

        public int cantidad { get; set; }

        public int idUsuario { get; set; }


    }
}
