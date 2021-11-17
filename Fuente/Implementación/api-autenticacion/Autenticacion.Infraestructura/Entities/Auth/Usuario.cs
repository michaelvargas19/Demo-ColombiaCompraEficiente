using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    public class Usuario : IdentityUser<int>
    {
        [Required(ErrorMessage = "Primer nombre requerido")]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        [Required]
        public string SegundoApellido { get; set; }

        [ForeignKey("TipoDocumento")]
        public int? idTipoDocumento {get;set;}

        public TipoDocumento TipoDocumento { get; set; }

        public string? Identificacion { get; set; }

        [Required]
        [ForeignKey("TipoAutenticacion")]
        public int IdTipoAuth { get; set; }

        public TipoAutenticacion TipoAutenticacion { get; set; }
        public string Organizacion { get; set; }

        public string Cargo { get; set; }

        public string Descripcion { get; set; }

        public bool EsExterno { get; set; }

        public string IndicativoFijo { get; set; }

        public string Telefono { get; set; }

        public string IndicativoMovil { get; set; }

        public bool IndHabilitado { get; set; }


        [NotMapped]
        public ICollection<Rol> Roles { get; set; }


    }
}
