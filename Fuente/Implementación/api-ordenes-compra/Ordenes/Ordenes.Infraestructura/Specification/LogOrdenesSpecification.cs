using Ordenes.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Infraestructura.Specification
{
    public class LogOrdenesSpecification : BaseSpecification<_AuditoriaOrdenes>
    {
        public LogOrdenesSpecification(string referencia, string request) : base(l => l.Referencia.ToUpper() == referencia.ToUpper() && l.Request.ToUpper() == request.ToUpper())
        {
        }
    }
}
