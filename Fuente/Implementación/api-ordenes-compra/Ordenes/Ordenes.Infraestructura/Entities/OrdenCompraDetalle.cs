using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordenes.Infraestructura.Entities
{
    [Table("ordOrdenCompraDetalle")]
    public class OrdenCompraDetalle
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idDetalle { get; set; }

        [ForeignKey("OrdenCompra")]
        public int idOrden { get; set; }

        public OrdenCompra OrdenCompra { get;  }


        [ForeignKey("Producto")]
        public int idProducto { get; set; }

        public Producto Producto { get; set; }

        public int cantidad { get; set; }

        public DateTime fechaCreacion { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuarioCrea { get; set; }

        public Usuario Usuario { get; set; }


        [Required]
        public bool indHabilitado { get; set; }

    }
}
