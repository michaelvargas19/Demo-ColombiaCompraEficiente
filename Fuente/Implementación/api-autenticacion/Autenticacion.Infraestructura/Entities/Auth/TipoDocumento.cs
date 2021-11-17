using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    [Table("usuTipoDocumento")]
    public class TipoDocumento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipo { get; set; }

        [Required]
        [MaxLength(3)]
        public string Codigo { get; set; }


        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }


        [Required]
        public int Orden { get; set; }


        [Required]
        public bool indHabilitado { get; set; }


    }
}
