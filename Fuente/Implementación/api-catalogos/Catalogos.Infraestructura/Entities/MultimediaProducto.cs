using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Catalogos.Infraestructura.Entities
{
    [Table("catMultimediaXProducto")]
    public class MultimediaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMultimedia { get; set; }

        [ForeignKey("Producto")]
        public int idProducto { get; set; }

        public Producto Producto { get; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string url { get; set; }

        public TIPO_MULTIMEDIA Tipo { get; set; }


    }




    
}
