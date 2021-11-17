using System.Collections.Generic;

namespace Catalogos.Dominio.Modelo.Queries
{
    public class ProductoQuery
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string sku { get; set; }


        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }

        public int NivelInventario { get; set; }


        public DescuentoQuery Descuento { get; set; }

        public IEnumerable<MultimediaQuery> Multimedia { get; set; }

        public string Marca { get; set; }

        public bool EnAlmacen { get; set; }

        public double Calificacion { get; set; }

        public bool EstaEnAlmacen()
        {
            return (this.NivelInventario > 0) ? true : false;
        }



    }
}
