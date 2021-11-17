using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo.Queries
{
    public class OrdenCompraDetalleQuery
    {

        public long idDetalle { get; set; }

        public int idOrden { get; set; }

        public int idProducto { get; set; }

        public ProductoQuery Producto { get; set; }

        public int cantidad { get; set; }

        public DateTime fechaCreacion { get; set; }

        public int idUsuarioCrea { get; set; }


    }
}
