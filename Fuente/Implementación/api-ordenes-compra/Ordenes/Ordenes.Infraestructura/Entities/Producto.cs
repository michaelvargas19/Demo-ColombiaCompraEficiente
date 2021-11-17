using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordenes.Infraestructura.Entities
{
    [Table("catProductos")]
    public class Producto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProducto { get; set; }


        public string Nombre { get; set; }

        public string SKU { get; set; }
        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }

        public int NivelInventario { get; set; }

        public int NivelAdvertencia { get; set; }

        [ForeignKey("idProducto")]
        public Descuento Descuento { get; set; }

        public bool Estado { get; set; }

        public bool EnAlmacen { get; set; }


    }
}
