using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordenes.Infraestructura.Entities
{
    [Table("ordPagos")]
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPago { get; set; }

        [ForeignKey("OrdenCompra")]
        public int idOrden { get; set; }

        public OrdenCompra OrdenCompra { get; }

        public DateTime fechaPago { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }

        public Usuario Usuario { get; set; }


        [Required]
        public bool indHabilitado { get; set; }
    }
}
