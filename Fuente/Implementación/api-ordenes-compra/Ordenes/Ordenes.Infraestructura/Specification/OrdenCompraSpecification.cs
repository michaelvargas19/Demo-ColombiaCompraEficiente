using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.Specification
{
    public class OrdenCompraSpecification : BaseSpecification<OrdenCompra>
    {

        public OrdenCompraSpecification(int idUsuario) : base(o => o.idUsuarioCrea == idUsuario && o.indHabilitado == true )
        {
            AddInclude(o=> o.Detalle);
            AddInclude(o=> o.Estado);
            AddInclude(o=> o.Pago);
        }

    }
}
