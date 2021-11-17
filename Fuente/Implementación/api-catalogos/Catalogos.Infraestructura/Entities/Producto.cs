using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogos.Infraestructura.Entities
{
    [Table("catProductos")]
    public class Producto 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProducto { get; set; }


        [ForeignKey("Catalogo")]
        public int idCatalogo { get; set; }

        public Catalogo Catalogo { get; set; }


        public string Nombre { get; set; }

        public string SKU { get; set; }
        public string Descripcion { get; set; }

        public double Prioridad { get; set; }
                
        public double iva { get; set; }

        public double PesoKg { get; set; }

        public double ValorUnitario { get; set; }

        public int NivelInventario { get; set; }

        public int NivelAdvertencia { get; set; }

        [ForeignKey("idProducto")]
        public Descuento Descuento { get; set; }

        [ForeignKey("idProducto")]
        public IEnumerable<MultimediaProducto> Multimedia { get; set; }

        public string Marca { get; set; }

        public bool Estado { get; set; }

        public bool EnAlmacen { get; set; }

        
        public double Calificacion { get; set; }

        
        public DateTime fechaCreacion { get; set; }


        [ForeignKey("Usuario")]
        public int idUsuarioCrea { get; set; }

        public Usuario Usuario { get; set; }

    }
}
