using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogos.Infraestructura.Entities
{
    /// <summary>
    /// Entidad que referencia a la tabla en la base de datos
    /// que almacena los catálogos
    /// </summary>
    [Table("catCatalogos")]
    public class Catalogo 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCatalogo { get; set; }

        public string Nombre { get; set; }

        public string CodigoCatalogo { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
        
        public double Calificacion { get; set; }

        [ForeignKey("idCatalogo")]
        public IEnumerable<MultimediaCatalogo> Multimedia { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuarioCrea { get; set; }

        public Usuario Usuario { get; set; }
    }
}
