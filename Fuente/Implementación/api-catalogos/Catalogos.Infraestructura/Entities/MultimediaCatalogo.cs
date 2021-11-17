using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Catalogos.Infraestructura.Entities
{
    [Table("catMultimediaXCatalogo")]
    public class MultimediaCatalogo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMultimedia { get; set; }

        [ForeignKey("Catalogo")]
        public int idCatalogo { get; set; }

        public Catalogo Catalogo { get; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string url { get; set; }

        public TIPO_MULTIMEDIA Tipo { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuarioCrea { get; set; }

        public Usuario Usuario { get; set; }

    }




    
}
