using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class CuentaCmd
    {

        [Required]
        [MaxLength(450)]
        public string AbreviacionAPP { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Contrasena { get; set; }

        [DataType(DataType.Password)]
        public string NuevaContrasena { get; set; }

        [Required]
        public string TokenJWT { get; set; }

        public bool datosParaLogin()
        {

            if ((AbreviacionAPP.Length > 0) && (Usuario.Length > 0) && (Contrasena.Length > 0))
            {
                return true;
            }

            return false;
        }

        public bool datosParaActualizacion()
        {
            if ((AbreviacionAPP.Length > 0) && (Usuario.Length > 0) &&
                (Contrasena.Length > 0) && (NuevaContrasena.Length > 0))
            {
                if (Contrasena.CompareTo(NuevaContrasena) == 0)
                {
                    return false;
                }

                return true;
            }

            return false;
        }


    }
}
