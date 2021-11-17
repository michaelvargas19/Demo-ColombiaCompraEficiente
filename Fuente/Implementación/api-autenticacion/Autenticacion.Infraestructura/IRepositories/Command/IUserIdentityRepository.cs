using Autenticacion.Infraestructura.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace Autenticacion.Infraestructura.IRepositories.Command
{
    public interface IUserIdentityRepository
    {
        IdentityResult AsigRoleUser(string userName, int idRole);

        IdentityResult RemoveAsigRoleUser(string userName, int idRole);


        int GetAccessFailedCount(Usuario usuario);

        IdentityResult AccessFailed(Usuario usuario);

        IdentityResult ResetAccessFailedCount(Usuario usuario);


        IdentityResult CreateUser(Usuario usuario, string contrasena);

        IEnumerable<Usuario> ReadAllUsers_WithOutManager();

        IEnumerable<Usuario> ReadAllUsers_WithManager();

        Usuario ReadByNameUser(string userName);

        Usuario ReadByIdUsers(string id);
        IdentityResult DeleteUser(string userName);

        IList<Claim> obtegerClaims(string nickname);

    }
}
