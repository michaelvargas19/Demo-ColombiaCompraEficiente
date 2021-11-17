using System;
using System.Collections.Generic;

namespace Catalogos.Dominio.Modelo.Queries
{
    public class CatalogoQuery
    {
        public int Id { get; set; }

        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }

   
        public IEnumerable<MultimediaQuery> Multimedia { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaFin { get; set; }

        public double Calificacion { get; set; }


    }


}
