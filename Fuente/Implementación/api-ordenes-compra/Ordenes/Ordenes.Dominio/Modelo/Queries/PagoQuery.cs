using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo.Queries
{
    public class PagoQuery
    {

        public long idPago { get; set; }

        public int idOrden { get; set; }

        public DateTime fechaPago { get; set; }
        
        public int idUsuario { get; set; }


    }
}
