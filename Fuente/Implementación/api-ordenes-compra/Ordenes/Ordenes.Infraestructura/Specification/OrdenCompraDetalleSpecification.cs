using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.Specification
{
    public class OrdenCompraDetalleSpecification : BaseSpecification<OrdenCompraDetalle>
    {

        public OrdenCompraDetalleSpecification(int idUsuario) : base(o => o.idUsuarioCrea == idUsuario && o.indHabilitado == true )
        {
                
        }

        public OrdenCompraDetalleSpecification(int idOrden, int idProducto) : base(o => o.idOrden == idOrden && o.idProducto == idProducto && o.indHabilitado == true )
        {

        }

    }
}
