using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.Specification
{
    public class ProductoSpecification : BaseSpecification<Producto>
    {
        public ProductoSpecification() : base()
        {
        }

        public ProductoSpecification(int idProducto) : base(c => c.idProducto == idProducto && c.EnAlmacen == true && c.NivelInventario > 0 )
        {
        }

        public ProductoSpecification(bool estado) : base(c => c.Estado == estado)
        {
        }
        public ProductoSpecification(int skip, int take) : base(c => c.Estado == true && c.EnAlmacen == true)
        {
            ApplyPaging(skip, take);
        }


        
    }
}
