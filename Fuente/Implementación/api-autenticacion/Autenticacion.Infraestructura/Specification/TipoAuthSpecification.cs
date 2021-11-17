using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities.Auth;

namespace Autenticacion.Infraestructura.Specification
{
    public class TipoAuthSpecification : BaseSpecification<TipoAutenticacion>
    {
        public TipoAuthSpecification() : base(ta => ta.IndHabilitado == true)
        {
            
        }

        public TipoAuthSpecification(int id) : base(x => x.IdTipo == id)
        {
        }
    }
}
