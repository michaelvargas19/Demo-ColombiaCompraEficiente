using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.Modelo.Queries
{
    public class ConfiguracionNewUserQuery
    {
        public bool Completado {get;set;}

        public List<TipoAutenticacionQuery> TiposAutenticacion {get;set;}        

        public List<TipoDocumentoQuery> TiposDocumento { get; set; }
        
        public List<RolQuery> Roles { get; set; }


    }
}
