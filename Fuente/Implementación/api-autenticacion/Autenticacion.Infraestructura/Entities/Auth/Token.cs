using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.Entities.Auth
{
    [Table("AspNetTokensValidacion")]
    public class Token : IdentityUserToken<int>
    {
        [Required]
        [ForeignKey("Aplicacion")]
        [DataType(DataType.Text)]
        public string IdAplicacion { get; set; }

        public Aplicacion Aplicacion { get; }

        [Required]
        public int LongitudCodigo { get; set; }


        [Required]
        public int MinutosDeVida { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaExpiracion { get; set; }

        public DateTime? FechaValidacion { get; set; }

        [Required]
        public bool Validado { get; set; }

        [Required]
        public string FirmaJWT { get; set; }

        [Required]
        [Key]
        public string Transaccion { get; set; }

        public bool IndHabilitado { get; set; }

        public bool tokenVigente()
        {
            if (FechaExpiracion <= DateTime.Now)
            {
                return false;
            }
            return true;


        }
    }
}
