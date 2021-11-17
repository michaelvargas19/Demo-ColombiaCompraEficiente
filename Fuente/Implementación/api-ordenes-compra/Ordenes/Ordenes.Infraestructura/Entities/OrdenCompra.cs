using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordenes.Infraestructura.Entities
{
    [Table("ordOrdenCompra")]
    public class OrdenCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idOrden { get; set; }

        public DateTime fechaCreacion { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuarioCrea { get; set; }

        public Usuario Usuario { get; set; }

        [ForeignKey("Estado")]
        public int idEstado { get; set; }

        public OrdenCompraEstado Estado { get; set; }

        [ForeignKey("idOrden")]
        public IEnumerable<OrdenCompraDetalle> Detalle {get;set;}

        [ForeignKey("idPago")]
        public Pago Pago { get; set; }

        public double valorTotal { get; set; }

        [Required]
        public bool indHabilitado { get; set; }

    }
}
