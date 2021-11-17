using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Queries
{
    public class TipoAutenticacionQuery
    {
        public int IdTipo { get; set; }

        public string Autenticacion { get; set; }

        public bool EsDirectorioActivo { get; set; }



    }
}
