using Autenticacion.Dominio.Specification;
using Autenticacion.Infraestructura.Entities.Auth;

namespace Autenticacion.Infraestructura.Specification
{
    public class TipoDocumentoSpecification : BaseSpecification<TipoDocumento>
    {
        public TipoDocumentoSpecification() : base(ta => ta.indHabilitado == true)
        {
            
        }

        public TipoDocumentoSpecification(int id) : base(x => x.IdTipo == id)
        {
        }
    }
}
