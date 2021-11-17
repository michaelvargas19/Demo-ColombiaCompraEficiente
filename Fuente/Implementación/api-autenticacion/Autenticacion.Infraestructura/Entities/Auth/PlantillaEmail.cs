using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    [Table("usuPlantillaEmail")]
    public class PlantillaEmail 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlantilla { get; set; }

        [Required]
        [ForeignKey("TipoPlantilla")]
        public int idTipoPlantilla { get; set; }

        public TipoPlantillaEmail TipoPlantilla { get; }



        [Required]
        [ForeignKey("Aplicacion")]
        [DataType(DataType.Text)]
        public string IdAplicacion { get; set; }

        public Aplicacion Aplicacion { get; }

        [Required]
        [MaxLength(250)]
        public string Nombre { get; set; }


        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        public string Plantilla { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("UsuarioCrea")]
        public int idUsuarioCrea { get; set; }

        public Usuario UsuarioCrea { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [ForeignKey("UsuarioModifica")]
        public int? idUsuarioModifica { get; set; }

        public Usuario UsuarioModifica { get; set; }

        [Required]
        public bool IndHabilitado { get; set; }

        
    }
}
