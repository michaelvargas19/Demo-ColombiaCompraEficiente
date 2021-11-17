using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class AlgoritmoSpecification : BaseSpecification<AlgoritmoDeSeguridad>
    {
        public AlgoritmoSpecification() : base()
        {
            
        }

        public AlgoritmoSpecification(string Algoritmo) : base(x => x.Algoritmo == Algoritmo)
        {
        }
    }
}
