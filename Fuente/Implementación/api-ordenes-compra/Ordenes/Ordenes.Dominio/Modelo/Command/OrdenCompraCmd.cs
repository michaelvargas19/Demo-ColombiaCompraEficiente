using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordenes.Dominio.Modelo.Command
{
    public class OrdenCompraCmd
    {
        public int idOrden { get; set; }

        public DateTime fechaCreacion { get; set; }

        [Required]
        [Range(1,Int32.MaxValue)]
        public int idUsuarioCrea { get; set; }


        public int idEstado { get; set; }

        public OrdenCompraEstadoCmd Estado { get; set; }

        public double valorTotal { get; set; }


    }
}
