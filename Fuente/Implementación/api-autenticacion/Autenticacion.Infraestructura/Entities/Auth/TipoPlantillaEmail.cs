using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    [Table("usuTipoPlantillaEmail")]
    public class TipoPlantillaEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }



        [DataType(DataType.DateTime)]
        public DateTime? FechaModificacion { get; set; }


        [Required]
        public bool IndHabilitado { get; set; }



    }

}
