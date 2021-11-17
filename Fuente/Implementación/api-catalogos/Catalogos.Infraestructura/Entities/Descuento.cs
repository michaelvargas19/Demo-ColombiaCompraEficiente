using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Catalogos.Infraestructura.Entities
{
    [Table("catDescuentosXProducto")]
    public class Descuento
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDescuento { get; set; }


        [ForeignKey("Producto")]
        public int idProducto { get; set; }

        public Producto Producto { get; set; }


        public string Nombre { get; set; }

        public double Porcentaje { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

    }
}
