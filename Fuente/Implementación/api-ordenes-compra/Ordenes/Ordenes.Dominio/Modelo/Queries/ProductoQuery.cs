using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo.Queries
{
    public class ProductoQuery
    {
        public int idProducto { get; set; }

        public string Nombre { get; set; }

        public string sku { get; set; }
        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }

        public int NivelInventario { get; set; }

        public int NivelAdvertencia { get; set; }

        public bool Estado { get; set; }
    }
}
