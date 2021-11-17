using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Command
{
    public class UsuarioCmd
    {

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        
        public string SegundoApellido { get; set; }

        public int? idTipoDocumento { get; set; }

        public string? Identificacion { get; set; }

        [Required]
        public int IdTipoAuth { get; set; }

        [Required]
        public string Organizacion { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public bool EsExterno { get; set; }


        public string IndicativoFijo { get; set; }

        public string Telefono { get; set; }

        public string IndicativoMovil { get; set; }

        public string Celular { get; set; }

        [Required]
        public string IdAplicacion { get; set; }

        public int IdRole { get; set; }


        public bool EsValido()
        {
            if ((EsExterno))
            {
                if ((Organizacion != null) && (Cargo != null) && (Descripcion != null))
                {
                    return true;
                }
                return false;
            }

            return true;
        }

    }
}
