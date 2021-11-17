using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class CodigoCmd
    {

        [Required]
        [MaxLength(450)]
        public string AbreviacionApp { get; set; }

        [Required]
        public string TokenJWT { get; set; }

        [Required]
        public bool CodigoGenerado { get; set; }

        [Required]
        public int LongitudCodigo { get; set; }

        [Required]
        public int MinutosVida { get; set; }

        [Required]
        public DateTime FechaGeneracion { get; set; }

        [Required]
        public DateTime FechaExpiracion { get; set; }

        [Required]
        public string Transaccion { get; set; }

        [Required]
        public string Mensaje { get; set; }

    }
}
