using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities;
using Autenticacion.Infraestructura.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.Specification
{
    public class UsuarioSpecification : BaseSpecification<Usuario>
    {
        public UsuarioSpecification() : base()
        {
        }

        public UsuarioSpecification(int idUsuario) : base(x => x.Id == idUsuario)
        {

        }

        public UsuarioSpecification(bool indHabilitado, string usuario) : base(x => x.NormalizedUserName == usuario.ToUpper() && x.IndHabilitado == indHabilitado)
        {

        }

        public UsuarioSpecification(string email, bool indHabilitado) : base(x =>  x.NormalizedEmail == email.ToUpper() && x.IndHabilitado == indHabilitado)
        {
        }

        public UsuarioSpecification(string usuario, string email) : base(x => x.NormalizedUserName == usuario.ToUpper()  && x.NormalizedEmail == email.ToUpper() && x.IndHabilitado == true)
        {
        }

    }
}
