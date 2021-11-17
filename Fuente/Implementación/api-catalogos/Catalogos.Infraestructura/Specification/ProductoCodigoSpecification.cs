using Catalogos.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogos.Infraestructura.Specification
{
    public class ProductoCodigoSpecification : BaseSpecification<Producto>
    {

        public ProductoCodigoSpecification(int id) : base(p=> p.idProducto == id )
        {
        }

        public ProductoCodigoSpecification(int id, bool estado) : base(p => p.idProducto == id &&  p.Estado == estado)
        {
        }

    }
}
