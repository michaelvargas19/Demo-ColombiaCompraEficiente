using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo.Queries
{
    public class OrdenCompraQuery
    {

        public int idOrden { get; set; }

        public DateTime fechaCreacion { get; set; }


        public int idEstado { get; set; }

        public OrdenCompraEstadoQuery Estado { get; set; }

        
        public IEnumerable<OrdenCompraDetalleQuery> Detalle { get; set; }

        public PagoQuery Pago { get; set; }

        public double valorTotal { get; set; }

        public bool indHabilitado { get; set; }


    }
}
