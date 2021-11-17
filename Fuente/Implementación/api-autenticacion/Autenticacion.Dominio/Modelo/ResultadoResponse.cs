using System;
using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Dominio.Modelo
{
    public class ResultadoResponse
    {
        [Required]
        public DateTime Fecha { set; get; }

        [Required]
        public string Proceso { get; set; }

        [Required]
        public bool Exitoso { get; set; }

        [Required]
        public string Mensaje { get; set; }

    }
}
