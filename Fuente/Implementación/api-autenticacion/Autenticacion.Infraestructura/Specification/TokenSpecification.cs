using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class TokenSpecification : BaseSpecification<Token>
    {
        public TokenSpecification() : base()
        {
        }

        public TokenSpecification(int idUsuario, string name) : base(x =>
                                                                          x.UserId == idUsuario && 
                                                                          x.Name == name)
        {

        }

        public TokenSpecification(int idUsuario, string AbreviacionApp, string name, string transaccion, bool indHabilitado) : base(x =>
                                                                                x.IdAplicacion == AbreviacionApp && 
                                                                                x.UserId == idUsuario &&
                                                                                x.Name == name &&
                                                                                x.Transaccion == transaccion &&
                                                                                x.IndHabilitado == indHabilitado)
        {

        }

        public TokenSpecification(int idUsuario, string AbreviacionApp, string firma, string name) : base(x => 
                                                                                x.UserId == idUsuario &&
                                                                                x.IdAplicacion == AbreviacionApp &&
                                                                                x.FirmaJWT == firma &&
                                                                                x.Name == name)
        {
        }

        public TokenSpecification(int idUsuario, string AbreviacionApp, string firma, string name, string transaccion, bool indHabilitado, bool vigente) : base(x => 
                                                                    x.UserId == idUsuario &&
                                                                    x.IdAplicacion == AbreviacionApp &&
                                                                    x.FirmaJWT == firma &&
                                                                    x.Name == name &&
                                                                    x.Transaccion == transaccion &&
                                                                    x.IndHabilitado == indHabilitado &&
                                                                    x.tokenVigente() == vigente)
        {
        }


        
        public TokenSpecification(int idUsuario, string AbreviacionApp, string name, bool indHabilitado) : base(x => 
                                                                    x.UserId == idUsuario &&
                                                                    x.IdAplicacion == AbreviacionApp &&
                                                                    x.Name == name &&
                                                                    x.IndHabilitado == indHabilitado)
        {
        }

    }
}
