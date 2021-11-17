using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class PlantillaEmailSpecification : BaseSpecification<PlantillaEmail>
    {
        public PlantillaEmailSpecification() : base()
        {
        }

        public PlantillaEmailSpecification(int idTipoPlanilla, string idAplicacion) : base(p => p.idTipoPlantilla == idTipoPlanilla && p.IdAplicacion == idAplicacion && p.IndHabilitado == true)
        {

        }


    }
}
