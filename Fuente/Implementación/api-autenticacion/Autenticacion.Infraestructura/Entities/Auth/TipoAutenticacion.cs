using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    [Table("AspNetTiposAutenticacion")]
    public class TipoAutenticacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }

        [Required]
        public string Autenticacion { get; set; }

        [Required]
        public bool EsDirectorioActivo { get; set; }

        [Required]
        public bool IndHabilitado { get; set; }

        public int? IdAD { get; set; }

        [ForeignKey("IdTipoAuth")]
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
