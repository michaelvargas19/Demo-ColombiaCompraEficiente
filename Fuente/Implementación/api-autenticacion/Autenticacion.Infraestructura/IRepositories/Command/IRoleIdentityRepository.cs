using Autenticacion.Infraestructura.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.IRepositories.Command
{
    public interface IRoleIdentityRepository
    {
        IEnumerable<Rol> GetAllRoles(Usuario usuario);

        Task<IdentityResult> CreateRoleAsync(string idAplicacion, string nombre, string display, string descripcion);

        Task<IdentityResult> CreateRelationUserRoleAsync(string userName, int idRole);

    }
}
